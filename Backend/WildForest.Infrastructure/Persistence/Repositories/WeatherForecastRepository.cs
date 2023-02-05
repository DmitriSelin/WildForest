using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Infrastructure.Context;

namespace WildForest.Infrastructure.Persistence.Repositories
{
    public sealed class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly WildForestDbContext _context;

        public WeatherForecastRepository(WildForestDbContext context)
        {
            _context = context;
        }

        public async Task<List<WeatherForecast>> GetWeatherForecastAsync(CityId cityId, DateOnly date)
        {
            return await _context.WeatherForecasts
                .Where(x => x.CityId == cityId && x.Date == date)
                .ToListAsync();
        }
    }
}