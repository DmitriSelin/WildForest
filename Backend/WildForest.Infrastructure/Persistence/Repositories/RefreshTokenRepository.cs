using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Tokens.ValueObjects;
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

    public async Task<RefreshToken?> GetTokenWithUserByValueAsync(Token token)
    {
        return await _context.RefreshTokens
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Token.Value == token.Value);
    }

    public async Task<bool> IsTokenUnique(Token token)
    {
        await Task.CompletedTask;

        return !_context.RefreshTokens
            .Any(x => x.Token.Value == token.Value);
    }

    public async Task AddTokenAsync(RefreshToken refreshToken, bool autoSaveChanges = true)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        
        if (autoSaveChanges)
            await _context.SaveChangesAsync();
    }

    public async Task RemoveOldRefreshTokensByUserIdAsync(UserId userId, bool autoSaveChanges = true)
    {
        var oldRefreshTokens = _context.RefreshTokens
            .Where(x => !x.IsActive && x.UserId == userId &&
                        x.CreationDate.Value.AddDays(2) <= DateTime.UtcNow);

        _context.RefreshTokens.RemoveRange(oldRefreshTokens);

        if (autoSaveChanges)
            await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetRefreshTokenByReplacedTokenAndUserIdAsync(
        ReplacedByToken replacedByToken, 
        UserId userId)
    {
        return await _context.RefreshTokens
            .SingleOrDefaultAsync(x => x.Token.Value == replacedByToken.Value && x.UserId == userId);
    }

    public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);

        await _context.SaveChangesAsync();
    }
}