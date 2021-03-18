using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Game.Enums
{
    public enum BattleLocationEnum
    {
        Unknown = 0,
        [Display(Description = "The Shire")]
        Shire = 1,
        [Display(Description = "Elven City")]
        ElvenCity = 2,
        [Display(Description = "Forest")]
        Forest = 3,
        [Display(Description = "Dungeons")]
        Dungeons = 4,
        [Display(Description = "Mordor")]
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
        public static string ToMessage(this BattleLocationEnum value) =>
            value switch
            {
                BattleLocationEnum.Shire     => "The Shire",
                BattleLocationEnum.ElvenCity => "Elven City",
                BattleLocationEnum.Forest    => "Forest",
                BattleLocationEnum.Dungeons  => "Dungeons",
                BattleLocationEnum.Mordor    => "Mordor",
                _                            => "Battle location"
            };


        /// <summary>
        /// Display a String for the Enums with an appropriate article
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessageWithArticle(this BattleLocationEnum value) =>
            value switch
            {
                BattleLocationEnum.Shire     => "the Shire",
                BattleLocationEnum.ElvenCity => "the Elven city",
                BattleLocationEnum.Forest    => "the forest",
                BattleLocationEnum.Dungeons  => "the dungeon",
                BattleLocationEnum.Mordor    => "Mordor",
                _                            => "Battle location"
            };

        /// <summary>
        /// Gets the background image file name for a battle location
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToImageURI(this BattleLocationEnum value) =>
            value switch
            {
                BattleLocationEnum.Shire     => "shire_background.png",
                BattleLocationEnum.ElvenCity => "rivendell_background.png",
                BattleLocationEnum.Forest    => "forest_background.png",
                BattleLocationEnum.Dungeons  => "dungeon_background.png",
                BattleLocationEnum.Mordor    => "mordor_background.png",
                _                            => "item.png"
            };
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
