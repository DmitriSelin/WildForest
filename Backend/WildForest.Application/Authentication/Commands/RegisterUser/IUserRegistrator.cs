using WildForest.Application.Authentication.Common;

namespace WildForest.Application.Authentication.Commands.RegisterUser
{
    public interface IUserRegistrator
    {
        public Task<AuthenticationResult> RegisterAsync(RegisterUserCommand command);
    }
}