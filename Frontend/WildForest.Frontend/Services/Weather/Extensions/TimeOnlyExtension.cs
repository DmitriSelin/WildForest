using System;

namespace WildForest.Frontend.Services.Weather.Extensions;

/// <summary>
/// Extension for putting time in 3-hours intervals
/// </summary>
internal static class TimeOnlyExtension
{
    /// <summary>
    /// Method for putting time in 3-hours intervals
    /// </summary>
    /// <param name="currentTime">Current time</param>
    /// <returns>TimeOnly object in interval</returns>
    /// <exception cref="ArgumentException">Throw ArgumentException if time is invalid</exception>
    internal static TimeOnly PutIntoTimeInterval(this TimeOnly currentTime)
    {
        TimeOnly[] timeIntervals = new TimeOnly[]
        {
            new TimeOnly(0, 0), new TimeOnly(3, 0),
            new TimeOnly(6, 0), new TimeOnly(9, 0),
            new TimeOnly(12, 0), new TimeOnly(15, 0),
            new TimeOnly(18, 0), new TimeOnly(21, 0)
        };

        TimeOnly interval;

        for (int i = 0; i < timeIntervals.Length; i++)
        {
            interval = timeIntervals[i];

            if (currentTime < interval)
            {
                if (i == 0)
                {
                    throw new ArgumentException("Invalid time");
                }
                
                return timeIntervals[i - 1];
            }
        }

        return timeIntervals[timeIntervals.Length - 1];
    }
}