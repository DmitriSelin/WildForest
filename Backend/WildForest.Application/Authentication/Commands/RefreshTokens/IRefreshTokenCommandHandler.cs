using WildForest.Application.Authentication.Common;
using ErrorOr;

namespace WildForest.Application.Authentication.Commands.RefreshTokens;

public interface IRefreshTokenCommandHandler
{
    public Task<ErrorOr<AuthenticationResult>> RefreshTokenAsync(RefreshTokenCommand command);
}