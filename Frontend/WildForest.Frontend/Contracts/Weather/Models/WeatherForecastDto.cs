using System;
using System.Text.Json.Serialization;

namespace WildForest.Frontend.Contracts.Weather.Models;

public sealed class WeatherForecastDto
{
    [JsonPropertyName("weatherId")]
    public Guid WeatherId { get; set; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("time")]
    public TimeOnly Time { get; set; }

    [JsonPropertyName("temperature")]
    public Temperature Temperature { get; set; }

    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public byte Humidity { get; set; }

    [JsonPropertyName("weatherDescription")]
    public WeatherDescription WeatherDescription { get; set; }

    [JsonPropertyName("cloudiness")]
    public byte Cloudiness { get; set; }

    [JsonPropertyName("wind")]
    public Wind Wind { get; set; }

    [JsonPropertyName("visibility")]
    public double Visibility { get; set; }

    [JsonPropertyName("precipitationProbability")]
    public byte PrecipitationProbability { get; set; }

    [JsonPropertyName("precipitationVolume")]
    public double PrecipitationVolume { get; set; }

    public WeatherForecastDto(
        Guid weatherId, DateOnly date, TimeOnly time, Temperature temperature,
        int pressure, byte humidity, WeatherDescription weatherDescription,
        byte cloudiness, Wind wind, double visibility,
        byte precipitationProbability, double precipitationVolume)
    {
        WeatherId = weatherId;
        Date = date;
        Time = time;
        Temperature = temperature;
        Pressure = pressure;
        Humidity = humidity;
        WeatherDescription = weatherDescription;
        Cloudiness = cloudiness;
        Wind = wind;
        Visibility = visibility;
        PrecipitationProbability = precipitationProbability;
        PrecipitationVolume = precipitationVolume;
    }
}