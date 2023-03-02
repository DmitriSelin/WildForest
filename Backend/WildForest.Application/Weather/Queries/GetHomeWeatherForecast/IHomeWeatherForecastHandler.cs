using ErrorOr;
using WildForest.Application.Weather.Common.Models;

namespace WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

public interface IHomeWeatherForecastHandler
{
    Task<ErrorOr<List<WeatherForecastDto>>> GetWeatherForecastAsync(HomeWeatherForecastQuery query);
}