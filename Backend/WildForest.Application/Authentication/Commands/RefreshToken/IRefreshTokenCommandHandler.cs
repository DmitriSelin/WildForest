using WildForest.Application.Authentication.Common;

namespace WildForest.Application.Authentication.Commands.RefreshToken;

public interface IRefreshTokenCommandHandler
{
    public Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenCommand command);
}