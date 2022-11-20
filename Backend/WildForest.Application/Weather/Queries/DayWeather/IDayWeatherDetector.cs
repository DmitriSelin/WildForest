using WildForest.Application.Weather.Common;

namespace WildForest.Application.Weather.Queries.DayWeather
{
    public interface IDayWeatherDetector
    {
        public WeatherForecust GetWeather(DayWeatherQuery query);
    }
}