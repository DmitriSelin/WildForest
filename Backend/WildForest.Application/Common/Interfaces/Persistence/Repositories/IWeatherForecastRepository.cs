using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories
{
    public interface IWeatherForecastRepository
    {
        Task<IEnumerable<WeatherForecast>?> GetWeatherForecastsWithMarkByDateAsync(CityId cityId, ForecastDate forecastDate);

        Task AddWeatherForecastsAsync(IEnumerable<WeatherForecast> forecasts);

        Task<WeatherForecast?> GetWeatherForecastByIdAsync(WeatherId weatherId);
    }
}