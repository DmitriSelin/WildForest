using Microsoft.EntityFrameworkCore;
using System.Linq;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Infrastructure.Context;

namespace WildForest.Infrastructure.Persistence
{
    public class DayWeatherRepository : IDayWeatherRepository
    {
        private readonly WildForestDbContext _context;

        public DayWeatherRepository(WildForestDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<DayWeather>> GetWeatherAsync(CityId cityId, DateOnly date)
        {
            return _context.DayWeather
                .Where(x => x.CityId == cityId && x.Date == date);
        }
    }
}