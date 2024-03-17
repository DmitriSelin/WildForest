using WildForest.Application.Common.Interfaces.Persistence.Base;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IWeatherForecastRepository : IRepository<WeatherForecast>
{
    Task AddWeatherForecastsAsync(IEnumerable<WeatherForecast> weatherForecasts);

    Task<IEnumerable<WeatherForecast>> GetWeatherForecastsWithRatingByDateAsync(
        DateOnly date, CityId cityId);

    Task<WeatherForecast?> GetWeatherForecastByIdAsync(WeatherForecastId weatherForecastId);
}