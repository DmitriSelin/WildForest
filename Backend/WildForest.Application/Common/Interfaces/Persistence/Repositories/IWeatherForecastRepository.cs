using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories
{
    public interface IWeatherForecastRepository
    {
        Task<List<WeatherForecast>> GetWeatherForecastAsync(CityId cityId, DateOnly date);
    }
}