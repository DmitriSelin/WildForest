using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Infrastructure.Context;

namespace WildForest.Infrastructure.Persistence
{
    public sealed class DayWeatherRepository : IDayWeatherRepository
    {
        private readonly WildForestDbContext _context;

        public DayWeatherRepository(WildForestDbContext context)
        {
            _context = context;
        }

        public async Task<List<DayWeather>> GetWeatherAsync(CityId cityId, DateTime date)
        {
            return await _context.DayWeather
                .Where(x => x.CityId == cityId && x.Date == date)
                .ToListAsync();
        }
    }
}