using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Entities;

namespace WildForest.Application.Authentication.Commands.RegisterUser
{
    public class UserRegistrator : IUserRegistrator
    {
        private readonly IUserRepository _userRepository;

        public UserRegistrator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AuthenticationResult Register(RegisterUserCommand command)
        {
            if (_userRepository.GetUserByEmailAsync(command.Email) != null)
            {
                throw new Exception();
            }

            var user = new User(Guid.NewGuid(), command.FirstName, command.LastName,
                Role.User, command.Email, command.Password);

            var authenticationResult = new AuthenticationResult(user, "token");

            return authenticationResult;
        }
    }
}