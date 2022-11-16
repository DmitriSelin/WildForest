using ErrorOr;
using System.Net;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Common.Exceptions;
using WildForest.Domain.User.Entities;

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

        public async ErrorOr<Task<AuthenticationResult>> LoginAsync(LoginUserQuery query)
        {
            User? user = await _userRepository.GetUserByEmailAsync(query.Email);

            if (user is null)
            {
                return Errors.Authentication.InvalidEmail;
            }

            if (query.Password != user.Password)
            {
                return Errors.Authentication.InvalidPassword;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}