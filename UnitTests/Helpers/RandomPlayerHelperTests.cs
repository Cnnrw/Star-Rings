using System.Linq;

using Game.Enums;
using Game.Helpers;
using Game.ViewModels;

using NUnit.Framework;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class RandomPlayerHelperTests
    {
        [Test]
        public void RandomPlayerHelper_GetAbilityValue_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetAbilityValue();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(2 - 1, result);
        }

        [Test]
        public void RandomPlayerHelper_GetLevel_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetLevel();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void RandomPlayerHelper_GetHealth_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetHealth(1);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterName_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetMonsterName();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("Suzbul", result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterDescription_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetMonsterDescription();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("The son of Dvelyn", result);
        }

        [Test]
        public void RandomPlayerHelper_GetCharacterDescription_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetCharacterDescription();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("the awesome", result);
        }

        [Test]
        public void RandomPlayerHelper_GetCharacterName_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetCharacterName();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("Doug", result);
        }

        [Test]
        public void RandomPlayerHelper_GetItem_Unknown_Should_Return_0()
        {
            // Arrange

            // Act
            var result = RandomPlayerHelper.GetItem(ItemLocationEnum.Unknown);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void RandomPlayerHelper_GetItem_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetItem(ItemLocationEnum.Feet);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreNotEqual(null, result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterDifficultyValue_Should_Pass()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetMonsterDifficultyValue();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(DifficultyEnum.Average, result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterImage_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetMonsterImage();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(MonsterImageEnum.DeadKing, result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterImage_99_Should_Return_DarkElf()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(99);

            // Act
            var result = RandomPlayerHelper.GetMonsterImage();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(MonsterImageEnum.DarkElf, result);
        }

        [Test]
        public void RandomPlayerHelper_GetCharacterImage_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetCharacterImage();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(CharacterImageEnum.jedi, result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterUniqueItem_2_Should_Return_2()
        {
            // Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(2);

            var expected = ItemIndexViewModel.Instance.Dataset.ElementAt(1).Id;

            // Act
            var result = RandomPlayerHelper.GetMonsterUniqueItem();

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(expected, result);
        }

        //[Test]
        //public void RandomPlayerHelper_GetRandomCharacter_InValid_Empty_CharacterList_Should_Return_New()
        //{
        //    // Arrange
        //    CharacterIndexViewModel.Instance.Dataset.Clear();

        //    // Act
        //    var result = RandomPlayerHelper.GetRandomCharacter(1);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(true, result.Name.Contains("Elf"));
        //}

        //[Test]
        //public async Task RandomPlayerHelper_GetRandomCharacter_Valid_CharacterList_1_Should_Return_1()
        //{
        //    // Arrange
        //    CharacterIndexViewModel.Instance.Dataset.Clear();
        //    await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "1" });

        //    // Arrange
        //    DiceHelper.EnableForcedRolls();
        //    DiceHelper.SetForcedRollValue(1);

        //    // Act
        //    var result = RandomPlayerHelper.GetRandomCharacter(1);

        //    // Reset
        //    DiceHelper.DisableForcedRolls();

        //    // Assert
        //    Assert.AreEqual(true, result.UniqueItem.Equals("1"));
        //}

        //[Test]
        //public async Task RandomPlayerHelper_GetRandomCharacter_Valid_CharacterList_3_Should_Return_2()
        //{
        //    // Arrange
        //    CharacterIndexViewModel.Instance.Dataset.Clear();
        //    await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "1" });
        //    await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "2" });
        //    await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "3" });

        //    DiceHelper.EnableForcedRolls();
        //    DiceHelper.SetForcedRollValue(2);

        //    // Act
        //    var result = RandomPlayerHelper.GetRandomCharacter(1);

        //    // Reset
        //    DiceHelper.DisableForcedRolls();

        //    // Assert
        //    Assert.AreEqual(true, result.UniqueItem.Equals("2"));
        //}

        //[Test]
        //public async Task RandomPlayerHelper_GetRandomCharacter_Valid_Health_Should_Be_Correct()
        //{
        //    // Arrange
        //    CharacterIndexViewModel.Instance.Dataset.Clear();
        //    await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "1" });
        //    await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "2" });
        //    await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "3" });

        //    DiceHelper.EnableForcedRolls();
        //    DiceHelper.SetForcedRollValue(2);

        //    // Act
        //    var result = RandomPlayerHelper.GetRandomCharacter(1);

        //    // Reset
        //    DiceHelper.DisableForcedRolls();

        //    // Assert
        //    Assert.AreEqual(result.MaxHealth, result.CurrentHealth);
        //}

        [Test]
        public void RandomPlayerHelper_GetRandomMonster_InValid_Empty_List_Should_Return_New_Monster()
        {
            // Arrange
            var save = MonsterIndexViewModel.Instance.Dataset;
            MonsterIndexViewModel.Instance.Dataset.Clear();

            // Act
            var result = RandomPlayerHelper.GetRandomMonster(1);

            // Reset
            MonsterIndexViewModel.Instance.Dataset = save;

            // Assert
            Assert.AreEqual(PlayerTypeEnum.Monster, result.PlayerType);
        }

        [Test]
        public void RandomPlayerHelper_GetRandomMonster_Valid_Should_Return_New_Monster()
        {
            // Arrange

            // Act
            var result = RandomPlayerHelper.GetRandomMonster(1);

            // Reset

            // Assert
            Assert.AreEqual(PlayerTypeEnum.Monster, result.PlayerType);
        }

        [Test]
        public void RandomPlayerHelper_GetRandomMonster_Valid_Items_True_Should_Return_New_Monster()
        {
            // Arrange

            // Act
            var result = RandomPlayerHelper.GetRandomMonster(1, true);

            // Reset

            // Assert
            Assert.AreEqual(PlayerTypeEnum.Monster, result.PlayerType);
        }

        [Test]
        public void RandomPlayerHelper_GetRandomMonster_Valid_Items_False_Should_Return_New_Monster()
        {
            // Arrange

            // Act
            var result = RandomPlayerHelper.GetRandomMonster(1);

            // Reset

            // Assert
            Assert.AreEqual(PlayerTypeEnum.Monster, result.PlayerType);
        }
    }
}
