using Game.Enums;

using NUnit.Framework;

namespace UnitTests.Models
{
    [TestFixture]
    public class BattleLocationEnumTests
    {
        [Test]
        public void BattleLocationEnumTests_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.Unknown.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Battle location", result);
        }

        [Test]
        public void BattleLocationEnumTests_Shire_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.Shire.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("The Shire", result);
        }

        [Test]
        public void BattleLocationEnumTests_ElvenCity_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.ElvenCity.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Elven City", result);
        }

        [Test]
        public void BattleLocationEnumTests_Forest_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.Forest.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Forest", result);
        }

        [Test]
        public void BattleLocationEnumTests_Dungeons_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.Dungeons.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Dungeons", result);
        }

        [Test]
        public void BattleLocationEnumTests_Mordor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.Mordor.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Mordor", result);
        }

        [Test]
        public void BattleLocationEnumTests_ToMessageWithArticle_Dungeons_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.Dungeons.ToMessageWithArticle();

            // Reset

            // Assert
            Assert.AreEqual("the dungeon", result);
        }

        [Test]
        public void BattleLocationEnumTests_ToMessageWithArticle_Forest_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.Forest.ToMessageWithArticle();

            // Reset

            // Assert
            Assert.AreEqual("the forest", result);
        }

        [Test]
        public void BattleLocationEnumTests_ToMessageWithArticle_ElvenCity_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.ElvenCity.ToMessageWithArticle();

            // Reset

            // Assert
            Assert.AreEqual("the Elven city", result);
        }

        [Test]
        public void BattleLocationEnumTests_ToMessageWithArticle_Mordor_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.Mordor.ToMessageWithArticle();

            // Reset

            // Assert
            Assert.AreEqual("Mordor", result);
        }

        [Test]
        public void BattleLocationEnumTests_ToMessageWithArticle_Shire_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.Shire.ToMessageWithArticle();

            // Reset

            // Assert
            Assert.AreEqual("the Shire", result);
        }

        [Test]
        public void BattleLocationEnumTests_ToImageUri_Dungeons_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.Dungeons.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("dungeon_background.png", result);
        }

        [Test]
        public void BattleLocationEnumTests_ToImageUri_ElvenCity_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.ElvenCity.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("rivendell_background.png", result);
        }

        [Test]
        public void BattleLocationEnumTests_ToImageUri_Forest_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.Forest.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("forest_background.png", result);
        }

        [Test]
        public void BattleLocationEnumTests_ToImageUri_Mordor_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.Mordor.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("mordor_background.png", result);
        }

        [Test]
        public void BattleLocationEnumTests_ToImageUri_Shire_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnum.Shire.ToImageURI();

            // Reset

            // Assert
            Assert.AreEqual("shire_background.png", result);
        }

        [Test]
        public void BattleLocationEnumTests_GetListBattleLocations_Should_Pass()
        {
            // Arrange

            // Act
            var result = BattleLocationEnumHelper.GetListBattleLocations;

            // Reset

            // Assert
            Assert.AreEqual(5, result.Count);
        }
    }
}
