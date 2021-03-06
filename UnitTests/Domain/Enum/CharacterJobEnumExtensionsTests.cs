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
        public void CharacterJobEnumExtensionsTests_Smuggler_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Smuggler.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Smuggler", result);
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

        [Test]
        public void CharacterJobEnumExtensionsTests_AstroDroid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.AstroDroid.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Astro Droid", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ProtocolDroid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.ProtocolDroid.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Protocol Droid", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ToImageURI_Jedi_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Jedi.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("jedi.png", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ToImageURI_Princess_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Princess.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("jedi_princess.png", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ToImageURI_Smuggler_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Smuggler.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("smuggler.png", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ToImageURI_Wookie_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Wookie.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("wookie.png", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ToImageURI_AstroDroid_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.AstroDroid.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("astromech.png", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ToImageURI_ProtocolDroid_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.ProtocolDroid.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("protocol_droid.png", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ToIconImageURI_Jedi_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Jedi.ToIconImageURI();

            // Reset

            // Assert
            Assert.AreEqual("jedi_icon.png", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ToIconImageURI_Princess_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Princess.ToIconImageURI();

            // Reset

            // Assert
            Assert.AreEqual("jedi_princess_icon.png", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ToIconImageURI_Smuggler_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Smuggler.ToIconImageURI();

            // Reset

            // Assert
            Assert.AreEqual("smuggler_icon.png", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ToIconImageURI_Wookie_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Wookie.ToIconImageURI();

            // Reset

            // Assert
            Assert.AreEqual("wookie_icon.png", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ToIconImageURI_AstroDroid_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.AstroDroid.ToIconImageURI();

            // Reset

            // Assert
            Assert.AreEqual("astromech_icon.png", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ToIconImageURI_ProtocolDroid_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.ProtocolDroid.ToIconImageURI();

            // Reset

            // Assert
            Assert.AreEqual("protocol_droid_icon.png", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_ToIconImageURI_Unknown_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Unknown.ToIconImageURI();

            // Reset

            // Assert
            Assert.AreEqual("item.png", result);
        }
    }
}
