using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Tokens.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Authentication;

public interface IRefreshTokenGenerator
{
    Task<RefreshToken> GenerateTokenAsync(UserId userId, CreatedByIp createdByIp);
}