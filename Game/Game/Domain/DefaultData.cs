using System.Collections.Generic;

using Game.Enums;

namespace Game.Models
{
    public static class DefaultData
    {
        /// <summary>
        /// Default Item Models
        /// </summary>
        public static readonly IEnumerable<ItemModel> Items = new[]
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
            }
        };

        /// <summary>
        /// Default characters
        /// </summary>
        public static readonly IEnumerable<CharacterModel> Characters = new[]
        {
            new CharacterModel
            {
                Name = "Obi-Wan Kenobi",
                Description = "A Jedi master and teacher of Anakin",
                Job = CharacterJobEnum.Jedi,
                ImageURI = "jedi.png",
                IconImageURI = "jedi_icon.png",
                Level = 7,
                MaxHealth = 8,
                Attack = 5,
                Defense = 3,
                Speed = 4
            },
            new CharacterModel
            {
                Name = "Keshmya Jungdasa",
                Description = "Leader of the Rebel unit",
                Job = CharacterJobEnum.Princess,
                ImageURI = "jedi_princess.png",
                IconImageURI = "jedi_princess_icon.png",
                Level = 3,
                MaxHealth = 7,
                Attack = 6,
                Defense = 3,
                Speed = 5
            },
            new CharacterModel
            {
                Name = "Jebusa Fivilar",
                Description = "Pilot of the ship",
                Job = CharacterJobEnum.Smuggler,
                ImageURI = "smuggler.png",
                IconImageURI = "smuggler_icon.png",
                Level = 2,
                MaxHealth = 4,
                Attack = 6,
                Defense = 2,
                Speed = 2
            },
            new CharacterModel
            {
                Name = "Chewbacca",
                Description = "A Wookie warrior from Kashyyyk",
                Job = CharacterJobEnum.Wookie,
                ImageURI = "wookie.png",
                IconImageURI = "wookie_icon.png",
                Level = 6,
                MaxHealth = 7,
                Attack = 6,
                Defense = 5,
                Speed = 4
            },
            new CharacterModel
            {
                Name = "C5PQ",
                Description = "Fluent in over 6 million forms of communication",
                Job = CharacterJobEnum.ProtocolDroid,
                ImageURI = "protocol_droid.png",
                IconImageURI = "protocol_droid_icon.png",
                Level = 4,
                MaxHealth = 2,
                Attack = 2,
                Defense = 1,
                Speed = 1
            },
            new CharacterModel
            {
                Name = "R3D3",
                Description = "A clunky Astromech",
                ImageURI = "astromech.png",
                IconImageURI = "astromech_icon.png",
                Job = CharacterJobEnum.AstroDroid,
                Level = 4,
                MaxHealth = 2,
                Attack = 2,
                Defense = 1,
                Speed = 1
            }
        };

        /// <summary>
        /// Default Monsters
        /// </summary>
        public static readonly IEnumerable<MonsterModel> Monsters = new[]
        {
            new MonsterModel
            {
                Name = "Dark Elf",
                Description = "",
                ImageURI = "dark_elf.png",
                IconImageURI = "dark_elf_icon.png",
                UniqueItem = string.Empty,
                BattleLocations = BattleLocationEnum.ElvenCity,
                Level = 0,
                Difficulty = DifficultyEnum.Hard,
                MaxHealth = 0,
                Speed = 0,
                Defense = 0,
                Attack = 0,
                Range = 0,
            },
            new MonsterModel
            {
                Name = "Dead King",
                Description = "",
                ImageURI = "dead_king.png",
                IconImageURI = "dead_king_icon.png",
                UniqueItem = string.Empty,
                BattleLocations = BattleLocationEnum.Dungeons &
                                  BattleLocationEnum.Mordor,
                Level = 0,
                Difficulty = DifficultyEnum.Hard,
                MaxHealth = 0,
                Speed = 0,
                Defense = 0,
                Attack = 0,
                Range = 0,
            },
            new MonsterModel
            {
                Name = "Nazgul",
                Description = "",
                ImageURI = "nazgul.png",
                IconImageURI = "nazgul_icon.png",
                UniqueItem = string.Empty,
                BattleLocations = BattleLocationEnum.Mordor &
                                  BattleLocationEnum.ElvenCity &
                                  BattleLocationEnum.Shire,
                Level = 0,
                Difficulty = DifficultyEnum.Hard,
                MaxHealth = 0,
                Speed = 0,
                Defense = 0,
                Attack = 0,
                Range = 0,
            },
            new MonsterModel
            {
                Name = "Oliphant",
                Description = "",
                ImageURI = "oliphant.png",
                IconImageURI = "oliphant_icon.png",
                UniqueItem = string.Empty,
                BattleLocations = BattleLocationEnum.Mordor,
                Level = 0,
                Difficulty = DifficultyEnum.Hard,
                MaxHealth = 0,
                Speed = 0,
                Defense = 0,
                Attack = 0,
                Range = 0,
            },
            new MonsterModel
            {
                Name = "Orc",
                Description = "",
                ImageURI = "orc.png",
                IconImageURI = "orc_icon.png",
                UniqueItem = string.Empty,
                BattleLocations = BattleLocationEnum.Mordor &
                                  BattleLocationEnum.Dungeons,
                Level = 0,
                Difficulty = DifficultyEnum.Hard,
                MaxHealth = 0,
                Speed = 0,
                Defense = 0,
                Attack = 0,
                Range = 0,
            },
            new MonsterModel
            {
                Name = "Smeagol",
                Description = "",
                ImageURI = "smeagol.png",
                IconImageURI = "smeagol_icon.png",
                UniqueItem = string.Empty,
                BattleLocations = BattleLocationEnum.Forest &
                                  BattleLocationEnum.Dungeons &
                                  BattleLocationEnum.Mordor &
                                  BattleLocationEnum.Shire &
                                  BattleLocationEnum.ElvenCity,
                Level = 0,
                Difficulty = DifficultyEnum.Hard,
                MaxHealth = 0,
                Speed = 0,
                Defense = 0,
                Attack = 0,
                Range = 0,
            },
            new MonsterModel
            {
                Name = "Spider",
                Description = "",
                ImageURI = "spider.png",
                IconImageURI = "spider_icon.png",
                UniqueItem = string.Empty,
                BattleLocations = BattleLocationEnum.Dungeons &
                                  BattleLocationEnum.Forest &
                                  BattleLocationEnum.ElvenCity,
                Level = 0,
                Difficulty = DifficultyEnum.Hard,
                MaxHealth = 0,
                Speed = 0,
                Defense = 0,
                Attack = 0,
                Range = 0,
            },
            new MonsterModel
            {
                Name = "Mountain Troll",
                Description = "",
                ImageURI = "troll.png",
                IconImageURI = "troll_icon.png",
                UniqueItem = string.Empty,
                BattleLocations = BattleLocationEnum.Dungeons &
                                  BattleLocationEnum.Forest &
                                  BattleLocationEnum.Mordor,
                Level = 0,
                Difficulty = DifficultyEnum.Hard,
                MaxHealth = 0,
                Speed = 0,
                Defense = 0,
                Attack = 0,
                Range = 0,
            },
            new MonsterModel
            {
                Name = "Warg Rider",
                Description = "",
                ImageURI = "warg_rider.png",
                IconImageURI = "warg_rider_icon.png",
                UniqueItem = string.Empty,
                BattleLocations = BattleLocationEnum.Forest &
                                  BattleLocationEnum.Mordor,
                Level = 0,
                Difficulty = DifficultyEnum.Hard,
                MaxHealth = 0,
                Speed = 0,
                Defense = 0,
                Attack = 0,
                Range = 0,
            }
        };

        /// <summary>
        /// Default Scores
        /// </summary>
        public static readonly IEnumerable<ScoreModel> Scores = new[]
        {
            new ScoreModel
            {
                Name = "Score 1",
                TurnCount = 12,
                RoundCount = 3,
                ExperienceGainedTotal = 1205,
                MonsterSlainNumber = 9
            },
            new ScoreModel
            {
                Name = "Score 2",
                TurnCount = 23,
                RoundCount = 7,
                ExperienceGainedTotal = 8420,
                MonsterSlainNumber = 17
            },
        };
    }
}