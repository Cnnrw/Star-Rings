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
                Description = "An elf, but like...an evil one.",
                ImageURI = "dark_elf.png",
                IconImageURI = "dark_elf_icon.png",
                UniqueItem = string.Empty,
                BattleLocation = BattleLocationEnum.ElvenCity,
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
                Description = "A ghastly apparition.",
                ImageURI = "dead_king.png",
                IconImageURI = "dead_king_icon.png",
                UniqueItem = string.Empty,
                BattleLocation = BattleLocationEnum.ElvenCity,
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
                Name = "Khamûl",
                Description = "The Easterling Nazgul. A ring-servant of Sauron.",
                ImageURI = "nazgul.png",
                IconImageURI = "nazgul_icon.png",
                UniqueItem = string.Empty,
                BattleLocation = BattleLocationEnum.Shire,
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
                Name = "Witch-King of Angmar",
                Description = "The leader of the Nazgul.",
                ImageURI = "nazgul.png",
                IconImageURI = "nazgul_icon.png",
                UniqueItem = string.Empty,
                BattleLocation = BattleLocationEnum.Mordor,
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
                Description = "A colossal, elephant-like beast.",
                ImageURI = "oliphant.png",
                IconImageURI = "oliphant_icon.png",
                UniqueItem = string.Empty,
                BattleLocation = BattleLocationEnum.Mordor,
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
                Description = "A standard minion of Sauron.",
                ImageURI = "orc.png",
                IconImageURI = "orc_icon.png",
                UniqueItem = string.Empty,
                BattleLocation = BattleLocationEnum.Mordor,
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
                Name = "Lost Orc",
                Description = "An orc who got separated from his group and wandered into the Shire by accident.",
                ImageURI = "orc.png",
                IconImageURI = "orc_icon.png",
                UniqueItem = string.Empty,
                BattleLocation = BattleLocationEnum.Shire,
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
                Description = "A Hobbit that was transformed by the power of the One Ring into a vile, cave-dwelling creature.",
                ImageURI = "smeagol.png",
                IconImageURI = "smeagol_icon.png",
                UniqueItem = string.Empty,
                BattleLocation = BattleLocationEnum.Dungeons,
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
                Name = "Tree Spider",
                Description = "A giant spider that spins its webs in the forest of Mirkwood.",
                ImageURI = "spider.png",
                IconImageURI = "spider_icon.png",
                UniqueItem = string.Empty,
                BattleLocation = BattleLocationEnum.Forest,
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
                Description = "A big, lumbering oaf that roams the Misty Mountains.",
                ImageURI = "troll.png",
                IconImageURI = "troll_icon.png",
                UniqueItem = string.Empty,
                BattleLocation = BattleLocationEnum.Dungeons,
                Level = 0,
                Difficulty = DifficultyEnum.Average,
                MaxHealth = 0,
                Speed = 0,
                Defense = 0,
                Attack = 0,
                Range = 0,
            },
            new MonsterModel
            {
                Name = "Gutiug",
                Description = "A warg rider roaming the outskirts of the Shire.",
                ImageURI = "warg_rider.png",
                IconImageURI = "warg_rider_icon.png",
                UniqueItem = string.Empty,
                BattleLocation = BattleLocationEnum.Shire,
                Level = 0,
                Difficulty = DifficultyEnum.Easy,
                MaxHealth = 0,
                Speed = 0,
                Defense = 0,
                Attack = 0,
                Range = 0,
            }
            new MonsterModel
            {
                Name = "Kruug",
                Description = "A warg rider roaming the forests.",
                ImageURI = "warg_rider.png",
                IconImageURI = "warg_rider_icon.png",
                UniqueItem = string.Empty,
                BattleLocation = BattleLocationEnum.Forest,
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
