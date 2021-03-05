using Game.Enums;
using System.Collections.Generic;

namespace Game.Models
{
    /// <summary>
    /// The Monsters in the Game
    /// Derives from BasePlayer Model
    /// </summary>
    public class MonsterModel : BasePlayerModel<MonsterModel>
    {
        #region Monster-specific Attributes

        /// <summary>
        /// The locations where the monster can appear
        /// </summary>
        public BattleLocationEnum BattleLocation { get; set; }

        public static List<string> ImagesURIs = new List<string>
        {
            "dark_elf.png",
            "dead_king.png",
            "nazgul.png",
            "oliphant.png",
            "orc.png",
            "smeagol.png",
            "spider.png",
            "troll.png",
            "warg_rider.png"
        };

        #endregion
        #region Constructors

        /// <summary>
        /// Default constructor for Monster Model. Sets the PlayerType to Monster
        /// and the Guid equal to the ID sent from the server.
        /// </summary>
        public MonsterModel()
        {
            PlayerType = PlayerTypeEnum.Monster;
            Guid = Id;
        }

        /// <summary>
        /// Make a copy
        /// </summary>
        /// <param name="data"></param>
        public MonsterModel(MonsterModel data) => Update(data);

        #endregion
        #region Overrides

        /// <summary>
        /// Update
        /// TODO: Maybe make a copy constructor?
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public sealed override bool Update(MonsterModel newData)
        {
            if (newData == null)
            {
                return false;
            }

            PlayerType = newData.PlayerType;
            Guid = newData.Guid;

            Name = newData.Name;
            Description = newData.Description;


            ImageURI = newData.ImageURI;
            IconImageURI = newData.IconImageURI;
            UniqueItem = newData.UniqueItem;

            Difficulty = newData.Difficulty;

            Level = newData.Level;
            Speed = newData.Speed;
            Defense = newData.Defense;
            Attack = newData.Attack;

            ExperienceTotal = newData.ExperienceTotal;
            ExperienceRemaining = newData.ExperienceRemaining;
            CurrentHealth = newData.CurrentHealth;
            MaxHealth = newData.MaxHealth;

            Head = newData.Head;
            Necklace = newData.Necklace;
            PrimaryHand = newData.PrimaryHand;
            OffHand = newData.OffHand;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;

            BattleLocation = newData.BattleLocation;

            return true;
        }

        /// <summary>
        /// Helper to combine the attributes into a single line, to make it
        /// easier to display the item as a string
        /// </summary>
        /// <returns></returns>
        public override string FormatOutput()
        {
            var myReturn = Name;
            myReturn += " , " + Description;
            myReturn += " , a " + Job.ToMessage();
            myReturn += " , Level : " + Level.ToString();
            myReturn += " , Difficulty : " + Difficulty.ToString();
            myReturn += " , Total Experience : " + ExperienceTotal;
            myReturn += " , Items : " + ItemSlotsFormatOutput();
            myReturn += " , Damage : " + GetDamageTotalString;

            return myReturn;
        }

        #endregion
    }
}
