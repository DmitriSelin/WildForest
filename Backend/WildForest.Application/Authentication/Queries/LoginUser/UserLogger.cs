﻿using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Entities;

namespace WildForest.Application.Authentication.Queries.LoginUser
{
    public class UserLogger : IUserLogger
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public UserLogger(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResult> LoginAsync(LoginUserQuery query)
        {
            User? user = await _userRepository.GetUserByEmailAsync(query.Email);

            if (user == null)
            {
                throw new Exception();
            }

            if (query.Password != user.Password)
            {
                throw new Exception();
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}