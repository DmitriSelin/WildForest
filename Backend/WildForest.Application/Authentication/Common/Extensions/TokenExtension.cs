using WildForest.Domain.Tokens.Entities;

namespace WildForest.Application.Authentication.Common.Extensions;

public static class TokenExtension
{
    public static void RevokeRefreshToken(
        this RefreshToken token,
        string revokedByIp,
        string reasonRevoked,
        string? replacedByToken = null)
    {
        if (replacedByToken == null)
        {
            token.Update(revokedByIp, reasonRevoked);
        }
        else
        {
            token.Update(revokedByIp, reasonRevoked, replacedByToken);
        }
    }
}