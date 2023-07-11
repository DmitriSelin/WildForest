using WildForest.Application.Authentication.Common;
using ErrorOr;
using WildForest.Application.Authentication.Commands.Registration.Commands;
using WildForest.Domain.Clients.Users.Entities;
using WildForest.Domain.Clients.Admins.Entites;

namespace WildForest.Application.Authentication.Commands.RegisterUser;

public interface IRegistrationService
{
    public Task<ErrorOr<AuthenticationResult<User>>> RegisterUserAsync(RegisterUserCommand command);

    public Task<ErrorOr<AuthenticationResult<Admin>>> RegisterAdminAsync(RegisterCommand command);
}