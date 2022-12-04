using WildForest.Domain.Weather.Entities;

namespace WildForest.Application.Weather.Common
{
    public record WeatherForecust(
        DayWeather WeatherForecast);   //TODO: Add another properties: user marks, comments 
}