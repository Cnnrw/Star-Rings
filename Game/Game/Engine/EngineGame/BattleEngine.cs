using Game.Engine.EngineBase;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;

namespace Game.Engine.EngineGame
{
    /// <summary>
    /// Battle Engine for the Game
    /// </summary>
    public class BattleEngine : BattleEngineBase, IBattleEngineInterface
    {
        // The BaseEngine
        public new EngineSettingsModel EngineSettings { get; } = EngineSettingsModel.Instance;

        /// <summary>
        /// Default Construtor
        /// </summary>
        public BattleEngine()
        {
            Round = new RoundEngine();
        }
    }
}
