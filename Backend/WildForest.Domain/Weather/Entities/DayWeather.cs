using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Weather.Entities
{
    public class DayWeather : Entity<WeatherId>
    {
        public DateOnly Date { get; }

        public DaySpan DaySpan { get; }

        public WeatherDetails WeatherDetails { get; }

        public DayWeather(WeatherId id, WeatherDetails weatherDetails) : base(id)
        {
            WeatherDetails = weatherDetails;
        }
    }
}