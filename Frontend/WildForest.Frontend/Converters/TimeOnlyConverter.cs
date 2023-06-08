using System;
using System.Globalization;
using System.Windows.Data;

namespace WildForest.Frontend.Converters
{
    /// <summary>
    /// Converter for displaying TimeOnly in HH:mm format
    /// </summary>
    internal sealed class TimeOnlyConverter : IValueConverter
    {
        /// <summary>
        /// Method for displaying TimeOnly in HH:mm format
        /// </summary>
        /// <param name="value">TimeOnly</param>
        /// <param name="targetType">Type</param>
        /// <param name="parameter">Parameter</param>
        /// <param name="culture">Culture</param>
        /// <returns>TimeOnly in HH:mm format</returns>
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