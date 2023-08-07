using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Marks;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
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

    public async Task<Mark?> GetMarkByIdWithVotesByUserIdAsync(MarkId markId, UserId userId)
    {
        return await _context.Marks
            .Include(x => x.Votes.Where(a => a.UserId == userId))
            .SingleOrDefaultAsync(x => x.Id == markId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}