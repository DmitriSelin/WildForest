using WildForest.Application.Weather.Common.Models;
using WildForest.Domain.Weather;

namespace WildForest.Application.Weather.Queries.GetHomeWeatherForecast.Fabrics;

public interface IWeatherForecastResponseFactory
{
    List<WeatherForecastResponse> Create(List<WeatherForecast> forecasts);
}