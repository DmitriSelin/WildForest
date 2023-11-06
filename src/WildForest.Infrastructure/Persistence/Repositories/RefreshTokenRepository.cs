using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Users.ValueObjects;
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

    public async Task AddTokenAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }

    public async Task RemoveOldRefreshTokensByUserIdAsync(UserId userId)
    {
        await Task.Run(() => RemoveOldRefreshTokensByUserId(userId));
    }

    private void RemoveOldRefreshTokensByUserId(UserId userId)
    {
        var utcNow = DateTime.UtcNow;

        var oldRefreshTokens = _context.RefreshTokens
            .Where(x => (x.RevokedDate! != null! || utcNow >= x.Expiration) && x.UserId == userId &&
                        x.CreationDate.AddDays(2) <= utcNow);

        _context.RefreshTokens.RemoveRange(oldRefreshTokens);
    }

    public async Task<RefreshToken?> GetRefreshTokenByReplacedTokenAndUserIdAsync(
        string replacedByToken, UserId userId)
    {
        return await _context.RefreshTokens
            .SingleOrDefaultAsync(x => x.Token == replacedByToken && x.UserId == userId);
    }

    public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
    {
        await Task.Run(() => _context.RefreshTokens.Update(refreshToken));
    }
}