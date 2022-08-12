using System;
using System.Globalization;
using System.Windows.Data;

namespace VersOne.Epub.WpfDemo.Utils
{
    /// <summary>
    /// XAML data converter which converts a value of type <see cref="Int32" /> to <see cref="Double" />.
    /// </summary>
    public class IntToDoubleConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value of type <see cref="Int32" /> to <see cref="Double" />.
        /// </summary>
        /// <param name="value"><see cref="Int32" /> value to convert from.</param>
        /// <param name="targetType">The type of the binding target property. Not used in this converter.</param>
        /// <param name="parameter">The converter parameter. Not used in this converter.</param>
        /// <param name="culture">The culture for the conversion. Not used in this converter.</param>
        /// <returns><paramref name="value" /> casted to <see cref="Double" />.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Int32))
            {
                throw new ArgumentException("value should be Int32");
            }
            int intValue = (int)value;
            return (double)intValue;
        }

        /// <summary>
        /// Converts a value of type <see cref="Double" /> to <see cref="Int32" />.
        /// </summary>
        /// <param name="value"><see cref="Double" /> value to convert from.</param>
        /// <param name="targetType">The type of the binding target property. Not used in this converter.</param>
        /// <param name="parameter">The converter parameter. Not used in this converter.</param>
        /// <param name="culture">The culture for the conversion. Not used in this converter.</param>
        /// <returns><paramref name="value" /> casted to <see cref="Int32" />.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Double))
            {
                throw new ArgumentException("value should be Double");
            }
            return Math.Truncate((double)value);
        }
    }
}
