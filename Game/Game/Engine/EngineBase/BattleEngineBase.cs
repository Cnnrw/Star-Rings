using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Game.Enums;
using Game.Models;

namespace Game.Engine.EngineBase
{
    /// <summary>
    /// Battle Engine for the Game
    /// </summary>
    public class BattleEngineBase : IBattleEngineInterface
    {

        // Track if the Battle is Running or Not
        internal bool BattleRunning;
        // The Round
        public IRoundEngineInterface Round { get; set; }

        // Engine Settings
        public EngineSettingsModel EngineSettings { get; } = EngineSettingsModel.Instance;

        /// <summary>
        /// Add the charcter to the character list
        /// </summary>
        public virtual bool PopulateCharacterList(CharacterModel data)
        {
            EngineSettings.CharacterList.Add(new PlayerInfoModel(data));

            return true;
        }

        /// <summary>
        /// Start the Battle
        /// </summary>
        /// <param name="isAutoBattle"></param>
        /// <returns></returns>
        public virtual bool StartBattle(bool isAutoBattle)
        {
            // Reset the Score so it is fresh
            EngineSettings.BattleScore = new ScoreModel {AutoBattle = isAutoBattle};

            BattleRunning = true;

            Round.NewRound();

            return true;
        }

        /// <summary>
        /// End the Battle
        /// </summary>
        /// <returns></returns>
        public virtual bool EndBattle()
        {
            BattleRunning = false;

            EngineSettings.BattleScore.CalculateScore();

            return true;
        }

        /// <summary>
        /// Set the Battle State
        /// </summary>
        public bool SetBattleStateEnum(BattleStateEnum data)
        {
            EngineSettings.BattleStateEnum = data;

            return true;
        }
    }
}
