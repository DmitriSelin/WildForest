using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Weather.ValueObjects;
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

    public async Task<WeatherMark?> GetWeatherMarkByWeatherIdAsync(WeatherId weatherId)
    {
        return await _context.WeatherMarks.SingleOrDefaultAsync(x => x.WeatherId == weatherId);
    }

    public async Task UpdateWeatherMarkAsync(WeatherMark weatherMark)
    {
        _context.WeatherMarks.Update(weatherMark);
        await _context.SaveChangesAsync();
    }
}