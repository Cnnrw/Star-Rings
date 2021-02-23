using Game.Enums;

using NUnit.Framework;

namespace UnitTests.Models
{
    [TestFixture]
    public class CharacterJobEnumExtensionsTests
    {
        [Test]
        public void CharacterJobEnumExtensionsTests_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Unknown.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Player", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_Jedi_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Jedi.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Jedi", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_Princess_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Princess.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Princess", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_Wookie_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Wookie.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Wookie", result);
        }
    }
}
