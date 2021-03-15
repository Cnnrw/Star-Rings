using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Game.Engine.EngineBase;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Game.Enums;
using Game.Helpers;
using Game.Models;
using Game.ViewModels;

namespace Game.Engine.EngineGame
{
    /// <summary>
    /// Manages the Rounds
    /// TODO: This whole file boys
    /// </summary>
    public class RoundEngine : RoundEngineBase, IRoundEngineInterface
    {
        // Hold the BaseEngine
        private readonly EngineSettingsModel EngineSettings = EngineSettingsModel.Instance;

        // The Turn Engine
        public RoundEngine()
        {
            Turn = new TurnEngine();
        }

        /// <summary>
        /// Clear the List between Rounds
        /// </summary>
        public override bool ClearLists()
        {
            EngineSettings.ItemPool.Clear();
            EngineSettings.MonsterList.Clear();
            return true;
        }

        /// <summary>
        /// Call to make a new set of monsters..
        /// </summary>
        public override bool NewRound()
        {
            // End the existing round
            EndRound();

            // Remove Character Buffs
            RemoveCharacterBuffs();

            // Choose where the new round will take place
            ChooseRoundLocation(); 

            // Populate New Monsters..
            AddMonstersToRound();

            // Make the BaseEngine.PlayerList
            MakePlayerList();

            // Set Order for the Round
            OrderPlayerListByTurnOrder();

            // Record the ordered PlayerList in the BattleScore
            EngineSettings.BattleScore.RoundsOrderedPlayerLists.Add(EngineSettings.PlayerList);

            // Populate BaseEngine.MapModel with Characters and Monsters
            EngineSettings.MapModel.PopulateMapModel(EngineSettings.PlayerList);

            // Update Score for the RoundCount
            EngineSettings.BattleScore.RoundCount++;

            return true;
        }

        /// <summary>
        /// Chooses a random location for the next round to take place in.
        /// </summary>
        /// <returns>The chosen location</returns>
        public BattleLocationEnum ChooseRoundLocation()
        {
            // TODO: Don't choose a location that has no Monsters for it
            // For instance, if the user doesn't create any monsters that spawn
            // in the Shire, then don't choose the Shire for the round location.
            var validLocations = BattleLocationEnumHelper.GetListBattleLocations;

            int index = DiceHelper.RollDice(1, validLocations.Count()) - 1;
            string chosenLocationName = validLocations[index];
            BattleLocationEnum chosenLocation = BattleLocationEnumHelper.ConvertStringToEnum(chosenLocationName);

            BattleEngineViewModel.Instance.Engine.EngineSettings.RoundLocation = chosenLocation;

            return chosenLocation;
        }

        /// <summary>
        /// Add Monsters to the Round
        /// Because Monsters can be duplicated, this will add 1, 2, 3 to their name
        /// </summary>
        /// <returns></returns>
        public override int AddMonstersToRound()
        {
            int targetLevel = 1;

            // Set target level to the highest level of the characters
            if (EngineSettings.CharacterList.Count() > 0)
            {
                targetLevel = Convert.ToInt32(EngineSettings.CharacterList.Max(m => m.Level));
            }

            // Identify all monsters that can spawn in the current round location
            List<MonsterModel> validMonsters = new List<MonsterModel>();
            ObservableCollection<MonsterModel> allMonsters = MonsterIndexViewModel.Instance.Dataset;

            foreach (MonsterModel monster in allMonsters)
            {
                if (monster.BattleLocation == BattleEngineViewModel.Instance.Engine.EngineSettings.RoundLocation)
                {
                    validMonsters.Add(monster);
                }
            }

            // Add a random number of valid monsters to the round up to max
            int EncounteredMonstersUpperLimit = Math.Min(validMonsters.Count(), EngineSettings.MaxNumberPartyMonsters);
            int EncounteredMonstersCount = DiceHelper.RollDice(1, EncounteredMonstersUpperLimit);

            for (int i = 0; i < EncounteredMonstersCount; i++)
            {
                int index = DiceHelper.RollDice(1, validMonsters.Count()) - 1;

                MonsterModel chosenMonster = validMonsters[index];

                // Choose level
                chosenMonster.Level = DiceHelper.RollDice(1, targetLevel);

                // Adjust values based on Difficulty
                chosenMonster.Attack = chosenMonster.Difficulty.ToModifier(chosenMonster.Attack);
                chosenMonster.Defense = chosenMonster.Difficulty.ToModifier(chosenMonster.Defense);
                chosenMonster.Speed = chosenMonster.Difficulty.ToModifier(chosenMonster.Speed);
                chosenMonster.Level = chosenMonster.Difficulty.ToModifier(chosenMonster.Level);

                // Get the new Max Health
                chosenMonster.MaxHealth = DiceHelper.RollDice(chosenMonster.Level, 10);

                // Adjust the health, If the new Max Health is above the rule for the level, use the original
                var MaxHealthAdjusted = chosenMonster.Difficulty.ToModifier(chosenMonster.MaxHealth);
                if (MaxHealthAdjusted < chosenMonster.Level * 10)
                {
                    chosenMonster.MaxHealth = MaxHealthAdjusted;
                }

                // Level up to the new level
                chosenMonster.LevelUpToValue(chosenMonster.Level);

                // Set ExperienceRemaining so Monsters can both use this method
                chosenMonster.ExperienceRemaining = LevelTableHelper.LevelDetailsList[chosenMonster.Level + 1].Experience;

                // Enter Battle at full health
                chosenMonster.CurrentHealth = chosenMonster.MaxHealth;

                chosenMonster.Head = RandomPlayerHelper.GetMonsterUniqueItem();

                EngineSettings.MonsterList.Add(new PlayerInfoModel(chosenMonster));

                // Remove the chosen monster from the pool of valid monsters
                validMonsters.RemoveAt(index);
            }

            return EngineSettings.MonsterList.Count;
        }

        /// <summary>
        /// At the end of the round
        /// Clear the ItemModel List
        /// Clear the MonsterModel List
        /// </summary>
        public override bool EndRound()
        {
            // In Auto Battle this happens and the characters get their items, In manual mode need to do it manualy
            if (EngineSettings.BattleScore.AutoBattle)
            {
                PickupItemsForAllCharacters();
            }

            // Reset Monster and Item Lists
            ClearLists();

            return true;
        }

        /// <summary>
        /// For each character pickup the items
        /// </summary>
        // In Auto Battle this happens and the characters get their items
        // When called manually, make sure to do the character pickup before calling EndRound
        public override void PickupItemsForAllCharacters()
        {
            // Order characters by lowest level so the weak ones have first pick
            var orderedCharacterList = EngineSettings.CharacterList.OrderBy(a => a.Level);

            foreach (var character in orderedCharacterList)
            {
                PickupItemsFromPool(character);
            }
        }

        /// <summary>
        /// Manage Next Turn
        ///
        /// Decides Who's Turn
        /// Remembers Current Player
        ///
        /// Starts the Turn
        ///
        /// </summary>
        public override RoundEnum RoundNextTurn()
        {
            // No characters, game is over...
            if (EngineSettings.CharacterList.Count < 1)
            {
                // Game Over
                EngineSettings.RoundStateEnum = RoundEnum.GameOver;
                return EngineSettings.RoundStateEnum;
            }

            // Check if round is over
            if (EngineSettings.MonsterList.Count < 1)
            {
                // If over, New Round
                EngineSettings.RoundStateEnum = RoundEnum.NewRound;
                return RoundEnum.NewRound;
            }

            if (EngineSettings.BattleScore.AutoBattle)
            {
                // Decide Who gets next turn
                // Remember who just went...
                EngineSettings.CurrentAttacker = GetNextPlayerTurn();
            }

            // Do the turn....
            if (EngineSettings.BattleScore.AutoBattle)
            {
                Turn.TakeTurn(EngineSettings.CurrentAttacker);
            }

            EngineSettings.RoundStateEnum = RoundEnum.NextTurn;

            return EngineSettings.RoundStateEnum;
        }

        /// <summary>
        /// Get the Next Player to have a turn
        /// </summary>
        public override PlayerInfoModel GetNextPlayerTurn()
        {
            return base.GetNextPlayerTurn();
        }

        /// <summary>
        /// Remove Dead Players from the List
        /// </summary>
        public override List<PlayerInfoModel> RemoveDeadPlayersFromList()
        {
            return base.RemoveDeadPlayersFromList();
        }

        /// <summary>
        /// Order the Players in Turn Sequence
        /// </summary>
        public override List<PlayerInfoModel> OrderPlayerListByTurnOrder()
        {
            // Order is based by...
            // Order by Speed (Descending if the round is not reversed)
            // Then by Highest level (Descending)
            // Then by Highest Experience Points (Descending)
            // Then by Character before MonsterModel (enum assending)
            // Then by Alphabetic on Name (Assending)
            // Then by First in list order (Assending

            EngineSettings.PlayerList = EngineSettings.PlayerList
                .OrderByDescending(a => a.GetSpeed())
                .ThenByDescending(a => a.Level)
                .ThenByDescending(a => a.ExperienceTotal)
                .ThenByDescending(a => a.PlayerType)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.ListOrder)
                .ToList();

            if (EngineSettings.EnableTimeWarpedRounds)
            {
                // Chance to reverse the order of the list so slower players move first
                int ReverseOrderRoll = DiceHelper.RollDice(1, 100);

                if (EngineSettings.ForceTimeWarpedRounds)
                {
                    ReverseOrderRoll = 0;
                }

                if (ReverseOrderRoll <= 5)
                {
                    EngineSettings.PlayerList = EngineSettings.PlayerList
                        .OrderBy(a => a.GetSpeed())
                        .ThenByDescending(a => a.Level)
                        .ThenByDescending(a => a.ExperienceTotal)
                        .ThenByDescending(a => a.PlayerType)
                        .ThenBy(a => a.Name)
                        .ThenBy(a => a.ListOrder)
                        .ToList();
                }
            }

            return EngineSettings.PlayerList;
        }

        /// <summary>
        /// Who is Playing this round?
        /// </summary>
        public override List<PlayerInfoModel> MakePlayerList()
        {
            // Start from a clean list of players
            EngineSettings.PlayerList.Clear();

            // Remember the Insert order, used for Sorting
            var listOrder = 0;

            foreach (var data in EngineSettings.CharacterList)
                if (data.Alive)
                {
                    EngineSettings.PlayerList.Add(
                        new PlayerInfoModel(data)
                        {
                            // Remember the order
                            ListOrder = listOrder
                        }
                    );

                    listOrder++;
                }

            foreach (var data in EngineSettings.MonsterList)
                if (data.Alive)
                {
                    EngineSettings.PlayerList.Add(
                        new PlayerInfoModel(data)
                        {
                            // Remember the order
                            ListOrder = listOrder
                        }
                    );

                    listOrder++;
                }

            // Roll to see if round is experiencing a time warp and should have
            // players act in order of slowest to fastest
            int ReverseOrderRoll = DiceHelper.RollDice(1, 100);


            return EngineSettings.PlayerList;
        }
            
        /// <summary>
        /// Get the Information about the Player
        /// </summary>
        public override PlayerInfoModel GetNextPlayerInList()
        {
            // Walk the list from top to bottom
            // If Player is found, then see if next player exist, if so return that.
            // If not, return first player (looped)

            // If List is empty, return null
            if (EngineSettings.PlayerList.Count == 0) return null;

            // No current player, so set the first one
            if (EngineSettings.CurrentAttacker == null) return EngineSettings.PlayerList.FirstOrDefault();

            // Find current player in the list
            var index = EngineSettings.PlayerList.FindIndex(m => m.Guid.Equals(EngineSettings.CurrentAttacker.Guid));

            // If at the end of the list, return the first element
            if (index == EngineSettings.PlayerList.Count() - 1) return EngineSettings.PlayerList.FirstOrDefault();

            // Return the next element
            return EngineSettings.PlayerList[index + 1];
        } 

        /// <summary>
        /// Pickup Items Dropped
        /// </summary>
        public override bool PickupItemsFromPool(PlayerInfoModel character)
        {
            return base.PickupItemsFromPool(character);
        }

        /// <summary>
        /// Swap out the item if it is better
        ///
        /// Prefer the item with higher damage
        /// Break ties with higher value, then higher range
        /// </summary>
        public override bool GetItemFromPoolIfBetter(PlayerInfoModel character, ItemLocationEnum setLocation)
        {
            var thisLocation = setLocation;
            if (setLocation == ItemLocationEnum.RightFinger)
            {
                thisLocation = ItemLocationEnum.Finger;
            }

            if (setLocation == ItemLocationEnum.LeftFinger)
            {
                thisLocation = ItemLocationEnum.Finger;
            }

            var ItemPool = EngineSettings.ItemPool.Where(a => a.Location == thisLocation)
                .OrderByDescending(a => a.Damage)
                .ThenByDescending(a => a.Value)
                .ThenByDescending(a => a.Range)
                .ToList();

            // If no items in the list, return...
            if (!ItemPool.Any())
            {
                return false;
            }

            var CharacterItem = character.GetItemByLocation(setLocation);
            if (CharacterItem == null)
            {
                SwapCharacterItem(character, setLocation, ItemPool.FirstOrDefault());
                return true;
            }

            foreach (var PoolItem in ItemPool)
            {
                // Swap for PoolItem if it has higher damage
                if (PoolItem.Damage > CharacterItem.Damage)
                {
                    SwapCharacterItem(character, setLocation, PoolItem);
                    return true;
                } else if (PoolItem.Value == CharacterItem.Value)
                {
                    // If damage is equal, swap for PoolItem if it has more value 
                    if (PoolItem.Value > CharacterItem.Value)
                    {
                        SwapCharacterItem(character, setLocation, PoolItem);
                        return true;
                    } else if (PoolItem.Value == CharacterItem.Value)
                    {
                        // If value is also equal, swap for PoolItem if it has a longer range
                        if (PoolItem.Range > CharacterItem.Range)
                        {
                            SwapCharacterItem(character, setLocation, PoolItem);
                            return true;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Swap the Item the character has for one from the pool
        ///
        /// Drop the current item back into the Pool
        /// </summary>
        public override ItemModel SwapCharacterItem(PlayerInfoModel character, ItemLocationEnum setLocation,
                                                    ItemModel       PoolItem)
        {
            return base.SwapCharacterItem(character, setLocation, PoolItem);
        }

        /// <summary>
        /// For all characters in player list, remove their buffs
        /// </summary>
        public override bool RemoveCharacterBuffs()
        {
            return base.RemoveCharacterBuffs();
        }
    }
}
