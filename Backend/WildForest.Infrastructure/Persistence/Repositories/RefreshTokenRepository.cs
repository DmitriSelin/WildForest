using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Clients.ValueObjects;
using WildForest.Domain.Tokens.Entities;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly WildForestDbContext _context;

    public RefreshTokenRepository(WildForestDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetTokenWithUserByValueAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Token == token);
    }

    public async Task<bool> IsTokenUnique(string token)
    {
        await Task.CompletedTask;

        return !_context.RefreshTokens
            .Any(x => x.Token == token);
    }

    public async Task AddTokenAsync(RefreshToken refreshToken, bool autoSaveChanges = true)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);

        if (autoSaveChanges)
            await _context.SaveChangesAsync();
    }

    public async Task RemoveOldRefreshTokensByUserIdAsync(PersonId userId, bool autoSaveChanges = true)
    {
        var utcNow = DateTime.UtcNow;

        var oldRefreshTokens = _context.RefreshTokens
            .Where(x => (x.RevokedDate! != null! || utcNow >= x.Expiration) && x.UserId == userId &&
                        x.CreationDate.AddDays(2) <= utcNow);

        _context.RefreshTokens.RemoveRange(oldRefreshTokens);

        if (autoSaveChanges)
            await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetRefreshTokenByReplacedTokenAndUserIdAsync(
        string replacedByToken, PersonId userId)
    {
        return await _context.RefreshTokens
            .SingleOrDefaultAsync(x => x.Token == replacedByToken && x.UserId == userId);
    }

    public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
        await _context.SaveChangesAsync();
    }
}