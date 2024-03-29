using WildForest.Application.Common.Interfaces.Persistence.Repositories.Base;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetTokenWithUserByValueAsync(string token);

    Task<bool> IsTokenUnique(string token);

    Task RemoveOldRefreshTokensByUserIdAsync(UserId userId);

    Task<RefreshToken?> GetRefreshTokenByReplacedTokenAndUserIdAsync(string replacedByToken, UserId userId);
}