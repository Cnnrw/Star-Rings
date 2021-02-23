using System;
using System.Linq;

using Game.Enums;

using NUnit.Framework;

namespace UnitTests.Helpers
{
    [TestFixture]
    class BattleLocationEnumHelperTests
    {
        [Test]
        public void BattleLocationEnumHelper_ConvertStringToEnum_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnumHelper.ConvertStringToEnum("Shire");

            // Reset

            // Assert
            Assert.AreEqual(BattleLocationEnum.Shire, result);
        }
    }
}