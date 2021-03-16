using Game.Enums;

using NUnit.Framework;

namespace UnitTests.Models
{
    [TestFixture]
    public class ItemLocationEnumTests
    {
        [Test]
        public void ItemLocationEnumTests_GetLocationByPosition_Unknown_Should_Pass()
        {
            // Arrange

            // Act
            var result = ItemLocationEnumHelper.GetLocationByPosition(99);

            // Reset

            // Assert
            Assert.AreEqual(ItemLocationEnum.Unknown, result);
        }
    }
}
