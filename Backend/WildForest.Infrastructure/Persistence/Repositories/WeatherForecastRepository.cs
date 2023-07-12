using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly WildForestDbContext _context;

    public WeatherForecastRepository(WildForestDbContext context)
    {
        _context = context;
    }

    public async Task AddWeatherForecastsAsync(IEnumerable<ThreeHourWeatherForecast> forecasts)
    {
        await _context.ThreeHourWeatherForecasts.AddRangeAsync(forecasts);
        await _context.SaveChangesAsync();
    }

    public async Task<ThreeHourWeatherForecast?> GetWeatherForecastByIdAsync(WeatherForecastId weatherId)
    {
        return await _context.ThreeHourWeatherForecasts
            .FirstOrDefaultAsync(x => x.Id == weatherId);
    }

    public async Task<IEnumerable<ThreeHourWeatherForecast>?> GetWeatherForecastsByCityIdAsync(CityId cityId)
    {
        return await _context.ThreeHourWeatherForecasts
            .Where(x => x.CityId == cityId)
            .ToListAsync();
    }
}