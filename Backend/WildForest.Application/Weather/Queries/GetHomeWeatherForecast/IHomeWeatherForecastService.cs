using ErrorOr;
using WildForest.Application.Weather.Common.Models;

namespace WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

public interface IHomeWeatherForecastService
{
    Task<ErrorOr<WeatherForecastDto>> GetWeatherForecastsAsync(HomeWeatherForecastQuery query);
}