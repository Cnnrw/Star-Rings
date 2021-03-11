using System.Collections.Generic;

using Game.Enums;
using Game.Models;

namespace Game.Engine.EngineModels
{
    /// <summary>
    /// Holds the Data Structures for the Battle Engine
    /// </summary>
    public class EngineSettingsModel
    {
        // Holds the official ScoreModel
        public ScoreModel BattleScore { get; set; } = new ScoreModel();

        // Holds the Battle Messages as they happen
        public BattleMessagesModel BattleMessagesModel { get; set; } = new BattleMessagesModel();

        // The Pool of items collected during the round as turns happen
        public List<ItemModel> ItemPool { get; set; } = new List<ItemModel>();

        // List of Monsters
        public List<PlayerInfoModel> MonsterList { get; set; } = new List<PlayerInfoModel>();

        // List of Characters
        public List<PlayerInfoModel> CharacterList { get; set; } = new List<PlayerInfoModel>();

        // Location of the current round
        public BattleLocationEnum RoundLocation { get; set; } = BattleLocationEnum.Unknown;

        // Current Player who is the attacker
        public PlayerInfoModel CurrentAttacker { get; set; }

        // Current Player who is the Defender
        public PlayerInfoModel CurrentDefender { get; set; }

        // The Action
        public ActionEnum CurrentAction { get; set; }

        // The Action that just happened
        public ActionEnum PreviousAction { get; set; } = ActionEnum.Unknown;

        // When the current action is an ability, what ability was selected
        public AbilityEnum CurrentActionAbility { get; set; }

        // When the current action is an ability, what ability was selected
        public CoordinatesModel CurrentMapLocation { get; set; }

        // When the current action is an ability, what ability was selected
        public CoordinatesModel MoveMapLocation { get; set; }

        // Hold the list of players (MonsterModel, and character by guid), and order by speed
        public List<PlayerInfoModel> PlayerList { get; set; } = new List<PlayerInfoModel>();

        // Current Round State
        public RoundEnum RoundStateEnum { get; set; } = RoundEnum.Unknown;

        // Max Number of Characters
        public int MaxNumberPartyCharacters { get; set; } = 6;

        // Max Number of Monsters
        public int MaxNumberPartyMonsters { get; set; } = 6;

        // Max Number of Rounds for AutoBattle
        public int MaxRoundCount { get; set; } = 100;

        // Max Number of Turns for AutoBattle
        public int MaxTurnCount { get; set; } = 1000;

        // Hold the MapModel
        public MapModel MapModel { get; set; } = new MapModel();

        // Hold the Battle Settings
        public BattleSettingsModel BattleSettingsModel { get; set; } = new BattleSettingsModel();

        // Hold the Battle State, Unknown is default
        public BattleStateEnum BattleStateEnum { get; set; } = BattleStateEnum.Unknown;

        public ActionEnum ForcedPlayerAction { get; set; } = ActionEnum.Unknown;

        public bool EnableTimeWarpedRounds { get; set; } = false;

        public bool ForceTimeWarpedRounds { get; set; } = false;

        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile EngineSettingsModel instance;
        private static readonly object              syncRoot = new object();

        public static EngineSettingsModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new EngineSettingsModel();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton
    }

    public class BattleGlobals
    {
        // HACKATHON #10
        // Determines if MiracleMax can revive a character
        // Set at beginning of battle, and updated to false during a round if a character is dealt lethal dmg
        public bool MiracleMaxCanRevive = true;

        // HACKATHON #17
        // Percentage chance of monsters returning as zombies after they die
        public int ChanceForZombie = 100;
    }

}
