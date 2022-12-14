using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Weather.Entities
{
    public class DayWeather : Entity<WeatherId>
    {
        public DateTime Date { get; }

        public DaySpan DaySpan { get; }

        public WeatherDetails WeatherDetails { get; } = null!;

        public CityId CityId { get; } = null!;

        public virtual City City { get; } = null!;

        private DayWeather(
            WeatherId id,
            DateTime date,
            DaySpan daySpan,
            WeatherDetails weatherDetails,
            CityId cityId) : base(id)
        {
            Date = date;
            DaySpan = daySpan;
            WeatherDetails = weatherDetails;
            CityId = cityId;
        }

        private DayWeather(WeatherId id) : base(id) { }

        public static DayWeather CreateDayWeather(
            DateTime date,
            DaySpan daySpan,
            WeatherDetails weatherDetails,
            CityId cityId)
        {
            return new(
                WeatherId.CreateWeatherId(),
                date,
                daySpan,
                weatherDetails,
                cityId);
        }

        public static DayWeather CreateDayWeather() 
            => new(WeatherId.CreateWeatherId());
    }
}