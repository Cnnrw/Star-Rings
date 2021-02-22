using Game.Engine.EngineGame;

using NUnit.Framework;

namespace UnitTests.Engine.EngineGame
{
    [TestFixture]
    public class RoundEngineGameTests
    {

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();

            Engine.Round = new RoundEngine();
            Engine.Round.Turn = new TurnEngine();
            Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            //Engine.StartBattle(true);   
        }

        [TearDown]
        public void TearDown()
        {
        }

        private BattleEngine Engine;

        [Test]
        public void RoundEngine_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
    }
}