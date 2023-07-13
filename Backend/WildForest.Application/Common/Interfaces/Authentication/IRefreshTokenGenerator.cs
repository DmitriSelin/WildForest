using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Authentication;

public interface IRefreshTokenGenerator
{
    Task<RefreshToken> GenerateTokenAsync(UserId personId, string createdByIp);
}