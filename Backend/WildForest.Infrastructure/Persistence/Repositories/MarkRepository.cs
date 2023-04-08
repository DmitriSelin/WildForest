using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Marks.Entities;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class MarkRepository : IMarkRepository
{
    private readonly WildForestDbContext _context;

    public MarkRepository(WildForestDbContext context)
    {
        _context = context;
    }

    public async Task AddMarkAsync(Mark mark)
    {
        await _context.Marks.AddAsync(mark);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Mark>?> GetMarksWithCommentsByWeatherIdAsync(WeatherId weatherId)
    {
        return await _context.Marks
            .Include(x => x.WeatherForecast)
            .Include(x => x.User)
            .Where(x => x.WeatherId == weatherId && x.Comment! != null!)
            .ToListAsync();
    }
}