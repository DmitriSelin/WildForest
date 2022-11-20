using WildForest.Domain.Common.Models;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Weather
{
    public class DayWeatherAggregate : AggregateRoot<WeatherId>
    {
        public DayWeather DayWeather { get; }

        public Mark.Entities.Mark Mark { get; }

        public City.Entities.City City { get; }

        private DayWeatherAggregate(
            WeatherId id,
            DayWeather dayWeather,
            Mark.Entities.Mark mark,
            City.Entities.City city) : base(id) 
        {
            DayWeather = dayWeather;
            Mark = mark;
            City = city;
        }

        public static DayWeatherAggregate CreateDayWeatherAggregate(
            DayWeather dayWeather, 
            Mark.Entities.Mark mark,
            City.Entities.City city)
        {
            return new(
                WeatherId.CreateWeatherId(),
                dayWeather, mark, city);
        }
    }
}