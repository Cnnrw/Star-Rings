using System.Linq;
using System.Threading.Tasks;
using Game.Engine.EngineBase;
using Game.Engine.EngineInterfaces;
using Game.ViewModels;

namespace Game.Engine.EngineGame
{
    /// <summary>
    /// AutoBattle Engine
    /// Runs the engine simulation with no user interaction
    /// </summary>
    public class AutoBattleEngine : AutoBattleEngineBase, IAutoBattleInterface
    {
        #region Algrorithm
        // Prepare for Battle
        // Pick 6 Characters
        // Initialize the Battle
        // Start a Round

        // Fight Loop
        // Loop in the round each turn
        // If Round is over, Start New Round
        // Check end state of round/game

        // Wrap up
        // Get Score
        // Save Score
        // Output Score
        #endregion Algrorithm

        public new IBattleEngineInterface Battle
        {
            get => base.Battle ?? (base.Battle = new BattleEngine());
            set => base.Battle = Battle;
        }

        public override bool CreateCharacterParty()
        {
            // Will first pull from existing characters
            foreach (var data in CharacterIndexViewModel.Instance.Dataset)
            {
                if (Battle.EngineSettings.CharacterList.Count() >= Battle.EngineSettings.MaxNumberPartyCharacters)
                {
                    break;
                }

                // Start off with max health if adding a character in
                data.CurrentHealth = data.GetMaxHealthTotal;
                Battle.PopulateCharacterList(data);
            }

            return true;
        }

        public override bool DetectInfinateLoop()
        {
            return base.DetectInfinateLoop();
        }

        public override Task<bool> RunAutoBattle()
        {
            throw new System.NotImplementedException();
        }
    }
}
