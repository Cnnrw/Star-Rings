using System;
using System.Globalization;

using Xamarin.Forms;

namespace Game.Converters
{

    /// <summary>
    ///     Converts from an Int to an Enum value
    ///     <remarks>
    ///         This converter is used by the Pickers, to change from the picker value to the string value etc.
    ///         This allows the convert in the picker to be data bound back and forth the model
    ///         The picker requires this because the picker must be a string, but the enum is a value...
    ///     </remarks>
    /// </summary>
    public class IntEnumConverter : IValueConverter
    {
        /// <summary>
        ///     Converts a string to an int
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is Enum
                ? (int)value
                : 0;

        /// <summary>
        ///     Converts from Int to String
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is int
                ? (object)Enum.ToObject(targetType, value)
                              .ToString()
                : 0;
    }
}
