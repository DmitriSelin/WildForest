using WildForest.Application.Weather.Common;

namespace WildForest.Application.Weather.Queries.GetTodayForecast
{
    public interface IWeatherForecastDetector
    {
        Task<WeatherForecust> GetTodayWeatherForecast(TodayForecastQuery query);
    }
}