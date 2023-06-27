using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IWeatherForecastRepository
{
    Task AddWeatherForecastsAsync(IEnumerable<WeatherForecast> forecasts);

    Task<WeatherForecast?> GetWeatherForecastByIdAsync(WeatherForecastId weatherId);

    Task<IEnumerable<WeatherForecast>?> GetWeatherForecastsByCityIdAsync(CityId cityId);
}