using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Game.Enums
{
    public enum CharacterImageEnum
    {
        Unknown = 0,
        [Display(Description = "AstroMech")]
        astromech   = 1,
        [Display(Description = "Jedi")]
        jedi  = 2,
        [Display(Description = "Jedi Princess")]
        jedi_princess    = 3,
        [Display(Description = "Protocol Droid")]
        protocol_droid  = 4,
        [Display(Description = "Smuggler")]
        smuggler       = 5,
        [Display(Description = "Wookie")]
        wookie    = 6,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class CharacterImageEnumExtensions
    {
        /// <summary>
        /// Convert the Job to its corresponding image
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToImageURI(this CharacterImageEnum value) =>
            value switch
            {
                CharacterImageEnum.astromech      => $"{CharacterImageEnum.astromech.ToString()}.png",
                CharacterImageEnum.jedi           => $"{CharacterImageEnum.jedi.ToString()}.png",
                CharacterImageEnum.jedi_princess  => $"{CharacterImageEnum.jedi_princess.ToString()}.png",
                CharacterImageEnum.protocol_droid => $"{CharacterImageEnum.protocol_droid.ToString()}.png",
                CharacterImageEnum.smuggler       => $"{CharacterImageEnum.smuggler.ToString()}.png",
                CharacterImageEnum.wookie         => $"{CharacterImageEnum.wookie.ToString()}.png",
                _                                 => "item.png"
            };

        /// <summary>
        /// Convert ImageURI to MonsterEnum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToIconImageURI(this CharacterImageEnum value) =>
            value switch
            {
                CharacterImageEnum.astromech      => $"{CharacterImageEnum.astromech.ToString()}_icon.png",
                CharacterImageEnum.jedi           => $"{CharacterImageEnum.jedi.ToString()}_icon.png",
                CharacterImageEnum.jedi_princess  => $"{CharacterImageEnum.jedi_princess.ToString()}_icon.png",
                CharacterImageEnum.protocol_droid => $"{CharacterImageEnum.protocol_droid.ToString()}_icon.png",
                CharacterImageEnum.smuggler       => $"{CharacterImageEnum.smuggler.ToString()}_icon.png",
                CharacterImageEnum.wookie         => $"{CharacterImageEnum.wookie.ToString()}_icon.png",
                _                                 => "item.png"
            };

        public static CharacterImageEnum FromImageURI(string value) =>
            value switch
            {
                "astromech.png"      => CharacterImageEnum.astromech,
                "jedi.png"           => CharacterImageEnum.jedi,
                "jedi_princess.png"  => CharacterImageEnum.jedi_princess,
                "protocol_droid.png" => CharacterImageEnum.protocol_droid,
                "smuggler.png"       => CharacterImageEnum.smuggler,
                "wookie.png"         => CharacterImageEnum.wookie,
                _                    => CharacterImageEnum.Unknown
            };
    }

    /// <summary>
    /// Helper for Character Jobs
    /// </summary>
    public static class CharacterImageEnumHelper
    {
        /// <summary>
        /// Gets the list of default character images
        /// Does not include the Unknown Job.
        /// </summary>
        public static List<string> GetListCharacterImages
        {
            get
            {
                var myList = Enum.GetNames(typeof(CharacterImageEnum)).ToList();
                var myReturn = myList.Where(a => a.ToString() != CharacterImageEnum.Unknown.ToString())
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
        public static CharacterImageEnum ConvertStringToEnum(string value) =>
            (CharacterImageEnum)Enum.Parse(typeof(CharacterImageEnum), value);

    }
}
