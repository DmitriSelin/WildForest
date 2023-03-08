using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Tokens.ValueObjects;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

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

    public async Task<ErrorOr<AuthenticationResult>> RefreshTokenAsync(RefreshTokenCommand command)
    {
        var token = Token.Create(command.Token);
        var iPAddress = CreatedByIp.Create(command.IpAddress);
        
        var refreshToken = await _refreshTokenRepository.GetTokenWithUserByValueAsync(token);

        if (refreshToken is null)
            return Errors.RefreshToken.NotFound;

        if (refreshToken.IsRevoked)
        {
            await RevokeDescendantRefreshTokens(refreshToken, refreshToken.User, iPAddress, 
                $"Attempted reuse of revoked ancestor token: {token.Value}");
            await _refreshTokenRepository.SaveChangesAsync();
        }

        if (!refreshToken.IsActive)
            return Errors.RefreshToken.InvalidToken;

        var newRefreshToken = await ReplaceRefreshTokenAsync(refreshToken.UserId, refreshToken, iPAddress);

        await _refreshTokenRepository.AddTokenAsync(newRefreshToken, false);
        await _refreshTokenRepository.RemoveOldRefreshTokensByUserIdAsync(newRefreshToken.UserId);

        var jwt = _jwtTokenGenerator.GenerateToken(refreshToken.User);

        return new AuthenticationResult(refreshToken.User, jwt, newRefreshToken.Token.Value);
    }

    private async Task<RefreshToken> ReplaceRefreshTokenAsync(UserId userId, RefreshToken token, CreatedByIp createdByIp)
    {
        var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(userId, createdByIp);
        RevokeRefreshToken(token, createdByIp, "Replaced by new token", refreshToken.Token.Value);

        return refreshToken;
    }

    private async Task RevokeDescendantRefreshTokens(
        RefreshToken refreshToken,
        User user,
        CreatedByIp createdByIp,
        string reason)
    {
        if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken.Value))
        {
            var childToken = await _refreshTokenRepository.GetRefreshTokenByReplacedTokenAndUserIdAsync(
                refreshToken.ReplacedByToken, user.Id);

            if (childToken!.IsActive)
            {
                RevokeRefreshToken(childToken, createdByIp, reason);
            }
            else
            {
                await RevokeDescendantRefreshTokens(childToken, user, createdByIp, reason);
            }
        }
    }

    private void RevokeRefreshToken(
        RefreshToken token,
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