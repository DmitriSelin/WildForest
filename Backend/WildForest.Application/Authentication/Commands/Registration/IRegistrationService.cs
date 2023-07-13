using ErrorOr;
using WildForest.Application.Authentication.Commands.Registration;
using WildForest.Application.Authentication.Common;

namespace WildForest.Application.Authentication.Commands.RegisterUser;

public interface IRegistrationService
{
    public Task<ErrorOr<AuthenticationResult>> RegisterAsync(RegisterCommand command, bool isUserRole = true);
}