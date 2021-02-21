using Game.Enums;
using Game.Models;

using NUnit.Framework;

namespace UnitTests.Models
{
    // TODO: Idk if Wookies have the ability to heal so I've commented those methods out -CW
    [TestFixture]
    public class PlayerInfoModelTests
    {
        [Test]
        public void PlayerInfoModel_Constructor_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel();

            // Act
            var result = new PlayerInfoModel(data);

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PlayerInfoModel_Constructor_Character_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterModel();

            // Act
            var result = new PlayerInfoModel(data);

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PlayerInfoModel_Constructor_Monster_Default_Should_Pass()
        {
            // Arrange
            var data = new MonsterModel();

            // Act
            var result = new PlayerInfoModel(data);

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PlayerInfoModel_Constructor_Character_Jedi_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterModel {Job = CharacterJobEnum.Jedi};

            // Act
            var result = new PlayerInfoModel(data);

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PlayerInfoModel_Constructor_Character_Wookie_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterModel {Job = CharacterJobEnum.Wookie};

            // Act
            var result = new PlayerInfoModel(data);

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PlayerInfoModel_Constructor_Character_Unknown_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterModel {Job = CharacterJobEnum.Unknown};

            // Act
            var result = new PlayerInfoModel(data);

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PlayerInfoModel_IsAbilityAvailable_Available_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel {Job = CharacterJobEnum.Wookie});

            // Act
            var result = data.IsAbilityAvailable(AbilityEnum.Heal);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void PlayerInfoModel_IsAbilityAvailable_Available_Zero_Should_Fail()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel {Job = CharacterJobEnum.Wookie});
            data.AbilityTracker[AbilityEnum.Heal] = 0;

            // Act
            var result = data.IsAbilityAvailable(AbilityEnum.Heal);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        // [Test]
        // public void PlayerInfoModel_SelectHealingAbility_Wookie_Heal_Available_Should_Pass()
        // {
        //     // Arrange
        //     var data = new PlayerInfoModel(new CharacterModel {Job = CharacterJobEnum.Wookie});
        //     data.AbilityTracker[AbilityEnum.Heal] = 1;
        //
        //     data.CurrentHealth = 1;
        //     data.MaxHealth = 100;
        //
        //     // Act
        //     var result = data.SelectHealingAbility();
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.AreEqual(AbilityEnum.Heal, result);
        // }

        // [Test]
        // public void PlayerInfoModel_SelectHealingAbility_Cleric_Heal_Not_Needed_Should_Pass()
        // {
        //     // Arrange
        //     var data = new PlayerInfoModel(new CharacterModel {Job = CharacterJobEnum.Wookie});
        //     data.AbilityTracker[AbilityEnum.Heal] = 1;
        //
        //     data.CurrentHealth = 100;
        //     data.MaxHealth = 100;
        //
        //     // Act
        //     var result = data.SelectHealingAbility();
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.AreEqual(AbilityEnum.Unknown, result);
        // }

        // [Test]
        // public void PlayerInfoModel_SelectHealingAbility_Cleric_Heal_Not_Available_Should_Return_Unknown()
        // {
        //     // Arrange
        //     var data = new PlayerInfoModel(new CharacterModel {Job = CharacterJobEnum.Wookie});
        //     data.AbilityTracker[AbilityEnum.Heal] = 0;
        //     data.AbilityTracker[AbilityEnum.Bandage] = 0;
        //
        //     data.CurrentHealth = 1;
        //     data.MaxHealth = 100;
        //
        //     // Act
        //     var result = data.SelectHealingAbility();
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.AreEqual(AbilityEnum.Unknown, result);
        // }

        [Test]
        public void PlayerInfoModel_SelectHealingAbility_Jedi_Bandage_Available_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel {Job = CharacterJobEnum.Jedi});
            data.AbilityTracker[AbilityEnum.Bandage] = 1;

            data.CurrentHealth = 1;
            data.MaxHealth = 100;

            // Act
            var result = data.SelectHealingAbility();

            // Reset

            // Assert
            Assert.AreEqual(AbilityEnum.Bandage, result);
        }

        [Test]
        public void PlayerInfoModel_SelectAbilityToUse_Jedi_Available_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel {Job = CharacterJobEnum.Jedi});
            data.AbilityTracker[AbilityEnum.Nimble] = 1;

            // Act
            var result = data.SelectAbilityToUse();

            // Reset

            // Assert
            Assert.AreEqual(AbilityEnum.Nimble, result);
        }

        [Test]
        public void PlayerInfoModel_SelectAbilityToUse_Wookie_Available_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel {Job = CharacterJobEnum.Wookie});
            data.AbilityTracker[AbilityEnum.Quick] = 1;

            // Act
            var unused = data.SelectAbilityToUse();

            // Reset

            // Assert
            // TODO: What is this asserting? lol.
        }

        [Test]
        public void PlayerInfoModel_SelectAbilityToUse_Monster_Should_Return_False()
        {
            // Arrange
            var data = new PlayerInfoModel(new MonsterModel());

            // Act
            var result = data.SelectAbilityToUse();

            // Reset

            // Assert
            Assert.AreEqual(AbilityEnum.Unknown, result);
        }

        // [Test]
        // public void PlayerInfoModel_SelectAbilityToUse_Cleric_Heal_Should_Skip()
        // {
        //     // Arrange
        //     var data = new PlayerInfoModel(new CharacterModel {Job = CharacterJobEnum.Wookie});
        //     data.AbilityTracker[AbilityEnum.Quick] = 0;
        //     data.AbilityTracker[AbilityEnum.Barrier] = 0;
        //     data.AbilityTracker[AbilityEnum.Curse] = 0;
        //
        //     // Act
        //     var result = data.SelectAbilityToUse();
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.AreEqual(AbilityEnum.Unknown, result);
        // }
    }
}