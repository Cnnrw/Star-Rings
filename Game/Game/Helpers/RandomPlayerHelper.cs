using System;
using System.Collections.Generic;
using System.Linq;

using Game.Enums;
using Game.Models;
using Game.ViewModels;

namespace Game.Helpers
{
    public static class RandomPlayerHelper
    {
        /// <summary>
        /// Get Health
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int GetHealth(int level) =>
            // Roll the Dice and reset the Health
            DiceHelper.RollDice(level, 10);

        /// <summary>
        /// Get A Random Difficulty
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterUniqueItem()
        {
            var itemIndex = DiceHelper.RollDice(1, ItemIndexViewModel.Instance.Dataset.Count()) - 1;

            // Check to see if there are enough items, if not, then just use the first one...
            var result = ItemIndexViewModel.Instance.Dataset.First().Id;

            if (itemIndex < ItemIndexViewModel.Instance.Dataset.Count)
            {
                result = ItemIndexViewModel.Instance.Dataset.ElementAt(itemIndex).Id;
            }
            return result;
        }

        /// <summary>
        /// Get A Random Difficulty
        /// </summary>
        /// <returns></returns>
        public static DifficultyEnum GetMonsterDifficultyValue()
        {
            var difficultyList = DifficultyEnumHelper.GetListMonster;

            var randomDifficulty = difficultyList.ElementAt(DiceHelper.RollDice(1, difficultyList.Count()) - 1);

            var result = DifficultyEnumHelper.ConvertStringToEnum(randomDifficulty);

            return result;
        }

        /// <summary>
        /// Get Random Image
        /// </summary>
        /// <returns></returns>
        public static MonsterImageEnum GetMonsterImage()
        {
            var imageCount = Enum.GetNames(typeof(MonsterImageEnum)).Length;
            var index = DiceHelper.RollDice(1, imageCount) - 1;

            return index < imageCount
                       ? (MonsterImageEnum)index
                       : (MonsterImageEnum)1;
        }

        /// <summary>
        /// Get Random Image
        /// </summary>
        /// <returns></returns>
        public static string GetCharacterImage()
        {
            var stringList = new List<string>
            {
                "item.png",
                "item.png",
                "item.png",
                "item.png",
                "item.png",
                "item.png",
                "item.png"
            };

            var index = DiceHelper.RollDice(1, stringList.Count()) - 1;

            var result = stringList.First();

            if (index < stringList.Count)
            {
                result = stringList.ElementAt(index);
            }

            return result;
        }

        /// <summary>
        /// Get Name
        ///
        /// Return a Random Name
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterName()
        {
            var firstNameList = new List<string>
            {
                "Bogbi",
                "Suzbul",
                "Srauguc",
                "Macreg",
                "Briglath",
                "Shelob",
                "Smegeul",
                "Ofdosh",
                "Ogdod",
                "Aushnosh",
                "Aurzur"
            };

            return firstNameList.ElementAt(DiceHelper.RollDice(1, firstNameList.Count) - 1);
        }

        /// <summary>
        /// Get Description
        ///
        /// Return a random description
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterDescription()
        {
            var stringList = new List<string>
            {
                "Hates Hobbits",
                "The son of Dvelyn",
                "Hides in shadows",
                "One evil monster"
            };

            return stringList.ElementAt(DiceHelper.RollDice(1, stringList.Count) - 1);
        }

        /// <summary>
        /// Get Name
        ///
        /// Return a Random Name
        /// </summary>
        /// <returns></returns>
        public static string GetCharacterName()
        {
            var stringList = new List<string>
            {
                "Mike",
                "Doug",
                "Jea",
                "Sue",
                "Tim",
                "Daren",
                "Dani",
                "Mami",
                "Mari",
                "Ryu",
                "Hucky",
                "Peanut",
                "Sumi",
                "Apple",
                "Ami",
                "Honami",
                "Sonomi",
                "Pat",
                "Sakue",
                "Isamu"
            };

            var index = DiceHelper.RollDice(1, stringList.Count()) - 1;

            var result = stringList.First();

            if (index < stringList.Count)
            {
                result = stringList.ElementAt(index);
            }

            return result;
        }

        /// <summary>
        /// Get Description
        ///
        /// Return a random description
        /// </summary>
        /// <returns></returns>
        public static string GetCharacterDescription()
        {
            var stringList = new List<string>
            {
                "the terrible",
                "the awesome",
                "the lost",
                "the old",
                "the younger",
                "the quiet",
                "the loud",
                "the helpless",
                "the happy",
                "the sleepy",
                "the angry",
                "the clever"
            };

            var index = DiceHelper.RollDice(1, stringList.Count()) - 1;

            var result = stringList.First();

            if (index < stringList.Count)
            {
                result = stringList.ElementAt(index);
            }

            return result;
        }

        /// <summary>
        /// Get Random Ability Number
        /// </summary>
        /// <returns></returns>
        public static int GetAbilityValue() =>
            // 0 to 9, not 1-10
            DiceHelper.RollDice(1, 10) - 1;

        /// <summary>
        /// Get a Random Level between 1 and 20
        /// </summary>
        /// <returns></returns>
        public static int GetLevel() =>
            DiceHelper.RollDice(1, 20);

        /// <summary>
        /// Get a Random Item for the Location
        /// Return the String for the ID
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GetItem(ItemLocationEnum location)
        {
            var itemList = ItemIndexViewModel.Instance.GetLocationItems(location);
            if (itemList.Count == 0)
                return null;

            // Add None to the list
            itemList.Add(new ItemModel {Id = null, Name = "None"});

            var result = itemList.First().Id;

            var index = DiceHelper.RollDice(1, itemList.Count()) - 1;
            if (index < itemList.Count)
                result = itemList.ElementAt(index).Id;

            return result;
        }

        /// <summary>
        /// Create Random Character for the battle
        /// </summary>
        /// <param name="MaxLevel"></param>
        /// <returns></returns>
        public static CharacterModel GetRandomCharacter(int MaxLevel)
        {
            var result = new CharacterModel
            {
                Level = DiceHelper.RollDice(1, MaxLevel),

                // Randomize Name
                Name = GetCharacterName(), Description = GetCharacterDescription(),

                // Randomize the Attributes
                Attack = GetAbilityValue(), Speed = GetAbilityValue(),
                Defense = GetAbilityValue(),

                // Randomize an Item for Location
                Head = GetItem(ItemLocationEnum.Head), Necklace = GetItem(ItemLocationEnum.Necklace),
                PrimaryHand = GetItem(ItemLocationEnum.PrimaryHand), OffHand = GetItem(ItemLocationEnum.OffHand),
                RightFinger = GetItem(ItemLocationEnum.Finger), LeftFinger = GetItem(ItemLocationEnum.Finger),
                Feet = GetItem(ItemLocationEnum.Feet), ImageURI = GetCharacterImage(),
                MaxHealth = DiceHelper.RollDice(MaxLevel, 10)
            };


            // Level up to the new level
            result.LevelUpToValue(result.Level);

            // Enter Battle at full health
            result.CurrentHealth = result.MaxHealth;

            return result;
        }

        /// <summary>
        /// Create Random Character for the battle
        /// </summary>
        /// <param name="maxLevel"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static MonsterModel GetRandomMonster(int maxLevel, bool items = false)
        {
            var result = new MonsterModel()
            {
                Level = DiceHelper.RollDice(1, maxLevel),

                // Randomize Name
                Name = GetMonsterName(),
                Description = GetMonsterDescription(),

                // Randomize the Attributes
                Attack = GetAbilityValue(),
                Speed = GetAbilityValue(),
                Defense = GetAbilityValue(),
                Difficulty = GetMonsterDifficultyValue()
            };

            var monsterImg = GetMonsterImage();
            result.ImageURI = monsterImg.ToImageURI();
            result.IconImageURI = monsterImg.ToIconImageURI();

            // Adjust values based on Difficulty
            result.Attack = result.Difficulty.ToModifier(result.Attack);
            result.Defense = result.Difficulty.ToModifier(result.Defense);
            result.Speed = result.Difficulty.ToModifier(result.Speed);
            result.Level = result.Difficulty.ToModifier(result.Level);

            // Get the new Max Health
            result.MaxHealth = DiceHelper.RollDice(result.Level, 10);

            // Adjust the health, If the new Max Health is above the rule for the level, use the original
            var maxHealthAdjusted = result.Difficulty.ToModifier(result.MaxHealth);
            if (maxHealthAdjusted < result.Level * 10)
                result.MaxHealth = maxHealthAdjusted;

            // Level up to the new level
            result.LevelUpToValue(result.Level);

            // Set ExperienceRemaining so Monsters can both use this method
            result.ExperienceRemaining = LevelTableHelper.LevelDetailsList[result.Level >= 20 ? 20 : result.Level + 1]
                                                         .Experience;

            // Enter Battle at full health
            result.CurrentHealth = result.MaxHealth;

            // Monsters can have weapons too....
            if (items)
            {
                result.Head = GetItem(ItemLocationEnum.Head);
                result.Necklace = GetItem(ItemLocationEnum.Necklace);
                result.PrimaryHand = GetItem(ItemLocationEnum.PrimaryHand);
                result.OffHand = GetItem(ItemLocationEnum.OffHand);
                result.RightFinger = GetItem(ItemLocationEnum.Finger);
                result.LeftFinger = GetItem(ItemLocationEnum.Finger);
                result.Feet = GetItem(ItemLocationEnum.Feet);
            }

            return result;
        }
    }
}
