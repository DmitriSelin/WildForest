using WildForest.Application.Weather.Common.JsonModels;
using WildForest.Domain.Cities.Entities;

namespace WildForest.Application.Common.Interfaces.Http;

public interface IWeatherForecastHttpClient
{
    Task<List<WeatherForecastVm>> GetWeatherForecastAsync(City city);
}