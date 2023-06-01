using System;

namespace WildForest.Frontend.Services.Weather.Extensions;

internal static class TimeOnlyExtension
{
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