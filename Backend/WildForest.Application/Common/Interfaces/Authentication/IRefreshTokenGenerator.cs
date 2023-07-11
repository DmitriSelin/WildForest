using WildForest.Domain.Clients.ValueObjects;
using WildForest.Domain.Tokens.Entities;

namespace WildForest.Application.Common.Interfaces.Authentication;

public interface IRefreshTokenGenerator
{
    Task<RefreshToken> GenerateTokenAsync(PersonId personId, string createdByIp);
}