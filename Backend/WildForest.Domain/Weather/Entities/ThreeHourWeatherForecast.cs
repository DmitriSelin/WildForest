using WildForest.Domain.Common.Models;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Weather.Entities;

public sealed class ThreeHourWeatherForecast : Entity<ThreeHourWeatherForecastId>
{
    public TimeOnly Time { get; }

    public Temperature Temperature { get; } = null!;

    public Pressure Pressure { get; } = null!;

    public Humidity Humidity { get; } = null!;

    public WeatherDescription Description { get; } = null!;

    public Cloudiness Cloudiness { get; } = null!;

    public Wind Wind { get; } = null!;

    public Visibility Visibility { get; } = null!;

    public PrecipitationProbability PrecipitationProbability { get; } = null!;

    public PrecipitationVolume? PrecipitationVolume { get; }

    public WeatherForecastId WeatherForecastId { get; } = null!;

    public WeatherForecast WeatherForecast { get; } = null!;

    private ThreeHourWeatherForecast(
        ThreeHourWeatherForecastId id,
        TimeOnly time,
        Temperature temperature,
        Pressure pressure,
        Humidity humidity,
        WeatherDescription weatherDescription,
        Cloudiness cloudiness,
        Wind wind,
        Visibility visibility,
        PrecipitationProbability precipitationProbability,
        PrecipitationVolume? precipitationVolume,
        WeatherForecastId weatherForecastId) : base(id)
    {
        Time = time;
        Temperature = temperature;
        Pressure = pressure;
        Humidity = humidity;
        Description = weatherDescription;
        Cloudiness = cloudiness;
        Wind = wind;
        Visibility = visibility;
        PrecipitationProbability = precipitationProbability;
        PrecipitationVolume = precipitationVolume;
        WeatherForecastId = weatherForecastId;
    }

    public static ThreeHourWeatherForecast Create(
        TimeOnly time,
        Temperature temperature,
        Pressure pressure,
        Humidity humidity,
        WeatherDescription weatherDescription,
        Cloudiness cloudiness,
        Wind wind,
        Visibility visibility,
        PrecipitationProbability precipitationProbability,
        PrecipitationVolume? precipitationVolume,
        WeatherForecastId weatherForecastId)
    {
        return new(
            ThreeHourWeatherForecastId.Create(), time,
            temperature, pressure, humidity,
            weatherDescription, cloudiness, wind,
            visibility, precipitationProbability,
            precipitationVolume, weatherForecastId);
    }

    private ThreeHourWeatherForecast(ThreeHourWeatherForecastId id) : base(id) { }
}