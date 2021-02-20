using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Enums
{
    /// <summary>
    /// The Types of Jobs a character can have
    /// Used in Character Crudi, and in Battles.
    /// </summary>
    public enum CharacterJobEnum
    {
        // Not specified
        Unknown = 0,

        // Luke is ...
        Jedi = 1,

        // Leia is ...
        Princess = 2,

        // Han is ...
        Smuggler = 3,

        // Chewbacca is ...
        Wookie = 4,

        // C3PO is ...
        ProtocolDroid = 5,

        // R2D2 is ...
        AstroDroid = 6,

        // Fighters hit hard and have fight abilities
        Fighter = 10,

        // Clerics defend well and have buff abilities
        Cleric = 12,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class CharacterJobEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this CharacterJobEnum value)
        {
            // Default String
            var Message = "Player";

            switch (value)
            {
                case CharacterJobEnum.Jedi:
                    Message = "Jedi";
                    break;

                case CharacterJobEnum.Princess:
                    Message = "Princess";
                    break;

                case CharacterJobEnum.Smuggler:
                    Message = "Smuggler";
                    break;

                case CharacterJobEnum.Wookie:
                    Message = "Wookie";
                    break;

                case CharacterJobEnum.ProtocolDroid:
                    Message = "Protocol Droid";
                    break;

                case CharacterJobEnum.AstroDroid:
                    Message = "Astro Droid";
                    break;

                case CharacterJobEnum.Fighter:
                    Message = "Fighter";
                    break;

                case CharacterJobEnum.Cleric:
                    Message = "Cleric";
                    break;
                case CharacterJobEnum.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return Message;
        }

        /// <summary>
        /// Convert the Job to its corresponding icon image
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToIconImageURI(this CharacterJobEnum value)
        {
            // Default String
            var ImageURI = "item.png";

            switch (value)
            {
                case CharacterJobEnum.Jedi:
                    ImageURI = "jedi_icon.png";
                    break;

                case CharacterJobEnum.Princess:
                    ImageURI = "jedi_princess_icon.png";
                    break;

                case CharacterJobEnum.Smuggler:
                    ImageURI = "smuggler_icon.png";
                    break;

                case CharacterJobEnum.Wookie:
                    ImageURI = "wookie_icon.png";
                    break;

                case CharacterJobEnum.ProtocolDroid:
                    ImageURI = "protocol_droid_icon.png";
                    break;

                case CharacterJobEnum.AstroDroid:
                    ImageURI = "astromech_icon.png";
                    break;

                case CharacterJobEnum.Unknown:
                default:
                    break;
            }

            return ImageURI;
        }
    }

    /// <summary>
    /// Helper for Character Jobs
    /// </summary>
    public static class CharacterJobEnumHelper
    {
        /// <summary>
        /// Gets the list of Character Jobs that Characters can have.
        /// Does not include the Unknown Job.
        /// </summary>
        public static List<string> GetListCharacterJobs
        {
            get
            {
                var myList = Enum.GetNames(typeof(CharacterJobEnum)).ToList();
                var myReturn = myList.Where(a =>
                                                a.ToString() != CharacterJobEnum.Unknown.ToString()
                                           )
                                     .OrderBy(a => a)
                                     .ToList();
                return myReturn;
            }
        }

        /// <summary>
        /// Given the string for an enum, return its value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CharacterJobEnum ConvertStringToEnum(string value) =>
            (CharacterJobEnum)Enum.Parse(typeof(CharacterJobEnum), value);
    }
}