using ErrorOr;
using WildForest.Application.Weather.Common.Models;

namespace WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

public interface IHomeWeatherForecastService
{
    Task<ErrorOr<List<WeatherForecastResponse>>> GetWeatherForecastsAsync(HomeWeatherForecastQuery query);
}