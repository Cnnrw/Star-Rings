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

        // A mid-range combatant
        Jedi = 1,

        // A quick and clever combatant
        Princess = 2,

        // Long range combatant
        Smuggler = 3,

        // A lumbering, close-range combatant
        Wookie = 4,

        // A slow, fragile, and close range combatant
        ProtocolDroid = 5,

        // A sturdy, mid-range combatant
        AstroDroid = 6,
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

                case CharacterJobEnum.Unknown:
                    break;
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
                var myReturn = myList.Where(a => a.ToString() != CharacterJobEnum.Unknown.ToString())
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
