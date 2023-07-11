using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Authentication.Common.Extensions;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Clients.Users.Entities;
using WildForest.Domain.Clients.ValueObjects;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Tokens.Entities;

namespace WildForest.Application.Authentication.Commands.RefreshTokens;

public sealed class RefreshTokenCommandHandler : IRefreshTokenCommandHandler
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RefreshTokenCommandHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IRefreshTokenGenerator refreshTokenGenerator,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _refreshTokenGenerator = refreshTokenGenerator;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult<User>>> RefreshTokenAsync(RefreshTokenCommand command)
    {
        var refreshToken = await _refreshTokenRepository.GetTokenWithUserByValueAsync(command.Token);

        if (refreshToken is null)
            return Errors.RefreshToken.NotFound;

        if (refreshToken.IsRevoked)
        {
            await RevokeDescendantRefreshTokens(refreshToken, refreshToken.User, command.IpAddress,
                $"Attempted reuse of revoked ancestor token: {command.Token}");
            await _refreshTokenRepository.UpdateRefreshTokenAsync(refreshToken);
        }

        if (!refreshToken.IsActive)
            return Errors.RefreshToken.InvalidToken;

        var newRefreshToken = await ReplaceRefreshTokenAsync(refreshToken.UserId, refreshToken, command.IpAddress);

        await _refreshTokenRepository.AddTokenAsync(newRefreshToken, false);
        await _refreshTokenRepository.RemoveOldRefreshTokensByUserIdAsync(newRefreshToken.UserId);

        var jwt = _jwtTokenGenerator.GenerateToken(refreshToken.User);

        return new AuthenticationResult<User>(refreshToken.User, jwt, newRefreshToken.Token);
    }

    private async Task<RefreshToken> ReplaceRefreshTokenAsync(PersonId userId, RefreshToken token, string createdByIp)
    {
        var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(userId, createdByIp);
        token.Update(createdByIp, "Replaced by new token", refreshToken.Token);

        return refreshToken;
    }

    private async Task RevokeDescendantRefreshTokens(
        RefreshToken refreshToken,
        User user,
        string createdByIp,
        string reason)
    {
        if (!string.IsNullOrEmpty(refreshToken?.ReplacedByToken))
        {
            var childToken = await _refreshTokenRepository.GetRefreshTokenByReplacedTokenAndUserIdAsync(
                refreshToken.ReplacedByToken, user.Id);

            if (childToken!.IsActive)
            {
                childToken.RevokeRefreshToken(createdByIp, reason);
            }
            else
            {
                await RevokeDescendantRefreshTokens(childToken, user, createdByIp, reason);
            }
        }
    }
}