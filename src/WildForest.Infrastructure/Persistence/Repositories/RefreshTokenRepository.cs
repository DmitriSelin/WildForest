using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Infrastructure.Persistence.Repositories.Base;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(WildForestDbContext context) : base(context) { }

    public async Task<RefreshToken?> GetTokenWithUserByValueAsync(string token)
    {
        return await Context.RefreshTokens
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Token == token);
    }

    public async Task<bool> IsTokenUnique(string token)
    {
        await Task.CompletedTask;

        return !Context.RefreshTokens
            .Any(x => x.Token == token);
    }

    public async Task RemoveOldRefreshTokensByUserIdAsync(UserId userId)
    {
        await Task.Run(() => RemoveOldRefreshTokensByUserId(userId));
    }

    private void RemoveOldRefreshTokensByUserId(UserId userId)
    {
        var utcNow = DateTime.UtcNow;

        var oldRefreshTokens = Context.RefreshTokens
            .Where(x => (x.RevokedDate! != null! || utcNow >= x.Expiration) && x.UserId == userId &&
                        x.CreationDate.AddDays(2) <= utcNow);

        Context.RefreshTokens.RemoveRange(oldRefreshTokens);
    }

    public async Task<RefreshToken?> GetRefreshTokenByReplacedTokenAndUserIdAsync(
        string replacedByToken, UserId userId)
    {
        return await Context.RefreshTokens
            .SingleOrDefaultAsync(x => x.Token == replacedByToken && x.UserId == userId);
    }
}