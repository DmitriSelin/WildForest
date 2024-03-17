using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Authentication.Commands.RefreshTokens;

public sealed class RefreshTokenCommandHandler : IRefreshTokenCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RefreshTokenCommandHandler(
        IUnitOfWork unitOfWork,
        IRefreshTokenGenerator refreshTokenGenerator,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _refreshTokenGenerator = refreshTokenGenerator;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> RefreshTokenAsync(RefreshTokenCommand command)
    {
        var refreshToken = await _unitOfWork.RefreshTokenRepository.GetTokenWithUserByValueAsync(command.Token);

        if (refreshToken is null)
            return Errors.RefreshToken.NotFound;

        if (refreshToken.IsRevoked)
        {
            await RevokeDescendantRefreshTokens(refreshToken, refreshToken.User, command.IpAddress,
                $"Attempted reuse of revoked ancestor token: {command.Token}");
            _unitOfWork.RefreshTokenRepository.Update(refreshToken);
        }

        if (!refreshToken.IsActive)
            return Errors.RefreshToken.InvalidToken;

        var newRefreshToken = await ReplaceRefreshTokenAsync(refreshToken.UserId, refreshToken, command.IpAddress);

        await _unitOfWork.RefreshTokenRepository.AddAsync(newRefreshToken);
        await _unitOfWork.RefreshTokenRepository.RemoveOldRefreshTokensByUserIdAsync(newRefreshToken.UserId);
        await _unitOfWork.SaveChangesAsync();

        var jwt = _jwtTokenGenerator.GenerateToken(refreshToken.User);

        return new AuthenticationResult(refreshToken.User, jwt, newRefreshToken.Token);
    }

    private async Task<RefreshToken> ReplaceRefreshTokenAsync(UserId userId, RefreshToken token, string createdByIp)
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
        if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
        {
            var childToken = await _unitOfWork.RefreshTokenRepository.GetRefreshTokenByReplacedTokenAndUserIdAsync(
                refreshToken.ReplacedByToken, user.Id);

            if (childToken!.IsActive)
            {
                childToken.Revoke(createdByIp, reason);
            }
            else
            {
                await RevokeDescendantRefreshTokens(childToken, user, createdByIp, reason);
            }
        }
    }
}