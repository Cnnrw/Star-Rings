using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public new ITurnEngineInterface Turn
        {
            get => base.Turn ?? (base.Turn = new TurnEngine());
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                base.Turn = Turn;
            }
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

            RoundLocation = chosenLocation;

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
                if (monster.BattleLocation == RoundLocation)
                {
                    validMonsters.Add(monster);
                }
            }

            // Add a random number of valid monsters to the round
            int encounteredMonsterCount = DiceHelper.RollDice(1, validMonsters.Count());

            for (int i = 0; i < encounteredMonsterCount; i++)
            {
                int index = DiceHelper.RollDice(1, validMonsters.Count()) - 1;
                MonsterModel chosenMonster = validMonsters[index];

                // Help identify which Monster it is
                chosenMonster.Name += " " + (EngineSettings.MonsterList.Count + 1);

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
        // When called manualy, make sure to do the character pickup before calling EndRound
        public override void PickupItemsForAllCharacters() => throw new NotImplementedException();

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
            return base.RoundNextTurn();
        }

        /// <summary>
        /// Get the Next Player to have a turn
        /// </summary>
        public override PlayerInfoModel GetNextPlayerTurn()
        {
            return base.GetNextPlayerInList();
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
            return base.OrderPlayerListByTurnOrder();
        }

        /// <summary>
        /// Who is Playing this round?
        /// </summary>
        public override List<PlayerInfoModel> MakePlayerList()
        {
            return base.MakePlayerList();
        }
            
        /// <summary>
        /// Get the Information about the Player
        /// </summary>
        public override PlayerInfoModel GetNextPlayerInList()
        {
            return base.GetNextPlayerInList();
        } 

        /// <summary>
        /// Pickup Items Dropped
        /// </summary>
        public override bool PickupItemsFromPool(PlayerInfoModel character) =>
            throw
                // TODO: Teams, You need to implement your own Logic if not using auto apply
                // I use the same logic for Auto Battle as I do for Manual Battle
                new NotImplementedException();

        /// <summary>
        /// Swap out the item if it is better
        ///
        /// Uses Value to determine
        /// </summary>
        public override bool GetItemFromPoolIfBetter(PlayerInfoModel character, ItemLocationEnum setLocation) =>
            throw new NotImplementedException();

        /// <summary>
        /// Swap the Item the character has for one from the pool
        ///
        /// Drop the current item back into the Pool
        /// </summary>
        public override ItemModel SwapCharacterItem(PlayerInfoModel character, ItemLocationEnum setLocation,
                                                    ItemModel       PoolItem) =>
            throw new NotImplementedException();

        /// <summary>
        /// For all characters in player list, remove their buffs
        /// </summary>
        public override bool RemoveCharacterBuffs()
        {
            return base.RemoveCharacterBuffs();
        }
    }
}
