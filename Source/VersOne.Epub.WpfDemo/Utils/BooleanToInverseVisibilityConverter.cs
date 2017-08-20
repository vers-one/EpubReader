using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VersOne.Epub.WpfDemo.Utils
{
    public class BooleanToInverseVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                return Visibility.Hidden;
            }
            return !(bool)value ? Visibility.Visible : (parameter != null ? Visibility.Collapsed : Visibility.Hidden);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
