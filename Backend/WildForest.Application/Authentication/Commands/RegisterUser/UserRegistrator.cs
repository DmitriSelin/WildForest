using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Exceptions;
using WildForest.Domain.User.Entities;
using WildForest.Domain.User.ValueObjects;

namespace WildForest.Application.Authentication.Commands.RegisterUser
{
    public class UserRegistrator : IUserRegistrator
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public UserRegistrator(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async ErrorOr<Task<AuthenticationResult>> RegisterAsync(RegisterUserCommand command)
        {
            User? user = await _userRepository.GetUserByEmailAsync(command.Email);

            if (user is not null)
            {
                return Errors.User.DupplicateEmail;
            }

            user = new User(UserId.CreateUserId(), command.FirstName, command.LastName,
                Role.User, command.Email, command.Password);

            await _userRepository.AddUserAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}