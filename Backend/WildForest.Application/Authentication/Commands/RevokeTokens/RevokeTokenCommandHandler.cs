using ErrorOr;
using WildForest.Application.Authentication.Common.Extensions;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Tokens.ValueObjects;

namespace WildForest.Application.Authentication.Commands.RevokeTokens;

public sealed class RevokeTokenCommandHandler : IRevokeTokenCommandHandler
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RevokeTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<ErrorOr<string>> RevokeRefreshTokenAsync(RevokeTokenCommand command)
    {
        var token = Token.Create(command.Token);
        var refreshToken = await _refreshTokenRepository.GetTokenWithUserByValueAsync(token);

        if (refreshToken is null)
        {
            return Errors.RefreshToken.NotFound;
        }

        if (!refreshToken.IsActive)
        {
            return Errors.RefreshToken.InvalidToken;
        }

        var createdByIp = CreatedByIp.Create(command.IpAddress);
        
        refreshToken.RevokeRefreshToken(createdByIp, "Revoke without replacement");
        await _refreshTokenRepository.UpdateRefreshTokenAsync(refreshToken);

        var result = "Token revoked";
        return result;
    }
}