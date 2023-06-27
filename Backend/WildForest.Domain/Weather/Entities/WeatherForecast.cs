using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Marks.Entities;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Domain.WeatherMarks.Entities;

namespace WildForest.Domain.Weather.Entities;

public sealed class WeatherForecast : Entity<WeatherId>
{
    public DateTime Time { get; }

    public Temperature Temperature { get; } = null!;

    public Pressure Pressure { get; } = null!;

    public Humidity Humidity { get; } = null!;

    public WeatherDescription WeatherDescription { get; } = null!;

    public Cloudiness Cloudiness { get; } = null!;

    public Wind Wind { get; } = null!;

    public Visibility Visibility { get; } = null!;

    public PrecipitationProbability PrecipitationProbability { get; } = null!;

    public PrecipitationVolume? PrecipitationVolume { get; }

    public CityId CityId { get; } = null!;

    public City City { get; } = null!;

    private readonly List<Mark> _marks = new();

    public IReadOnlyList<Mark> Marks => _marks.AsReadOnly();

    public WeatherMark WeatherMark { get; } = null!;

    private WeatherForecast(
        WeatherId id,
        DateTime time,
        Temperature temperature,
        Pressure pressure,
        Humidity humidity,
        WeatherDescription weatherDescription,
        Cloudiness cloudiness,
        Wind wind,
        Visibility visibility,
        PrecipitationProbability precipitationProbability,
        PrecipitationVolume? precipitationVolume,
        CityId cityId) : base(id)
    {
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
        CityId = cityId;
    }

    private WeatherForecast(WeatherId id) : base(id) { }

    public static WeatherForecast Create(
        DateTime time,
        Temperature temperature,
        Pressure pressure,
        Humidity humidity,
        WeatherDescription weatherDescription,
        Cloudiness cloudiness,
        Wind wind,
        Visibility visibility,
        PrecipitationProbability precipitationProbability,
        PrecipitationVolume? precipitationVolume,
        CityId cityId)
    {
        return new(
            WeatherId.Create(),
            time,
            temperature,
            pressure,
            humidity,
            weatherDescription,
            cloudiness,
            wind,
            visibility,
            precipitationProbability,
            precipitationVolume,
            cityId);
    }
}