using System.Collections.Generic;
using System.Linq;

namespace Game.Enums
{
    /// <summary>
    /// The Conditions a round can have
    /// </summary>
    public enum BattleModeEnum
    {
        // Invalid State
        Unknown = 0,

        // Default, just click until the end
        SimpleNext = 1,

        // Allow user to choose ability instead of action
        SimpleAbility = 2,

        // Map that just clicks until the end, monsters move, players don't move
        MapNext = 3,

        // Map that allows characters to choose ability, monsters move, players don't ove
        MapAbility = 4,

        // Map that allows characters to move and choose ability
        MapFull = 5,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class BattleModeEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this BattleModeEnum value)
        {
            // Default String
            var Message = "Unknown";

            switch (value)
            {
                case BattleModeEnum.MapFull:
                    Message = "Map All Actions";
                    break;

                case BattleModeEnum.MapNext:
                    Message = "Map Next Button";
                    break;

                case BattleModeEnum.MapAbility:
                    Message = "Map Abilities";
                    break;

                case BattleModeEnum.SimpleNext:
                    Message = "Simple Next";
                    break;

                case BattleModeEnum.SimpleAbility:
                    Message = "Simple Abilities";
                    break;
            }
            return Message;
        }
    }

    /// <summary>
    /// Helper for the BattleMode Enum Class
    /// </summary>
    public static class BattleModeEnumHelper
    {
        /// <summary>
        /// Returns a list of strings of the enum for BattleMode
        /// Removes the BattleModes that are not changable by Items such as Unknown, MaxHealth
        /// </summary>
        public static List<string> GetListAll
        {
            get
            {
                var myList = System.Enum.GetNames(typeof(BattleModeEnum)).ToList();
                return myList;
            }
        }

        /// <summary>
        /// Returns a list of Full strings of the enum for BattleMode
        /// Removes the BattleModes that are not changable by Items such as Unknown, MaxHealth
        /// </summary>
        public static IEnumerable<string> GetListMessageAll =>
            (from object item in System.Enum.GetValues(typeof(BattleModeEnum))
             select ((BattleModeEnum)item).ToMessage()).ToList();

        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BattleModeEnum ConvertStringToEnum(string value) =>
            (BattleModeEnum)System.Enum.Parse(typeof(BattleModeEnum), value);

        /// <summary>
        /// Given the Full String for an enum, return its value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BattleModeEnum ConvertMessageStringToEnum(string value) =>
            System.Enum.GetValues(typeof(BattleModeEnum)).Cast<BattleModeEnum>()
                  .FirstOrDefault(item => item.ToMessage().Equals(value));
    }
}