using WildForest.Application.Authentication.Common;
using ErrorOr;

namespace WildForest.Application.Authentication.Commands.RegisterUser
{
    public interface IUserRegistrator
    {
        public ErrorOr<Task<AuthenticationResult>> RegisterAsync(RegisterUserCommand command);
    }
}