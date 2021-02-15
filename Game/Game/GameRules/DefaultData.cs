using System.Collections.Generic;

using Game.Models;
using Game.Models.Enums;
using Game.ViewModels;

namespace Game.GameRules
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel
                {
                    Name = "Lembas Bread",
                    Description = "A fresh loaf of Elven bread",
                    ImageURI = "item.png",
                    Range = 0,
                    Damage = 5,
                    Value = 3,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense
                },
                new ItemModel
                {
                    Name = "Hobbit feet",
                    Description = "Nimble feet to carry you from danger",
                    ImageURI = "item.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed
                },
                new ItemModel
                {
                    Name = "Ring of Power",
                    Description = "A golden ring that turns its wearer invisible",
                    ImageURI = "item.png",
                    Range = 0,
                    Damage = 0,
                    Value = 8,
                    Location = ItemLocationEnum.RightFinger,
                    Attribute = AttributeEnum.Speed
                },
                new ItemModel
                {
                    Name = "Troll Axe",
                    Description = "A heavy iron axe",
                    ImageURI = "item.png",
                    Range = 5,
                    Damage = 11,
                    Value = 7,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack
                },
            };

            return datalist;
        }

        /// <summary>
        /// Load Example Scores
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<ScoreModel> LoadData(ScoreModel temp)
        {
            var datalist = new List<ScoreModel>()
            {
                new ScoreModel
                {
                    Name = "First Score", Description = "Test Data",
                },
                new ScoreModel
                {
                    Name = "Second Score", Description = "Test Data",
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<CharacterModel> LoadData(CharacterModel temp)
        {
            var HeadString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);
            var NecklaceString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Necklace);
            var PrimaryHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
            var OffHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.OffHand);
            var FeetString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Feet);
            var RightFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
            var LeftFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);

            var datalist = new List<CharacterModel>()
            {
                new CharacterModel()
                {
                    Name = "Luke Skywalker",
                    Description = "A Jedi master from Tatooine",
                    Job = CharacterJobEnum.Jedi,
                    ImageURI = "jedi_icon.png",
                    Level = 7,
                    MaxHealth = 8,
                    Attack = 5,
                    Defense = 3,
                    Speed = 4
                },
                new CharacterModel()
                {
                    Name = "Chewbacca",
                    Description = "A Wookie warrior from Kashyyyk",
                    Job = CharacterJobEnum.Wookie,
                    ImageURI = "wookie_icon.png",
                    Level = 6,
                    MaxHealth = 7,
                    Attack = 6,
                    Defense = 5,
                    Speed = 4
                },
                new CharacterModel()
                {
                    Name = "C3PO",
                    Description = "Fluent in over 6 million forms of communication",
                    Job = CharacterJobEnum.ProtocolDroid,
                    ImageURI = "protocol_droid_icon.png",
                    Level = 4,
                    MaxHealth = 2,
                    Attack = 2,
                    Defense = 1,
                    Speed = 1
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load Monsters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<MonsterModel> LoadData(MonsterModel temp)
        {
            var datalist = new List<MonsterModel>()
            {
                new MonsterModel
                {
                    Name = "Bogbi",
                    Description = "Hates Hobbits",
                    ImageURI = "item.png",
                },
                new MonsterModel
                {
                    Name = "Srauguc",
                    Description = "One evil monster",
                    ImageURI = "item.png",
                },
                new MonsterModel
                {
                    Name = "Shelob",
                    Description = "Hides in shadows",
                    ImageURI = "item.png",
                },
            };

            return datalist;
        }
    }
}