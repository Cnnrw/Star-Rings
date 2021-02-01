using System.Collections.Generic;

using Game.Models;
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
                new ItemModel {
                    Name = "I1",
                    Description = "I1",
                    ImageURI = "item.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "I2",
                    Description = "I2",
                    ImageURI = "item.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "I3",
                    Description = "I3",
                    ImageURI = "item.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "I4",
                    Description = "I4",
                    ImageURI = "item.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "I5",
                    Description = "I5",
                    ImageURI = "item.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "I6",
                    Description = "I6",
                    ImageURI = "item.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Attack
                },
            };

            for (int i = 0; i < 20; i++)
            {
                var item = new ItemModel
                {
                    ImageURI = "item.png",
                    Range = 2,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack
                };
                item.Name = "I" + (datalist.Count+1).ToString();
                item.Description = item.Name;

                datalist.Add(item);
            }

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
                new ScoreModel {
                    Name = "First Score",
                    Description = "Test Data",
                },

                new ScoreModel {
                    Name = "Second Score",
                    Description = "Test Data",
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
        var NecklassString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Necklass);
        var PrimaryHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
        var OffHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.OffHand);
        var FeetString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Feet);
        var RightFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
        var LeftFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);

        var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "Luke Skywalker",
                    Description = "A young Jedi Master",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "item.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Leia Organa",
                    Description = "Leader of the Rebel Alliance",
                    Level = 1,
                    MaxHealth = 4,
                    ImageURI = "item.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Han Solo",
                    Description = "Co-pilot of the Millenium Falcon",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "item.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Chewbacca",
                    Description = "Wookie co-pilot of the Millenium Falcon",
                    Level = 1,
                    MaxHealth = 6,
                    ImageURI = "item.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "C3PO",
                    Description = "A humanoid robot fluent in over 6 million languages",
                    Level = 1,
                    MaxHealth = 3,
                    ImageURI = "item.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString, 
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "R2D2",
                    Description = "An astromech droid manufactured by Industrial Automation",
                    Level = 1,
                    MaxHealth = 4,
                    ImageURI = "item.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },
            };

        return datalist;
    }

    /// <summary>
    /// Load Characters
    /// </summary>
    /// <param name="temp"></param>
    /// <returns></returns>
    public static List<MonsterModel> LoadData(MonsterModel temp)
    {
        var datalist = new List<MonsterModel>()
            {
                new MonsterModel {
                    Name = "M1",
                    Description = "M1",
                    ImageURI = "item.png",
                },

                new MonsterModel {
                    Name = "M2",
                    Description = "M2",
                    ImageURI = "item.png",
                },

                new MonsterModel {
                    Name = "M3",
                    Description = "M3",
                    ImageURI = "item.png",
                },

                new MonsterModel {
                    Name = "M4",
                    Description = "M4",
                    ImageURI = "item.png",
                },

                new MonsterModel {
                    Name = "M5",
                    Description = "M5",
                    ImageURI = "item.png",
                },

                new MonsterModel {
                    Name = "M6",
                    Description = "M6",
                    ImageURI = "item.png",
                },

                new MonsterModel {
                    Name = "M7",
                    Description = "M7",
                    ImageURI = "item.png",
                },
            };

        return datalist;
    }
}
}