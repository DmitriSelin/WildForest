using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Weather.Entities
{
    public class WeatherForecast : Entity<WeatherId>
    {
        public DateOnly Date { get; }

        public TimeOnly Time { get; }

        public Temperature Temperature { get; }

        public Pressure Pressure { get; }

        public Humidity Humidity { get; }

        public WeatherDescription WeatherDescription { get; }

        public Cloudiness Cloudiness { get; }

        public Wind Wind { get; }

        public Visibility Visibility { get; }

        public PrecipitationProbability PrecipitationProbability { get; }

        public PrecipitationVolume? PrecipitationVolume { get; }

        public CityId CityId { get; } = null!;

        public virtual City City { get; } = null!;

        private WeatherForecast(
            WeatherId id,
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