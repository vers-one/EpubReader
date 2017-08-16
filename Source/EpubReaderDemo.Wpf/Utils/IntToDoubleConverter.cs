using System;
using System.Globalization;
using System.Windows.Data;

namespace EpubReaderDemo.Wpf.Utils
{
    public class IntToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Int32))
            {
                throw new ArgumentException("value should be Int32");
            }
            int intValue = (int)value;
            return (double)intValue;
        }

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
