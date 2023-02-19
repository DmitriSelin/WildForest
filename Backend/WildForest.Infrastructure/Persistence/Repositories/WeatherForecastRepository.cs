using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories
{
    public sealed class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly WildForestDbContext _context;

        public WeatherForecastRepository(WildForestDbContext context)
        {
            _context = context;
        }

        public async Task<List<WeatherForecast>> GetWeatherForecastAsync(CityId cityId, ForecastDate forecastDate)
        {
            return await _context.WeatherForecasts
                .Where(x => x.CityId.Value == cityId.Value && x.ForecastDate.Value == forecastDate.Value)
                .ToListAsync();
        }
    }
}