using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Tokens.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Authentication.Queries.LoginUser
{
    public sealed class UserLogger : IUserLogger
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public UserLogger(
            IJwtTokenGenerator jwtTokenGenerator, 
            IRefreshTokenGenerator refreshTokenGenerator,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository) 
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
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

            var createdByIp = CreatedByIp.Create(query.IpAddress);
            var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(user.Id, createdByIp);
            await _refreshTokenRepository.AddTokenAsync(refreshToken, false);
            
            await _refreshTokenRepository.RemoveOldRefreshTokensByUserIdAsync(user.Id);
            
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token, refreshToken.Token.Value);
        }
    }
}