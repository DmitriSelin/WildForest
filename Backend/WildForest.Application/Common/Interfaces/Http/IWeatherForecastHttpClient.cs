using WildForest.Application.Weather.Common.JsonModels;
using WildForest.Domain.Cities.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Http;

public interface IWeatherForecastHttpClient
{
    Task<List<WeatherForecastVm>> GetWeatherForecastAsync(CityId cityId);
}