using System.Collections.Generic;
using System.Linq;

namespace Game.Enums
{
    /// <summary>
    /// The Types of Abilities players can have, used in Ability CRUDi and Battles.
    /// </summary>
    public enum AbilityEnum
    {
        // Not specified
        Unknown = 0,

        // Not specified
        None = 1,

        // General Abilities 10 Range
        // Heal Self
        Bandage = 10,


        // Fighter Abilities > 20 Range
        // Buff Speed
        Nimble = 21,

        // Buff Defense
        Toughness = 22,

        // Buff Attack
        Focus = 23,


        // Cleric Abilities > 50 Range
        // Buff Speed
        Quick = 51,

        // Buff Defense
        Barrier = 52,

        // Buff Attack
        Curse = 53,

        // Heal Self
        Heal = 54,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class AbilityEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this AbilityEnum value)
        {
            // Default String
            var message = "None";

            switch (value)
            {
                case AbilityEnum.Bandage:
                    message = "Apply Bandages";
                    break;

                case AbilityEnum.Nimble:
                    message = "React Quickly";
                    break;

                case AbilityEnum.Toughness:
                    message = "Toughen Up";
                    break;

                case AbilityEnum.Focus:
                    message = "Mental Focus";
                    break;

                case AbilityEnum.Quick:
                    message = "Anticipate";
                    break;

                case AbilityEnum.Barrier:
                    message = "Barrier Defense";
                    break;

                case AbilityEnum.Curse:
                    message = "Shout Curse";
                    break;

                case AbilityEnum.Heal:
                    message = "Heal Self";
                    break;
            }

            return message;
        }
    }

    /// <summary>
    /// Helper for the Ability Enum Class
    /// </summary>
    public static class AbilityEnumHelper
    {
        /// <summary>
        /// Returns a list of strings of the enum for Ability
        /// Removes the Abilities that are not changeable by Items
        /// such as Unknown, MaxHealth
        /// </summary>
        public static IEnumerable<string> GetFullList
        {
            get
            {
                var myList = System.Enum.GetNames(typeof(AbilityEnum)).ToList();
                return myList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Jedi
        /// </summary>
        public static IEnumerable<string> GetListJedi
        {
            get
            {
                var abilityList = new List<string>
                {
                    AbilityEnum.Nimble.ToString(),
                    AbilityEnum.Toughness.ToString(),
                    AbilityEnum.Focus.ToString()
                };

                abilityList.AddRange(GetListOthers);
                return abilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Wookie
        /// </summary>
        public static IEnumerable<string> GetListWookie
        {
            get
            {
                var abilityList = new List<string>
                {
                    AbilityEnum.Quick.ToString(),
                    AbilityEnum.Barrier.ToString(),
                    AbilityEnum.Curse.ToString(),
                    AbilityEnum.Heal.ToString()
                };

                abilityList.AddRange(GetListOthers);
                return abilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum of not Cleric or Fighter
        /// </summary>
        public static IEnumerable<string> GetListOthers
        {
            get
            {
                var abilityList = new List<string> {AbilityEnum.Bandage.ToString()};

                return abilityList;
            }
        }

        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AbilityEnum ConvertStringToEnum(string value)
        {
            return (AbilityEnum)System.Enum.Parse(typeof(AbilityEnum), value);
        }
    }
}
