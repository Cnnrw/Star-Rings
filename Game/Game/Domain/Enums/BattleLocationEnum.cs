using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Enums
{
    /// <summary>
    ///     <para>
    ///         The locations Monsters may appear and Battles take place. BattleLocationEnum
    ///         has been marked with the flag attribute, meaning bitwise operations can be
    ///         used to specify enumerated battle location values.
    ///     </para>
    ///     <para/>
    ///     Possible combinations of values with FlagsAttribute:
    ///     <list type="number">
    ///         <item><term>- Shire</term></item>
    ///         <item><term>- ElvenCity</term></item>
    ///         <item><term>- Shire, ElvenCity</term></item>
    ///         <item><term>- Forest</term></item>
    ///         <item><term>- Shire, Forest</term></item>
    ///         <item><term>- ElvenCity, Forest</term></item>
    ///         <item><term>- Shire, ElvenCity, Forest</term></item>
    ///         <item><term>- Dungeons</term></item>
    ///         <item><term>- Shire, Dungeons</term></item>
    ///         <item><term>- ElvenCity, Dungeons</term></item>
    ///         <item><term>- Shire, ElvenCity, Dungeons</term></item>
    ///         <item><term>- Forest, Dungeons</term></item>
    ///         <item><term>- Shire, Forest, Dungeons</term></item>
    ///         <item><term>- ElvenCity, Forest, Dungeons</term></item>
    ///         <item><term>- Shire, ElvenCity, Forest, Dungeons</term></item>
    ///         <item><term>- Mordor</term></item>
    ///     </list>
    ///     etc.
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
            var Result = value switch
                         {
                             BattleLocationEnum.Shire     => "The Shire",
                             BattleLocationEnum.ElvenCity => "Elven City",
                             BattleLocationEnum.Forest    => "Forest",
                             BattleLocationEnum.Dungeons  => "Dungeons",
                             BattleLocationEnum.Mordor    => "Mordor",
                             _                            => "Battle location"
                         };

            return Result;
        }

        /// <summary>
        /// Display a String for the Enums with an appropriate article
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessageWithArticle(this BattleLocationEnum value)
        {
            // Default String
            var Result = value switch
            {
                BattleLocationEnum.Shire => "the Shire",
                BattleLocationEnum.ElvenCity => "an Elven city",
                BattleLocationEnum.Forest => "the forest",
                BattleLocationEnum.Dungeons => "a dungeon",
                BattleLocationEnum.Mordor => "Mordor",
                _ => "Battle location"
            };

            return Result;
        }

        /// <summary>
        /// Gets the background image file name for a battle location
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToImageUri(this BattleLocationEnum value)
        {
            // Default String
            var Result = value switch
            {
                BattleLocationEnum.Shire => "shire_background.png",
                BattleLocationEnum.ElvenCity => "page_background_2.png",
                BattleLocationEnum.Forest => "forest_background.png",
                BattleLocationEnum.Dungeons => "dungeon_background.png",
                BattleLocationEnum.Mordor => "page_background_5.png",
                _ => "item.png"
            };

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
                var myList = Enum.GetNames(typeof(BattleLocationEnum)).ToList();
                var myReturn = myList.Where(a => a != BattleLocationEnum.Unknown.ToString())
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
            (BattleLocationEnum)Enum.Parse(typeof(BattleLocationEnum), value);
    }
}
