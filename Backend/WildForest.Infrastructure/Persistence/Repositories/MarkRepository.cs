using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Marks.Entities;
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
}