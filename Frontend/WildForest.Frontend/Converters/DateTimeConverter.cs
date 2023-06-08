using System;
using System.Globalization;
using System.Windows.Data;

namespace WildForest.Frontend.Converters;

/// <summary>
/// Converter for displaying DateTime in HH:mm format
/// </summary>
internal sealed class DateTimeConverter : IValueConverter
{
    /// <summary>
    /// Method for displaying DateTime in HH:mm format
    /// </summary>
    /// <param name="value">DateTime</param>
    /// <param name="targetType">Type</param>
    /// <param name="parameter">Parameter</param>
    /// <param name="culture">Culture</param>
    /// <returns>DateTime in HH:mm format</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dt)
        {
            return dt.ToString("HH:mm");
        }

        return null!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}