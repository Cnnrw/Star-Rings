using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Enums
{
    /// <summary>
    /// Types of Hits during a Turn.
    /// </summary>
    public enum HitStatusEnum
    {
        Unknown = 0,

        // Default for settings mode to use
        Default = 1,

        // Miss
        Miss = 10,

        // Critical Miss, miss and something bad happens
        CriticalMiss = 15,

        // Hit
        Hit = 20,

        // Critical Hit, bonus after hit happens
        CriticalHit = 25
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class HitStatusEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this HitStatusEnum value)
        {
            // Default String
            var Message = "Unknown";

            switch (value)
            {
                case HitStatusEnum.Hit:
                    Message = " hits ";
                    break;

                case HitStatusEnum.CriticalHit:
                    Message = " hits really hard ";
                    break;

                case HitStatusEnum.Miss:
                    Message = " misses ";
                    break;

                case HitStatusEnum.CriticalMiss:
                    Message = " misses really badly";
                    break;

                case HitStatusEnum.Unknown:
                    break;

                case HitStatusEnum.Default:
                    break;
            }

            return Message;
        }
    }

    /// <summary>
    /// Helper for the Hit Enum Class
    /// </summary>
    public static class HitStatusEnumHelper
    {
        /// <summary>
        /// Returns a list of strings of the enum for Hit
        /// Removes the Hits that are not changable by Items such as Unknown, MaxHealth
        /// </summary>
        public static List<string> GetListAll
        {
            get
            {
                var myList = Enum.GetNames(typeof(HitStatusEnum)).ToList();
                return myList;
            }
        }

        /// <summary>
        /// Returns a list of Full strings of the enum for Hit
        /// Removes the Hits that are not changable by Items such as Unknown, MaxHealth
        /// </summary>
        public static IEnumerable<string> GetListMessageAll =>
            (from object item in Enum.GetValues(typeof(HitStatusEnum)) select ((HitStatusEnum)item).ToMessage())
            .ToList();

        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HitStatusEnum ConvertStringToEnum(string value) =>
            (HitStatusEnum)Enum.Parse(typeof(HitStatusEnum), value);

        /// <summary>
        /// Given the Full String for an enum, return its value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HitStatusEnum ConvertMessageStringToEnum(string value) =>
            Enum.GetValues(typeof(HitStatusEnum)).Cast<HitStatusEnum>()
                .FirstOrDefault(item => item.ToMessage().Equals(value));
    }
}
