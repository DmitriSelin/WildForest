using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Application.Common.Interfaces.Http;

public interface IWeatherForecastHttpClient
{
    Task<List<WeatherForecast>> GetWeatherForecastAsync(CityId cityId);
}