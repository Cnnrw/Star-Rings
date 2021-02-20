using System.Collections.Generic;
using System.Linq;

namespace Game.Enums
{
    /// <summary>
    /// The Types of location a battle can take place in.
    /// Used in Battles.
    /// </summary>
    public enum BattleLocationEnum
    {
        // Not specified
        Unknown = 0,

        // Rolling hills and open fields
        Shire = 1,

        // A magical metropolis
        ElvenCity = 2,

        // A dark, tangled forest
        Forest = 3,

        // Underground chambers and caves
        Dungeons = 4,

        // Desolate, firey wasteland
        Mordor = 5,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class BattleLocationEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this BattleLocationEnum value)
        {
            // Default String
            var Result = "Battle location";

            switch (value)
            {
                case BattleLocationEnum.Shire:
                    Result = "The Shire";
                    break;

                case BattleLocationEnum.ElvenCity:
                    Result = "Elven City";
                    break;

                case BattleLocationEnum.Forest:
                    Result = "Forest";
                    break;

                case BattleLocationEnum.Dungeons:
                    Result = "Dungeons";
                    break;

                case BattleLocationEnum.Mordor:
                    Result = "Mordor";
                    break;
            }

            return Result;
        }
    }

    /// <summary>
    /// Helper for Battle Locations
    /// </summary>
    public static class BattleLocationEnumHelper
    {
        /// <summary>
        /// Gets the list of Battle Locations.
        /// Does not include the Unknown Battle Location.
        /// </summary>
        public static List<string> GetListBattleLocations
        {
            get
            {
                var myList = System.Enum.GetNames(typeof(BattleLocationEnum)).ToList();
                var myReturn = myList.Where(a =>
                                                a.ToString() != BattleLocationEnum.Unknown.ToString()
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
        public static BattleLocationEnum ConvertStringToEnum(string value) =>
            (BattleLocationEnum)System.Enum.Parse(typeof(BattleLocationEnum), value);
    }
}