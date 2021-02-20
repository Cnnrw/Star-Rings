using Game.Models;

using NUnit.Framework;

namespace UnitTests.Models
{
    [TestFixture]
    public class CoordinatesModelTests
    {
        [Test]
        public void CoordinatesModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new CoordinatesModel();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CoordinatesModel_Get_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new CoordinatesModel();

            // Reset

            // Assert
            Assert.AreEqual(0, result.Row);
            Assert.AreEqual(0, result.Column);
        }

        [Test]
        public void CoordinatesModel_Set_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new CoordinatesModel {Row = 3, Column = 2};

            // Reset

            // Assert
            Assert.AreEqual(3, result.Row);
            Assert.AreEqual(2, result.Column);
        }
    }
}