using System;
using System.Collections.Generic;

using Game.Engine.EngineBase;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Game.Enums;
using Game.Models;

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
        /// Add Monsters to the Round
        ///
        /// Because Monsters can be duplicated, will add 1, 2, 3 to their name
        ///
        /// </summary>
        /// <returns></returns>
        /*
         * Hint:
         * I don't have crudi monsters yet so will add 6 new ones..
         * If you have crudi monsters, then pick from the list
         * Consdier how you will scale the monsters up to be appropriate for the characters to fight
         *
         */
        // TODO: Teams, You need to implement your own Logic can not use mine.
        public override int AddMonstersToRound() => throw new NotImplementedException();

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
        public override RoundEnum RoundNextTurn() =>
            throw
                // No characters, game is over..
                // Check if round is over
                // If in Auto Battle pick the next attacker
                // Do the turn..
                new NotImplementedException();

        /// <summary>
        /// Get the Next Player to have a turn
        /// </summary>
        public override PlayerInfoModel GetNextPlayerTurn() =>
            throw
                // Remove the Dead
                // Get Next Player
                new NotImplementedException();

        /// <summary>
        /// Remove Dead Players from the List
        /// </summary>
        public override List<PlayerInfoModel> RemoveDeadPlayersFromList() => throw new NotImplementedException();

        /// <summary>
        /// Order the Players in Turn Sequence
        /// </summary>
        public override List<PlayerInfoModel> OrderPlayerListByTurnOrder() =>
            throw
                // TODO Teams: Implement the order
                new NotImplementedException();

        /// <summary>
        /// Who is Playing this round?
        /// </summary>
        public override List<PlayerInfoModel> MakePlayerList() =>
            throw
                // Start from a clean list of players
                // Remember the Insert order, used for Sorting
                // Add the Characters
                // Add the Monsters
                new NotImplementedException();

        /// <summary>
        /// Get the Information about the Player
        /// </summary>
        public override PlayerInfoModel GetNextPlayerInList() =>
            throw
                // Walk the list from top to bottom
                // If Player is found, then see if next player exist, if so return that.
                // If not, return first player (looped)
                // If List is empty, return null
                // No current player, so set the first one
                // Find current player in the list
                // If at the end of the list, return the first element
                // Return the next element
                new NotImplementedException();

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
        public override bool RemoveCharacterBuffs() => throw new NotImplementedException();
    }
}