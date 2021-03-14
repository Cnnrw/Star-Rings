using System;
using System.Collections.Generic;
using System.Linq;

using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Game.Enums;
using Game.Helpers;
using Game.Models;

namespace Game.Engine.EngineBase
{
    /// <summary>
    ///     Manages the Rounds
    /// </summary>
    public class RoundEngineBase : IRoundEngineInterface
    {
        // Hold the BaseEngine
        readonly EngineSettingsModel _engineSettings = EngineSettingsModel.Instance;

        // Hold the turn
        public ITurnEngineInterface Turn { get; set; }

        /// <summary>
        ///     Clear the List between Rounds
        /// </summary>
        public virtual bool ClearLists()
        {
            _engineSettings.ItemPool.Clear();
            _engineSettings.MonsterList.Clear();
            return true;
        }

        /// <summary>
        ///     Set the Current Attacker
        /// </summary>
        public virtual bool SetCurrentAttacker(PlayerInfoModel player)
        {
            _engineSettings.CurrentAttacker = player;

            return true;
        }

        /// <summary>
        ///     Set the Current Attacker
        /// </summary>
        public virtual bool SetCurrentDefender(PlayerInfoModel player)
        {
            _engineSettings.CurrentDefender = player;

            return true;
        }

        /// <summary>
        ///     Call to make a new set of monsters...
        /// </summary>
        /// <returns></returns>
        public virtual bool NewRound()
        {
            // End the existing round
            EndRound();

            // Remove Character Buffs
            RemoveCharacterBuffs();

            // Populate New Monsters...
            AddMonstersToRound();

            // Make the PlayerList
            MakePlayerList();

            // Set Order for the Round
            OrderPlayerListByTurnOrder();

            // Populate BaseEngine.MapModel with Characters and Monsters
            _engineSettings.MapModel.PopulateMapModel(_engineSettings.PlayerList);

            // Update Score for the RoundCount
            _engineSettings.BattleScore.RoundCount++;

            return true;
        }

        /*
         * Hint:
         * I don't have crudi monsters yet so will add 6 new ones...
         * If you have crudi monsters, then pick from the list
         * Consider how you will scale the monsters up to be appropriate for the characters to fight
         */
        /// <summary>
        ///     Add Monsters to the Round
        ///     Because Monsters can be duplicated, will add 1, 2, 3 to their name
        /// </summary>
        /// <returns></returns>
        public virtual int AddMonstersToRound()
        {
            // TODO: Teams, You need to implement your own Logic can not use mine.

            var targetLevel = 1;

            // min character level
            if (_engineSettings.CharacterList.Any())
                targetLevel = Convert.ToInt32(_engineSettings.CharacterList.Min(m => m.Level));

            for (var i = 0; i < _engineSettings.MaxNumberPartyMonsters; i++)
            {
                var data = RandomPlayerHelper.GetRandomMonster(targetLevel, _engineSettings.BattleSettingsModel.AllowMonsterItems);

                // Help identify which Monster it is
                data.Name += " " + _engineSettings.MonsterList.Count() + 1;

                _engineSettings.MonsterList.Add(new PlayerInfoModel(data));
            }

            return _engineSettings.MonsterList.Count();
        }

        /// <summary>
        ///     At the end of the round
        ///     Clear the ItemModel List
        ///     Clear the MonsterModel List
        /// </summary>
        /// <returns></returns>
        public virtual bool EndRound()
        {
            // In Auto Battle this happens and the characters get their items, In manual mode need to do it manualy
            if (_engineSettings.BattleScore.AutoBattle) PickupItemsForAllCharacters();

            // Reset Monster and Item Lists
            ClearLists();

            return true;
        }

        /// <summary>
        ///     For each character pickup the items
        /// </summary>
        public virtual void PickupItemsForAllCharacters()
        {
            // In Auto Battle this happens and the characters get their items
            // When called manualy, make sure to do the character pickup before calling EndRound

            // Have each character pickup items...
            foreach (var character in _engineSettings.CharacterList) PickupItemsFromPool(character);
        }

        /// <summary>
        ///     Manage Next Turn
        ///     Decides Who's Turn
        ///     Remembers Current Player
        ///     Starts the Turn
        /// </summary>
        /// <returns></returns>
        public virtual RoundEnum RoundNextTurn()
        {
            // No characters, game is over...
            if (_engineSettings.CharacterList.Count < 1)
            {
                // Game Over
                _engineSettings.RoundStateEnum = RoundEnum.GameOver;
                return _engineSettings.RoundStateEnum;
            }

            // Check if round is over
            if (_engineSettings.MonsterList.Count < 1)
            {
                // If over, New Round
                _engineSettings.RoundStateEnum = RoundEnum.NewRound;
                return RoundEnum.NewRound;
            }

            if (_engineSettings.BattleScore.AutoBattle)
            {
                // Decide Who gets next turn
                // Remember who just went...
                _engineSettings.CurrentAttacker = GetNextPlayerTurn();

                // Only Attack for now
                _engineSettings.CurrentAction = ActionEnum.Attack;
            }

            // Do the turn....
            Turn.TakeTurn(_engineSettings.CurrentAttacker);

            _engineSettings.RoundStateEnum = RoundEnum.NextTurn;

            return _engineSettings.RoundStateEnum;
        }

        /// <summary>
        ///     Get the Next Player to have a turn
        /// </summary>
        /// <returns></returns>
        public virtual PlayerInfoModel GetNextPlayerTurn()
        {
            // Remove the Dead
            RemoveDeadPlayersFromList();

            // Get Next Player
            var PlayerCurrent = GetNextPlayerInList();

            return PlayerCurrent;
        }

        /// <summary>
        ///     Remove Dead Players from the List
        /// </summary>
        /// <returns></returns>
        public virtual List<PlayerInfoModel> RemoveDeadPlayersFromList()
        {
            _engineSettings.PlayerList = _engineSettings.PlayerList.Where(m => m.Alive).ToList();
            return _engineSettings.PlayerList;
        }

        /// <summary>
        ///     Order the Players in Turn Sequence
        /// </summary>
        public virtual List<PlayerInfoModel> OrderPlayerListByTurnOrder()
        {
            // Order is based by...
            // Order by Speed (Desending)
            // Then by Highest level (Descending)
            // Then by Highest Experience Points (Descending)
            // Then by Character before MonsterModel (enum assending)
            // Then by Alphabetic on Name (Assending)
            // Then by First in list order (Assending

            _engineSettings.PlayerList = _engineSettings.PlayerList.OrderByDescending(a => a.GetSpeed())
                                                        .ThenByDescending(a => a.Level)
                                                        .ThenByDescending(a => a.ExperienceTotal)
                                                        .ThenByDescending(a => a.PlayerType)
                                                        .ThenBy(a => a.Name)
                                                        .ThenBy(a => a.ListOrder)
                                                        .ToList();

            return _engineSettings.PlayerList;
        }

        /// <summary>
        ///     Who is Playing this round?
        /// </summary>
        public virtual List<PlayerInfoModel> MakePlayerList()
        {
            // Start from a clean list of players
            _engineSettings.PlayerList.Clear();

            // Remember the Insert order, used for Sorting
            var listOrder = 0;

            foreach (var data in _engineSettings.CharacterList)
                if (data.Alive)
                {
                    _engineSettings.PlayerList.Add(
                                                   new PlayerInfoModel(data)
                                                   {
                                                       // Remember the order
                                                       ListOrder = listOrder
                                                   });

                    listOrder++;
                }

            foreach (var data in _engineSettings.MonsterList)
                if (data.Alive)
                {
                    _engineSettings.PlayerList.Add(
                                                   new PlayerInfoModel(data)
                                                   {
                                                       // Remember the order
                                                       ListOrder = listOrder
                                                   });

                    listOrder++;
                }

            return _engineSettings.PlayerList;
        }

        /// <summary>
        ///     Who is Playing this round?
        /// </summary>
        public virtual List<PlayerInfoModel> PlayerList()
        {
            // Start from a clean list of players
            _engineSettings.PlayerList.Clear();

            // Remember the Insert order, used for Sorting
            var listOrder = 0;

            foreach (var data in _engineSettings.CharacterList)
                if (data.Alive)
                {
                    _engineSettings.PlayerList.Add(
                                                   new PlayerInfoModel(data)
                                                   {
                                                       // Remember the order
                                                       ListOrder = listOrder
                                                   });

                    listOrder++;
                }

            foreach (var data in _engineSettings.MonsterList.Where(data => data.Alive))
            {
                _engineSettings.PlayerList.Add(
                                               new PlayerInfoModel(data)
                                               {
                                                   // Remember the order
                                                   ListOrder = listOrder
                                               });

                listOrder++;
            }

            return _engineSettings.PlayerList;
        }

        /// <summary>
        ///     Get the Information about the Player
        /// </summary>
        /// <returns></returns>
        public virtual PlayerInfoModel GetNextPlayerInList()
        {
            // Walk the list from top to bottom
            // If Player is found, then see if next player exist, if so return that.
            // If not, return first player (looped)

            // If List is empty, return null
            if (_engineSettings.PlayerList.Count == 0) return null;

            // No current player, so set the first one
            if (_engineSettings.CurrentAttacker == null) return _engineSettings.PlayerList.FirstOrDefault();

            // Find current player in the list
            var index = _engineSettings.PlayerList.FindIndex(m => m.Guid.Equals(_engineSettings.CurrentAttacker.Guid));

            // If at the end of the list, return the first element
            if (index == _engineSettings.PlayerList.Count() - 1) return _engineSettings.PlayerList.FirstOrDefault();

            // Return the next element
            return _engineSettings.PlayerList[index + 1];
        }

        /// <summary>
        ///     Pickup Items Dropped
        /// </summary>
        /// <param name="character"></param>
        public virtual bool PickupItemsFromPool(PlayerInfoModel character)
        {
            // TODO: Teams, You need to implement your own Logic if not using auto apply

            // I use the same logic for Auto Battle as I do for Manual Battle

            //if (BaseEngine.BattleScore.AutoBattle)
            {
                // Have the character, walk the items in the pool, and decide if any are better than current one.

                GetItemFromPoolIfBetter(character, ItemLocationEnum.Head);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.Necklace);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.PrimaryHand);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.OffHand);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.RightFinger);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.LeftFinger);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.Feet);
            }
            return true;
        }

        /// <summary>
        ///     Swap out the item if it is better
        ///     Uses Value to determine
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        public virtual bool GetItemFromPoolIfBetter(PlayerInfoModel character, ItemLocationEnum setLocation)
        {
            var thisLocation = setLocation;
            if (setLocation == ItemLocationEnum.RightFinger) thisLocation = ItemLocationEnum.Finger;

            if (setLocation == ItemLocationEnum.LeftFinger) thisLocation = ItemLocationEnum.Finger;

            var myList = _engineSettings.ItemPool.Where(a => a.Location == thisLocation)
                                        .OrderByDescending(a => a.Value)
                                        .ToList();

            // If no items in the list, return...
            if (!myList.Any()) return false;

            var CharacterItem = character.GetItemByLocation(setLocation);
            if (CharacterItem == null)
            {
                SwapCharacterItem(character, setLocation, myList.FirstOrDefault());
                return true;
            }

            foreach (var PoolItem in myList)
                if (PoolItem.Value > CharacterItem.Value)
                {
                    SwapCharacterItem(character, setLocation, PoolItem);
                    return true;
                }

            return true;
        }

        /// <summary>
        ///     Swap the Item the character has for one from the pool
        ///     Drop the current item back into the Pool
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        /// <param name="PoolItem"></param>
        /// <returns></returns>
        public virtual ItemModel SwapCharacterItem(PlayerInfoModel character, ItemLocationEnum setLocation,
                                                   ItemModel       PoolItem)
        {
            // Put on the new ItemModel, which drops the one back to the pool
            var droppedItem = character.AddItem(setLocation, PoolItem.Id);

            // Add the PoolItem to the list of selected items
            _engineSettings.BattleScore.ItemModelSelectList.Add(PoolItem);

            // Remove the ItemModel just put on from the pool
            _engineSettings.ItemPool.Remove(PoolItem);

            if (droppedItem != null)
                // Add the dropped ItemModel to the pool
                _engineSettings.ItemPool.Add(droppedItem);

            return droppedItem;
        }

        /// <summary>
        ///     For all characters in player list, remove their buffs
        /// </summary>
        /// <returns></returns>
        public virtual bool RemoveCharacterBuffs()
        {
            foreach (var data in _engineSettings.PlayerList) data.ClearBuffs();

            foreach (var data in _engineSettings.CharacterList) data.ClearBuffs();
            return true;
        }
    }
}
