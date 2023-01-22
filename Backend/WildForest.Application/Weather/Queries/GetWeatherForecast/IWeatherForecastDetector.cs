using ErrorOr;
using WildForest.Application.Weather.Common;

namespace WildForest.Application.Weather.Queries.GetWeatherForecast
{
    public interface IWeatherForecastDetector
    {
        Task<ErrorOr<List<WeatherForecastDto>>> GetWeatherForecast(ForecastQuery query);
    }
}