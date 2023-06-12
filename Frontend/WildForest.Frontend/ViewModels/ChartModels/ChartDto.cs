using System;

namespace WildForest.Frontend.ViewModels.ChartModels;

/// <summary>
/// Class used for showing time tooltip in chart
/// </summary>
internal sealed class ChartDto
{
    public TimeOnly Time { get; set; }

    public double Temperature { get; set; }

    public ChartDto(TimeOnly time, double temperature)
    {
        Time = time;
        Temperature = temperature;
    }
}