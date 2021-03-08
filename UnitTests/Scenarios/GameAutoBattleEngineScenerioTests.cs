using System.Threading.Tasks;

using Game.Engine.EngineGame;
using Game.Helpers;
using Game.Models;

using NUnit.Framework;

namespace Scenario
{
    [TestFixture]
    public class GameAutoBattleEngineScenarioTests
    {

        [SetUp]
        public void Setup()
        {
            AutoBattle = new AutoBattleEngine();

            AutoBattle.Battle.EngineSettings.CharacterList.Clear();
            AutoBattle.Battle.EngineSettings.MonsterList.Clear();
            AutoBattle.Battle.EngineSettings.CurrentDefender = null;
            AutoBattle.Battle.EngineSettings.CurrentAttacker = null;

            AutoBattle.Battle.StartBattle(true); // Clear the Engine
        }

        [TearDown]
        public void TearDown()
        {
        }

        private AutoBattleEngine AutoBattle;

        [Test]
        public void AutoBattleEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AutoBattle;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
