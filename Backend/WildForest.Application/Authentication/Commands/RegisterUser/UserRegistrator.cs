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
            User? user = _userRepository.GetUserByEmailAsync(command.Email).Result;

            if (user != null)
            {
                throw new Exception();
            }

            user = new User(Guid.NewGuid(), command.FirstName, command.LastName,
                Role.User, command.Email, command.Password);

            _userRepository.AddUserAsync(user);

            return new AuthenticationResult(user, "token");
        }
    }
}