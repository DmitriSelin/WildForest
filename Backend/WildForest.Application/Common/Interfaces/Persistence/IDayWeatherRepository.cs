using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Application.Common.Interfaces.Persistence
{
    public interface IDayWeatherRepository
    {
        Task<IQueryable<DayWeather>> GetWeatherAsync(CityId cityId, DateOnly date);
    }
}