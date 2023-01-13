using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Exceptions;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Cities.ValueObjects;

namespace WildForest.Application.Authentication.Commands.RegisterUser
{
    public sealed class UserRegistrator : IUserRegistrator
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public UserRegistrator(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> RegisterAsync(RegisterUserCommand command)
        {
            User? user = await _userRepository.GetUserByEmailAsync(command.Email);

            if (user is not null)
            {
                return Errors.User.DupplicateEmail;
            }

            user = User.CreateUser(command.FirstName, command.LastName,
                Role.User, command.Email, command.Password, CityId.CreateCityId(command.CityId));

            await _userRepository.AddUserAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}