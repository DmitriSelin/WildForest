using WildForest.Domain.Clients.ValueObjects;
using WildForest.Domain.Tokens.Entities;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetTokenWithUserByValueAsync(string token);

    Task<bool> IsTokenUnique(string token);

    Task AddTokenAsync(RefreshToken refreshToken, bool autoSaveChanges = true);

    Task RemoveOldRefreshTokensByUserIdAsync(PersonId userId, bool autoSaveChanges = true);

    Task<RefreshToken?> GetRefreshTokenByReplacedTokenAndUserIdAsync(string replacedByToken, PersonId userId);

    Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
}