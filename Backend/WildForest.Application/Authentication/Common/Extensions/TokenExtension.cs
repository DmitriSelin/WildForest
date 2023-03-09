using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Tokens.ValueObjects;

namespace WildForest.Application.Authentication.Common.Extensions;

public static class TokenExtension
{
    public static void RevokeRefreshToken(
        this RefreshToken token,
        CreatedByIp createdByIp,
        string reasonRevoked,
        string? replacedByToken = null)
    {
        token.Revoked.Update(DateTime.UtcNow);
        token.RevokedByIp.Update(createdByIp.Value);
        token.ReasonRevoked.Update(reasonRevoked);
        
        if (replacedByToken is not null)
            token.ReplacedByToken.Update(replacedByToken);
    }
}