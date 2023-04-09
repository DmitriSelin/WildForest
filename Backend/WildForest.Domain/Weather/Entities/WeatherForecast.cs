using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Marks.Entities;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Domain.WeatherMarks.Entities;

namespace WildForest.Domain.Weather.Entities
{
    public sealed class WeatherForecast : Entity<WeatherId>
    {
        public ForecastDate ForecastDate { get; } = null!;

        public ForecastTime ForecastTime { get; } = null!;

        public Temperature Temperature { get; } = null!;

        public Pressure Pressure { get; } = null!;

        public Humidity Humidity { get; } = null!;

        public WeatherDescription WeatherDescription { get; } = null!;

        public Cloudiness Cloudiness { get; } = null!;

        public Wind Wind { get; } = null!;

        public Visibility Visibility { get; } = null!;

        public PrecipitationProbability PrecipitationProbability { get; } = null!;

        public PrecipitationVolume? PrecipitationVolume { get; } = null!;

        public CityId CityId { get; } = null!;

        public City City { get; } = null!;

        private readonly List<Mark> _marks = new();

        public IReadOnlyList<Mark> Marks => _marks.AsReadOnly();

        public WeatherMark WeatherMark { get; } = null!;

        private WeatherForecast(
            WeatherId id,
            ForecastDate forecastDate,
            ForecastTime forecastTime,
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
            ForecastDate = forecastDate;
            ForecastTime = forecastTime;
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
            ForecastDate forecastDate,
            ForecastTime forecastTime,
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
                forecastDate,
                forecastTime,
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
}