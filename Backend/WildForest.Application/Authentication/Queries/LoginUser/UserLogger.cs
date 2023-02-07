using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Authentication.Queries.LoginUser
{
    public sealed class UserLogger : IUserLogger
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public UserLogger(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> LoginAsync(LoginUserQuery query)
        {
            var email = Email.Create(query.Email);
            var password = Password.Create(query.Password);

            User? user = await _userRepository.GetUserByEmailAsync(email);

            if (user is null || password != user.Password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}