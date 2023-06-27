using ErrorOr;
using WildForest.Application.Authentication.Common.Extensions;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Common.Errors;

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
        var refreshToken = await _refreshTokenRepository.GetTokenWithUserByValueAsync(command.Token);

        if (refreshToken is null)
        {
            return Errors.RefreshToken.NotFound;
        }

        if (!refreshToken.IsActive)
        {
            return Errors.RefreshToken.InvalidToken;
        }

        refreshToken.RevokeRefreshToken(command.IpAddress, "Revoke without replacement");
        await _refreshTokenRepository.UpdateRefreshTokenAsync(refreshToken);

        return "Token revoked";
    }
}