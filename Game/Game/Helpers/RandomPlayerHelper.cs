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
            var result = ItemIndexViewModel.Instance.Dataset.ElementAt(itemIndex).Id;

            return result;
        }

        /// <summary>
        /// Get A Random Difficulty
        /// </summary>
        /// <returns></returns>
        public static DifficultyEnum GetMonsterDifficultyValue()
        {
            var DifficultyList = DifficultyEnumHelper.GetListMonster;

            var RandomDifficulty = DifficultyList.ElementAt(DiceHelper.RollDice(1, DifficultyList.Count()) - 1);

            var result = DifficultyEnumHelper.ConvertStringToEnum(RandomDifficulty);

            return result;
        }

        /// <summary>
        /// Get Random Image
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterImage()
        {
            List<string> FirstNameList = new List<string>
            {
                "item.png",
                "item.png",
                "item.png",
                "item.png",
                "item.png",
                "item.png"
            };

            var result = FirstNameList.ElementAt(DiceHelper.RollDice(1, FirstNameList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get Random Image
        /// </summary>
        /// <returns></returns>
        public static string GetCharacterImage()
        {
            List<string> FirstNameList = new List<string>
            {
                "item.png",
                "item.png",
                "item.png",
                "item.png",
                "item.png",
                "item.png",
                "item.png"
            };

            var result = FirstNameList.ElementAt(DiceHelper.RollDice(1, FirstNameList.Count()) - 1);

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
            List<string> FirstNameList = new List<string>
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

            var result = FirstNameList.ElementAt(DiceHelper.RollDice(1, FirstNameList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get Description
        ///
        /// Return a random description
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterDescription()
        {
            List<string> StringList = new List<string>
            {
                "Hates Hobbits",
                "The son of Dvelyn",
                "Hides in shadows",
                "One evil monster"
            };

            var result = StringList.ElementAt(DiceHelper.RollDice(1, StringList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get Monster Battle Location
        ///
        /// Return a random BattleLocation
        /// </summary>
        /// <returns></returns>
        private static BattleLocationEnum GetMonsterBattleLocation()
        {
            var BattleLocationList = BattleLocationEnumHelper.GetListBattleLocations;

            var RandomBattleLocation =
                BattleLocationList.ElementAt(DiceHelper.RollDice(1, BattleLocationList.Count()) - 1);

            var result = BattleLocationEnumHelper.ConvertStringToEnum(RandomBattleLocation);

            return result;
        }

        /// <summary>
        /// Get Name
        ///
        /// Return a Random Name
        /// </summary>
        /// <returns></returns>
        public static string GetCharacterName()
        {
            List<string> FirstNameList = new List<string>
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

            var result = FirstNameList.ElementAt(DiceHelper.RollDice(1, FirstNameList.Count()) - 1);

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
            List<string> StringList = new List<string>
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

            var result = StringList.ElementAt(DiceHelper.RollDice(1, StringList.Count()) - 1);

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
        /// Get a Random Level
        /// </summary>
        /// <returns></returns>
        public static int GetLevel() =>
            // 1-20
            DiceHelper.RollDice(1, 20);

        /// <summary>
        /// Get a Random Item for the Location
        ///
        /// Return the String for the ID
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GetItem(ItemLocationEnum location)
        {
            var ItemList = ItemIndexViewModel.Instance.GetLocationItems(location);
            if (ItemList.Count == 0)
            {
                return null;
            }

            // Add None to the list
            ItemList.Add(new ItemModel {Id = null, Name = "None"});

            var result = ItemList.ElementAt(DiceHelper.RollDice(1, ItemList.Count()) - 1).Id;
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
                Name = GetCharacterName(),
                Description = GetCharacterDescription(),

                // Randomize the Attributes
                Attack = GetAbilityValue(),
                Speed = GetAbilityValue(),
                Defense = GetAbilityValue(),

                // Randomize an Item for Location
                Head = GetItem(ItemLocationEnum.Head),
                Necklace = GetItem(ItemLocationEnum.Necklace),
                PrimaryHand = GetItem(ItemLocationEnum.PrimaryHand),
                OffHand = GetItem(ItemLocationEnum.OffHand),
                RightFinger = GetItem(ItemLocationEnum.Finger),
                LeftFinger = GetItem(ItemLocationEnum.Finger),
                Feet = GetItem(ItemLocationEnum.Feet),
                ImageURI = GetCharacterImage(),
                MaxHealth = DiceHelper.RollDice(MaxLevel, 10)
            };


            // Level up to the new level
            result.LevelUpToValue(result.Level);

            // Enter Battle at full health
            result.CurrentHealth = result.MaxHealth;

            return result;
        }

        /// <summary>
        /// Create Random Monster for the battle
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
                ImageURI = GetMonsterImage(),
                BattleLocation = GetMonsterBattleLocation(),
                Difficulty = GetMonsterDifficultyValue()
            };

            // Adjust values based on Difficulty
            result.Attack = result.Difficulty.ToModifier(result.Attack);
            result.Defense = result.Difficulty.ToModifier(result.Defense);
            result.Speed = result.Difficulty.ToModifier(result.Speed);
            result.Level = result.Difficulty.ToModifier(result.Level);

            // Get the new Max Health
            result.MaxHealth = DiceHelper.RollDice(result.Level, 10);

            // Adjust the health, If the new Max Health is above the rule for the level, use the original
            var MaxHealthAdjusted = result.Difficulty.ToModifier(result.MaxHealth);
            if (MaxHealthAdjusted < result.Level * 10)
            {
                result.MaxHealth = MaxHealthAdjusted;
            }

            // Level up to the new level
            result.LevelUpToValue(result.Level);

            // Set ExperienceRemaining so Monsters can both use this method
            result.ExperienceRemaining = LevelTableHelper.LevelDetailsList[result.Level + 1].Experience;

            // Enter Battle at full health
            result.CurrentHealth = result.MaxHealth;

            // Monsters can have weapons too....
            if (!items)
            {
                return result;
            }

            result.Head = GetItem(ItemLocationEnum.Head);
            result.Necklace = GetItem(ItemLocationEnum.Necklace);
            result.PrimaryHand = GetItem(ItemLocationEnum.PrimaryHand);
            result.OffHand = GetItem(ItemLocationEnum.OffHand);
            result.RightFinger = GetItem(ItemLocationEnum.Finger);
            result.LeftFinger = GetItem(ItemLocationEnum.Finger);
            result.Feet = GetItem(ItemLocationEnum.Feet);

            return result;
        }
    }
}