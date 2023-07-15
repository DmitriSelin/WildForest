using WildForest.Domain.Common.Models;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Weather.Entities;

public sealed class ThreeHourWeatherForecast : Entity<WeatherForecastId>
{
    public DateTime Date { get; }

    public Temperature Temperature { get; } = null!;

    public Pressure Pressure { get; } = null!;

    public Humidity Humidity { get; } = null!;

    public WeatherDescription WeatherDescription { get; } = null!;

    public Cloudiness Cloudiness { get; } = null!;

    public Wind Wind { get; } = null!;

    public Visibility Visibility { get; } = null!;

    public PrecipitationProbability PrecipitationProbability { get; } = null!;

    public PrecipitationVolume? PrecipitationVolume { get; }

    public WeatherForecastId DayWeatherForecastId { get; } = null!;

    public DayWeatherForecast DayWeatherForecast { get; } = null!;

    private ThreeHourWeatherForecast(
        WeatherForecastId id,
        DateTime date,
        Temperature temperature,
        Pressure pressure,
        Humidity humidity,
        WeatherDescription weatherDescription,
        Cloudiness cloudiness,
        Wind wind,
        Visibility visibility,
        PrecipitationProbability precipitationProbability,
        PrecipitationVolume? precipitationVolume,
        WeatherForecastId dayWeatherForecastId) : base(id)
    {
        Date = date;
        Temperature = temperature;
        Pressure = pressure;
        Humidity = humidity;
        WeatherDescription = weatherDescription;
        Cloudiness = cloudiness;
        Wind = wind;
        Visibility = visibility;
        PrecipitationProbability = precipitationProbability;
        PrecipitationVolume = precipitationVolume;
        DayWeatherForecastId = dayWeatherForecastId;
    }

    private ThreeHourWeatherForecast(WeatherForecastId id) : base(id) { }

    public static ThreeHourWeatherForecast Create(
        DateTime date,
        Temperature temperature,
        Pressure pressure,
        Humidity humidity,
        WeatherDescription weatherDescription,
        Cloudiness cloudiness,
        Wind wind,
        Visibility visibility,
        PrecipitationProbability precipitationProbability,
        PrecipitationVolume? precipitationVolume,
        WeatherForecastId dayWeatherForecastId)
    {
        return new(
            WeatherForecastId.Create(),
            date,
            temperature,
            pressure,
            humidity,
            weatherDescription,
            cloudiness,
            wind,
            visibility,
            precipitationProbability,
            precipitationVolume,
            dayWeatherForecastId);
    }
}