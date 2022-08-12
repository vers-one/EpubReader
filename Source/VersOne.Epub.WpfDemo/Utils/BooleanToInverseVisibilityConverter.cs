using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VersOne.Epub.WpfDemo.Utils
{
    /// <summary>
    /// XAML data converter which converts <c>false</c> to <see cref="Visibility.Visible" />
    /// and <c>true</c> to <see cref="Visibility.Hidden" /> or <see cref="Visibility.Collapsed"/> (depending on the supplied additional parameter).
    /// </summary>
    public class BooleanToInverseVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts <c>false</c> to <see cref="Visibility.Visible" />
        /// and <c>true</c> to <see cref="Visibility.Hidden" /> or <see cref="Visibility.Collapsed"/> (depending on the value of the <paramref name="parameter"/>).
        /// </summary>
        /// <param name="value">Boolean value to convert from.</param>
        /// <param name="targetType">The type of the binding target property. Not used in this converter.</param>
        /// <param name="parameter">
        /// Additional parameter which determines whether <c>true</c> value should be converted to <see cref="Visibility.Hidden" /> (if the parameter is <c>null</c>)
        /// or to <see cref="Visibility.Collapsed"/> (if the parameter is not <c>null</c>).
        /// </param>
        /// <param name="culture">The culture for the conversion. Not used in this converter.</param>
        /// <returns>
        /// <see cref="Visibility.Visible" /> if the <paramref name="value"/> is <c>false</c> and
        /// <see cref="Visibility.Hidden" /> or <see cref="Visibility.Collapsed"/> (depending on the value of the <paramref name="parameter"/>)
        /// if the <paramref name="value"/> is <c>true</c>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                return Visibility.Hidden;
            }
            if (!(bool)value)
            {
                return Visibility.Visible;
            }
            else if (parameter != null)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// Performs a backwards conversion. Not used in this converter.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>This method always returns <c>null</c>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
