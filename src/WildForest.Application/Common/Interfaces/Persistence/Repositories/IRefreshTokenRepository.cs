using WildForest.Application.Common.Interfaces.Persistence.Base;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetTokenWithUserByValueAsync(string token);

    Task<bool> IsTokenUnique(string token);

    Task AddTokenAsync(RefreshToken refreshToken);

    Task RemoveOldRefreshTokensByUserIdAsync(UserId userId);

    Task<RefreshToken?> GetRefreshTokenByReplacedTokenAndUserIdAsync(string replacedByToken, UserId userId);

    Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
}