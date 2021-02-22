using System.Collections.Generic;

using Game.Enums;
using Game.Models;

namespace Game.Helpers
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
                    ImageURI = "lembas.png",
                    Range = 0,
                    Damage = 5,
                    Value = 3,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense,
                    Category = ItemCategories.Food,
                    IsConsumable = true
                },
                new ItemModel
                {
                    Name = "Hobbit feet",
                    Description = "Nimble feet to carry you from danger",
                    ImageURI = "hobbit_foot.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed,
                    Category = ItemCategories.Armour,
                    IsConsumable = false
                },
                new ItemModel
                {
                    Name = "Gimli's Axe",
                    Description = "A heavy iron axe",
                    ImageURI = "gimlis_axe.png",
                    Range = 5,
                    Damage = 11,
                    Value = 7,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack,
                    Category = ItemCategories.Weapon,
                    IsConsumable = false
                },
                new ItemModel
                {
                    Name = "Gandalf's Fireworks",
                    Description = "Bright sparks which dazzle your foe",
                    ImageURI = "fireworks.png",
                    Range = 7,
                    Damage = 1,
                    Value = 3,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Speed,
                    Category = ItemCategories.AreaEffect,
                    IsConsumable = true
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
            var datalist = new List<CharacterModel>()
            {
                new CharacterModel()
                {
                    Name = "Obi-Wan Kenobi",
                    Description = "A Jedi master and teacher of Anakin",
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
                    Name = "Keshmya Jungdasa",
                    Description = "Leader of the Rebel unit",
                    Job = CharacterJobEnum.Princess,
                    ImageURI = "jedi_princess_icon.png",
                    Level = 3,
                    MaxHealth = 7,
                    Attack = 6,
                    Defense = 3,
                    Speed = 5
                },
                new CharacterModel()
                {
                    Name = "Jebusa Fivilar",
                    Description = "Pilot of the ship",
                    Job = CharacterJobEnum.Smuggler,
                    ImageURI = "smuggler_icon.png",
                    Level = 2,
                    MaxHealth = 4,
                    Attack = 6,
                    Defense = 2,
                    Speed = 2
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
                    Name = "C5PQ",
                    Description = "Fluent in over 6 million forms of communication",
                    Job = CharacterJobEnum.ProtocolDroid,
                    ImageURI = "protocol_droid_icon.png",
                    Level = 4,
                    MaxHealth = 2,
                    Attack = 2,
                    Defense = 1,
                    Speed = 1
                },
                new CharacterModel()
                {
                    Name = "R3D3",
                    Description = "A clunky Astromech",
                    Job = CharacterJobEnum.AstroDroid,
                    ImageURI = "astromech_icon.png",
                    Level = 4,
                    MaxHealth = 2,
                    Attack = 2,
                    Defense = 1,
                    Speed = 1
                },
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