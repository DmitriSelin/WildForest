using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Marks.Entities;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Domain.Weather
{
    public class WeatherAggregate : AggregateRoot<WeatherId>
    {
        public WeatherForecast WeatherForecast { get; }

        public Mark Mark { get; }

        public City City { get; }

        private WeatherAggregate(
            WeatherId id,
            WeatherForecast weatherForecast,
            Mark mark,
            City city) : base(id) 
        {
            WeatherForecast = weatherForecast;
            Mark = mark;
            City = city;
        }

        public static WeatherAggregate Create(
            WeatherForecast dayWeather, 
            Mark mark,
            City city)
        {
            return new(
                WeatherId.Create(),
                dayWeather, mark, city);
        }
    }
}