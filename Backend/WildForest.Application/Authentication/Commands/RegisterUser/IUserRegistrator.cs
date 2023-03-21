using WildForest.Application.Authentication.Common;
using ErrorOr;

namespace WildForest.Application.Authentication.Commands.RegisterUser
{
    public interface IUserRegistrator
    {
        public Task<ErrorOr<AuthenticationResult>> RegisterAsync(RegisterUserCommand command);

        public Task<ErrorOr<AuthenticationResult>> RegisterAdminAsync(RegisterUserCommand command);
    }
}