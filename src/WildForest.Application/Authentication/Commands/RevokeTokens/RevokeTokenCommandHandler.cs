using ErrorOr;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Domain.Common.Errors;

namespace WildForest.Application.Authentication.Commands.RevokeTokens;

public sealed class RevokeTokenCommandHandler : IRevokeTokenCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public RevokeTokenCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<string>> RevokeRefreshTokenAsync(RevokeTokenCommand command)
    {
        var refreshToken = await _unitOfWork.RefreshTokenRepository.GetTokenWithUserByValueAsync(command.Token);

        if (refreshToken is null)
        {
            return Errors.RefreshToken.NotFound;
        }

        if (!refreshToken.IsActive)
        {
            return Errors.RefreshToken.InvalidToken;
        }

        refreshToken.Revoke(command.IpAddress, "Revoke without replacement");
        _unitOfWork.RefreshTokenRepository.Update(refreshToken);
        await _unitOfWork.SaveChangesAsync();

        return "Token revoked";
    }
}