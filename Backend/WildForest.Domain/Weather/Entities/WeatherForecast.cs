using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Weather.Entities
{
    public class WeatherForecast : Entity<WeatherId>
    {
        public ForecastDateTime ForecastDateTime { get; } = null!;

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

        public virtual City City { get; } = null!;

        private WeatherForecast(
            WeatherId id,
            ForecastDateTime forecastDateTime,
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
            ForecastDateTime = forecastDateTime;
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

        public static WeatherForecast CreateWeatherForecast(
            ForecastDateTime forecastDateTime,
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
                WeatherId.CreateWeatherId(),
                forecastDateTime,
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