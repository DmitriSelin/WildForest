using WildForest.Domain.Weather;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IWeatherForecastRepository
{
    Task AddWeatherForecastsAsync(IEnumerable<WeatherForecast> weatherForecasts);
}