using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Enums
{
    /// <summary>
    /// The Types of location a battle can take place in.
    /// Used in Monster, Battles.
    ///
    /// <remarks>
    ///     All possible combinations of values with FlagsAttribute:
    ///         0 - Unknown
    ///         1 - Shire
    ///         2 - ElvenCity
    ///         3 - Shire, ElvenCity
    ///         4 - Forest
    ///         5 - Shire, Forest
    ///         6 - ElvenCity, Forest
    ///         7 - Shire, ElvenCity, Forest
    ///         8 - Dungeons
    ///         9 - Shire, Dungeons
    ///        10 - ElvenCity, Dungeons
    ///        11 - Shire, ElvenCity, Dungeons
    ///        12 - Forest, Dungeons
    ///        13 - Shire, Forest, Dungeons
    ///        14 - ElvenCity, Forest, Dungeons
    ///        15 - Shire, ElvenCity, Forest, Dungeons
    ///        16 - Mordor
    ///        17 - Shire, Mordor
    /// etc.
    /// </remarks>
    /// </summary>
    [Flags]
    public enum BattleLocationEnum
    {
        Unknown = 0,

        // Rolling hills and open fields
        Shire = 1,

        // A magical metropolis
        ElvenCity = 2,

        // A dark, tangled forest
        Forest = 4,

        // Underground chambers and caves
        Dungeons = 8,

        // Desolate, fiery wasteland
        Mordor = 16,
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