using WildForest.Application.Authentication.Common;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Entities;

namespace WildForest.Application.Authentication.Commands.RegisterUser
{
    public class UserRegistrator : IUserRegistrator
    {
        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            var user = new User(Guid.NewGuid(), firstName, lastName, Role.User, email, password);

            var authenticationResult = new AuthenticationResult(user, "token");

            return authenticationResult;
        }
    }
}