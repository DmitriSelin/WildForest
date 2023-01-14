using ErrorOr;
using WildForest.Application.Weather.Common;

namespace WildForest.Application.Weather.Queries.GetTodayForecast
{
    public interface IWeatherForecastDetector
    {
        Task<ErrorOr<List<WeatherForecust>>> GetTodayWeatherForecast(ForecastQuery query);
    }
}