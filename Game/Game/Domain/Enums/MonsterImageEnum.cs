using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Game.Enums
{
    public enum MonsterImageEnum
    {
        [Display(Description = "Unknown")]
        Unknown = 0,
        [Display(Description = "Dark Elf")]
        DarkElf   = 1,
        [Display(Description = "Dead King")]
        DeadKing  = 2,
        [Display(Description = "Nazgul")]
        Nazgul    = 3,
        [Display(Description = "Oliphant")]
        Oliphant  = 4,
        [Display(Description = "Orc")]
        Orc       = 5,
        [Display(Description = "Spider")]
        Spider    = 6,
        [Display(Description = "Troll")]
        Troll     = 7,
        [Display(Description = "Warg Rider")]
        WargRider = 8
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class MonsterImageEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this MonsterImageEnum value) =>
            value switch
            {
                MonsterImageEnum.DarkElf   => "Dark Elf",
                MonsterImageEnum.DeadKing  => "Dead King",
                MonsterImageEnum.Nazgul    => "Nazgul",
                MonsterImageEnum.Oliphant  => "Oliphant",
                MonsterImageEnum.Orc       => "Orc",
                MonsterImageEnum.Spider    => "Spider",
                MonsterImageEnum.Troll     => "Troll",
                MonsterImageEnum.WargRider => "Warg Rider",
                _                     => "Monster"
            };

        /// <summary>
        /// Convert the Job to its corresponding image
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToImageURI(this MonsterImageEnum value) =>
            value switch
            {
                MonsterImageEnum.DarkElf   => "dark_elf.png",
                MonsterImageEnum.DeadKing  => "dead_king.png",
                MonsterImageEnum.Nazgul    => "nazgul.png",
                MonsterImageEnum.Oliphant  => "oliphant.png",
                MonsterImageEnum.Orc       => "orc.png",
                MonsterImageEnum.Spider    => "spider.png",
                MonsterImageEnum.Troll     => "troll.png",
                MonsterImageEnum.WargRider => "warg_rider.png",
                _                     => "item.png"
            };

        /// <summary>
        /// Convert ImageURI to MonsterEnum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MonsterImageEnum FromImageURI(string value) =>
            value switch
            {
                "dark_elf.png"   => MonsterImageEnum.DarkElf,
                "dead_king.png"  => MonsterImageEnum.DeadKing,
                "nazgul.png"     => MonsterImageEnum.Nazgul,
                "oliphant.png"   => MonsterImageEnum.Oliphant,
                "orc.png"        => MonsterImageEnum.Orc,
                "spider.png"     => MonsterImageEnum.Spider,
                "troll.png"      => MonsterImageEnum.Troll,
                "warg_rider.png" => MonsterImageEnum.WargRider,
                _                => MonsterImageEnum.Unknown
            };

        /// <summary>
        /// Convert the Job to its corresponding icon image
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToIconImageURI(this MonsterImageEnum value) =>
            value switch
            {
                MonsterImageEnum.DarkElf   => "dark_elf_icon.png",
                MonsterImageEnum.DeadKing  => "dead_king_icon.png",
                MonsterImageEnum.Nazgul    => "nazgul_icon.png",
                MonsterImageEnum.Oliphant  => "oliphant_icon.png",
                MonsterImageEnum.Orc       => "orc_icon.png",
                MonsterImageEnum.Spider    => "spider_icon.png",
                MonsterImageEnum.Troll     => "troll_icon.png",
                MonsterImageEnum.WargRider => "warg_rider_icon.png",
                _                     => "item.png"
            };
    }

    /// <summary>
    /// Helper for Character Jobs
    /// </summary>
    public static class MonsterImageEnumHelper
    {
        /// <summary>
        /// Gets the list of default Monsters
        /// Does not include the Unknown Job.
        /// </summary>
        public static List<string> GetListMonsters
        {
            get
            {
                var myList = Enum.GetNames(typeof(MonsterImageEnum)).ToList();
                var myReturn = myList.Where(a => a.ToString() != MonsterImageEnum.Unknown.ToString())
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
        public static MonsterImageEnum ConvertStringToEnum(string value) =>
            (MonsterImageEnum)Enum.Parse(typeof(MonsterImageEnum), value);

    }
}
