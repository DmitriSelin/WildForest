using ErrorOr;
using WildForest.Application.Weather.Common;

namespace WildForest.Application.Weather.Queries.GetWeatherForecast
{
    public interface IWeatherForecastDetector
    {
        Task<ErrorOr<List<WeatherForecustDto>>> GetWeatherForecast(ForecastQuery query);
    }
}