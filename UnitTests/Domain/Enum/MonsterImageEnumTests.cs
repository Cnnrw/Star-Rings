using Game.Enums;

using NUnit.Framework;

namespace UnitTests.Models
{
    [TestFixture]
    public class MonsterImageEnumTests
    {
        [Test]
        public void MonsterImageEnumTests_GetListMonsters_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnumHelper.GetListMonsters;

            // Reset

            // Assert
            Assert.AreEqual(8, result.Count);
        }

        [Test]
        public void MonsterImageEnumTests_ConvertStringToEnum_Orc_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnumHelper.ConvertStringToEnum("Orc");

            // Reset

            // Assert
            Assert.AreEqual(MonsterImageEnum.Orc, result);
        }
    }
}
