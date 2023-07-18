using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Weather;
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
        await _context.SaveChangesAsync();
    }
}