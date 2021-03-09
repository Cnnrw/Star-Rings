using Game.Engine.EngineGame;

using NUnit.Framework;

namespace UnitTests.Engine.EngineGame
{
    [TestFixture]
    public class TurnEngineGameTests
    {

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();
            Engine.Round = new RoundEngine();
            Engine.Round.Turn = new TurnEngine();
            //Engine.StartBattle(true);   // Start engine in auto battle mode
        }

        [TearDown]
        public void TearDown()
        {
        }

        private BattleEngine Engine;

        [Test]
        public void TurnEngine_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void SelectCharacterToAttack_Null_Player_List_Returns_Null()
        {
            // Arrange
            Engine.EngineSettings.PlayerList = null;
            

            // Act
            var result = Engine.Round.Turn.SelectCharacterToAttack();

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void SelectCharacterToAttack_Empty_Player_List_Returns_Null()
        {
            // Arrange
            Engine.EngineSettings.PlayerList.Clear();

            // Act
            var result = Engine.Round.Turn.SelectCharacterToAttack();

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }
    }
}
