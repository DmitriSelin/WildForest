using System.Collections.Generic;
using System.Text.Json.Serialization;
using WildForest.Frontend.Contracts.Weather.Models;

namespace WildForest.Frontend.Contracts.Weather;

public sealed class WeatherForecastVm 
{
    [JsonPropertyName("weatherForecasts")]
    public List<WeatherForecastDto> WeatherForecasts { get; set; }

    [JsonPropertyName("mediumMark")]
    public double MediumMark { get; set; }

    public WeatherForecastVm(List<WeatherForecastDto> weatherForecasts, double mediumMark)
    {
        WeatherForecasts = weatherForecasts;
        MediumMark = mediumMark;
    }
}