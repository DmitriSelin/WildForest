using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Domain.Clients.Users.Entities;

namespace WildForest.Application.Authentication.Commands.RefreshTokens;

public interface IRefreshTokenCommandHandler
{
    public Task<ErrorOr<AuthenticationResult<User>>> RefreshTokenAsync(RefreshTokenCommand command);
}