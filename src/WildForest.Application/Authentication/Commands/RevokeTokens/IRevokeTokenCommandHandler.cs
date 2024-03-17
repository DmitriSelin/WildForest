using ErrorOr;

namespace WildForest.Application.Authentication.Commands.RevokeTokens;

public interface IRevokeTokenCommandHandler
{
    Task<ErrorOr<string>> RevokeRefreshTokenAsync(RevokeTokenCommand command);
}