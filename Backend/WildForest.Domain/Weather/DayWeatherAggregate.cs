using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Marks.Entities;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Weather
{
    public class DayWeatherAggregate : AggregateRoot<WeatherId>
    {
        public DayWeather DayWeather { get; }

        public Mark Mark { get; }

        public City City { get; }

        private DayWeatherAggregate(
            WeatherId id,
            DayWeather dayWeather,
            Mark mark,
            City city) : base(id) 
        {
            DayWeather = dayWeather;
            Mark = mark;
            City = city;
        }

        public static DayWeatherAggregate CreateDayWeatherAggregate(
            DayWeather dayWeather, 
            Mark mark,
            City city)
        {
            return new(
                WeatherId.CreateWeatherId(),
                dayWeather, mark, city);
        }
    }
}