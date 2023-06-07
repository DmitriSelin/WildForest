using System.Globalization;
using System;
using System.Windows.Data;

namespace WildForest.Frontend.Converters;

internal sealed class UtcDateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dt)
        {
            int difference = DateTime.UtcNow.Hour - DateTime.Now.Hour;

            if (difference < 0)
                difference *= -1;

            dt = dt.AddHours(difference);

            return dt.ToString("HH:mm");
        }

        return null!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}