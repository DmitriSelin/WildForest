using ErrorOr;
using WildForest.Application.Weather.Common;
using WildForest.Application.Weather.Common.JsonModels;

namespace WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

public interface IHomeWeatherForecastHandler
{
    Task<ErrorOr<WeatherForecastVm>> GetWeatherForecastAsync(HomeWeatherForecastQuery query);
}