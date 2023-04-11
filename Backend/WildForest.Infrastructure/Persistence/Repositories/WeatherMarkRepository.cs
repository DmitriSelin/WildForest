using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.WeatherMarks.Entities;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class WeatherMarkRepository : IWeatherMarkRepository
{
    private readonly WildForestDbContext _context;

    public WeatherMarkRepository(WildForestDbContext context)
    {
        _context = context;
    }

    public async Task AddWeatherMarkAsync(WeatherMark weatherMark)
    {
        await _context.WeatherMarks.AddAsync(weatherMark);
        await _context.SaveChangesAsync();
    }
}