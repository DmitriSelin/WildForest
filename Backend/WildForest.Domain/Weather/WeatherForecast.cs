using WildForest.Domain.Common.Models;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Weather;

public class WeatherForecast : AggregateRoot<WeatherForecastId>
{
    private WeatherForecast(WeatherForecastId id) : base(id) { }
}