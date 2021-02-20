using Game.Helpers;

using NUnit.Framework;

namespace UnitTests.GameRules
{
    [TestFixture]
    public class LevelTableHelperTests
    {
        [Test]
        public void LevelTableHelper_Valid_ClearAndLoadDataTable_Should_Pass()
        {
            // Arrange

            // Act
            var result = LevelTableHelper.ClearAndLoadDataTable();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void LevelTableHelper_Valid_LoadLevelData_Should_Pass()
        {
            // Arrange

            // Act
            var result = LevelTableHelper.LoadLevelData();

            // Reset

            // Assert
            Assert.AreEqual(44, result);
        }
    }
}