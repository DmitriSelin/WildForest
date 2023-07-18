using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Weather.Entities;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class ThreeHourWeatherForecastRepository : IThreeHourWeatherForecastRepository
{
    private readonly WildForestDbContext _context;

    public ThreeHourWeatherForecastRepository(WildForestDbContext context)
    {
        _context = context;
    }

    public async Task AddWeatherForecastsAsync(IEnumerable<ThreeHourWeatherForecast> weatherForecasts)
    {
        await _context.ThreeHourWeatherForecasts.AddRangeAsync(weatherForecasts);
        await _context.SaveChangesAsync();
    }
}