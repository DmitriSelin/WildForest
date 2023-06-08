using System.Globalization;
using System;
using System.Windows.Data;

namespace WildForest.Frontend.Converters;

/// <summary>
/// Converter for time conversion from UTC to local time
/// </summary>
internal sealed class UtcDateTimeConverter : IValueConverter
{
    /// <summary>
    /// Method for time conversion from UTC to local time
    /// </summary>
    /// <param name="value">DateTime</param>
    /// <param name="targetType">Type</param>
    /// <param name="parameter">Parameter</param>
    /// <param name="culture">Culture</param>
    /// <returns>Local DateTime in HH:mm format</returns>
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