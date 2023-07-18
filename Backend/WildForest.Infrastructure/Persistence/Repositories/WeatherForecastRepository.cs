using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
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
        
    }

    public async Task<ThreeHourWeatherForecast?> GetWeatherForecastByIdAsync(WeatherForecastId weatherId)
    {
        throw new();
    }
}