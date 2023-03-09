using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Tokens.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetTokenWithUserByValueAsync(Token token);

    Task<bool> IsTokenUnique(Token token);

    Task AddTokenAsync(RefreshToken refreshToken, bool autoSaveChanges = true);

    Task RemoveOldRefreshTokensByUserIdAsync(UserId userId, bool autoSaveChanges = true);

    Task<RefreshToken?> GetRefreshTokenByReplacedTokenAndUserIdAsync(ReplacedByToken replacedByToken, UserId userId);

    Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
}