using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Weather.Entities;

public sealed class WeatherForecast : Entity<WeatherForecastId>
{
    public DateTime Date { get; }

    public Temperature Temperature { get; } = null!;

    public Pressure Pressure { get; } = null!;

    public Humidity Humidity { get; } = null!;

    public WeatherDescription WeatherDescription { get; } = null!;

    public Cloudiness Cloudiness { get; } = null!;

    public Wind Wind { get; } = null!;

    public PrecipitationProbability PrecipitationProbability { get; } = null!;

    public PrecipitationVolume? PrecipitationVolume { get; }

    public CityId CityId { get; } = null!;

    public City City { get; } = null!;

    private WeatherForecast(
        WeatherForecastId id,
        DateTime date,
        Temperature temperature,
        Pressure pressure,
        Humidity humidity,
        WeatherDescription weatherDescription,
        Cloudiness cloudiness,
        Wind wind,
        PrecipitationProbability precipitationProbability,
        PrecipitationVolume? precipitationVolume,
        CityId cityId) : base(id)
    {
        Date = date;
        Temperature = temperature;
        Pressure = pressure;
        Humidity = humidity;
        WeatherDescription = weatherDescription;
        Cloudiness = cloudiness;
        Wind = wind;
        PrecipitationProbability = precipitationProbability;
        PrecipitationVolume = precipitationVolume;
        CityId = cityId;
    }

    private WeatherForecast(WeatherForecastId id) : base(id) { }

    public static WeatherForecast Create(
        DateTime date,
        Temperature temperature,
        Pressure pressure,
        Humidity humidity,
        WeatherDescription weatherDescription,
        Cloudiness cloudiness,
        Wind wind,
        PrecipitationProbability precipitationProbability,
        PrecipitationVolume? precipitationVolume,
        CityId cityId)
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
            precipitationProbability,
            precipitationVolume,
            cityId);
    }
}