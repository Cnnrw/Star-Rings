using Game.Enums;

using NUnit.Framework;

namespace UnitTests.Models
{
    [TestFixture]
    public class MonsterImageEnumExtensionsTests
    {
        [Test]
        public void MonsterImageEnumExtensionsTests_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.Unknown.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Monster", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_DarkElf_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.DarkElf.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Dark Elf", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_DeadKing_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.DeadKing.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Dead King", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_Nazgul_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.Nazgul.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Nazgul", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_Oliphant_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.Oliphant.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Oliphant", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_Orc_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.Orc.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Orc", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_Spider_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.Spider.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Spider", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_Troll_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.Troll.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Troll", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_WargRider_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.WargRider.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Warg Rider", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_ToImageURI_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.Unknown.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("item.png", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_ToImageURI_DarkElf_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.DarkElf.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("dark_elf.png", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_ToImageURI_DeadKing_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.DeadKing.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("dead_king.png", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_ToImageURI_Nazgul_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.Nazgul.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("nazgul.png", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_ToImageURI_Oliphant_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.Oliphant.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("oliphant.png", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_ToImageURI_Orc_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.Orc.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("orc.png", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_ToImageURI_Spider_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.Spider.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("spider.png", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_ToImageURI_Troll_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.Troll.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("troll.png", result);
        }

        [Test]
        public void MonsterImageEnumExtensionsTests_ToImageURI_WargRider_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterImageEnum.WargRider.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("warg_rider.png", result);
        }
    }
}
