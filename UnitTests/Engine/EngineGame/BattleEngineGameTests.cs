using Game.Engine.EngineGame;
using Game.Models;

using NUnit.Framework;

namespace UnitTests.Engine.EngineGame
{
    [TestFixture]
    public class BattleEngineGameTests
    {

        [SetUp]
        public void Setup()
        {
            _engine = new BattleEngine
            {
                Round = new RoundEngine
                {
                    Turn = new TurnEngine()
                }
            };
        }

        [TearDown]
        public void TearDown()
        { }

        private BattleEngine _engine;

        [Test]
        public void BattleEngine_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = _engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BattleEngine_EndBattle_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = _engine.EndBattle();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void BattleEngine_Valid_PopulateCharacterList_Should_Pass()
        {
            // Arrange
            var character = new CharacterModel();

            // Act
            var result = _engine.PopulateCharacterList(character);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}
