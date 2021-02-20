using Game.Engine.EngineBase;
using Game.Enums;
using Game.Models;

using NUnit.Framework;

namespace UnitTests.Engine.EngineBase
{
    [TestFixture]
    public class BattleEngineBaseTests
    {

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngineBase();
            Engine.Round = new RoundEngineBase();
            Engine.Round.Turn = new TurnEngineBase();
        }

        [TearDown]
        public void TearDown()
        {
        }

        BattleEngineBase Engine;

        [Test]
        public void BattleEngine_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BattleEngine_StartBattle_Valid_AutoModel_True_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.StartBattle(true);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, Engine.EngineSettings.BattleScore.AutoBattle);
        }

        [Test]
        public void BattleEngine_EndBattle_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.EndBattle();

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
            var result = Engine.PopulateCharacterList(character);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void BattleEngine_SetBattleStateEnum_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.SetBattleStateEnum(BattleStateEnum.Battling);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}