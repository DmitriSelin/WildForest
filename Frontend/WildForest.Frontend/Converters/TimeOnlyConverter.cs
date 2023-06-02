using System;
using System.Globalization;
using System.Windows.Data;

namespace WildForest.Frontend.Converters
{
    internal sealed class TimeOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeOnly time)
            {
                return time.ToString("HH:mm");
            }

            return null!;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}