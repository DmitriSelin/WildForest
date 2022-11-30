using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Weather.Entities
{
    public class DayWeather : Entity<WeatherId>
    {
        public DateOnly Date { get; }

        public DaySpan DaySpan { get; }

        public CityId CityId { get; }

        public virtual City City { get; set; } = null!;

        public WeatherDetails WeatherDetails { get; }

        public DayWeather(
            WeatherId id,
            DateOnly date,
            DaySpan daySpan,
            CityId cityId,
            WeatherDetails weatherDetails) : base(id)
        {
            Date = date;
            DaySpan = daySpan;
            CityId = cityId;
            WeatherDetails = weatherDetails;
        }
    }
}