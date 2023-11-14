using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather;
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

    public async Task AddWeatherForecastsAsync(IEnumerable<WeatherForecast> weatherForecasts)
    {
        await _context.WeatherForecasts.AddRangeAsync(weatherForecasts);
    }

    public async Task<WeatherForecast?> GetWeatherForecastByIdAsync(WeatherForecastId weatherForecastId)
    {
        return await _context.WeatherForecasts
            .FirstOrDefaultAsync(x => x.Id == weatherForecastId);
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsWithRatingByDateAsync(DateOnly date, CityId cityId)
    {
        return await _context.WeatherForecasts
            .Include(x => x.Rating)
            .Include(x => x.ThreeHourWeatherForecasts)
            .Where(x => x.Date >= date && x.CityId == cityId)
            .ToListAsync();
    }
}