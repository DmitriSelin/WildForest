﻿using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Tokens.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Authentication.Commands.RegisterUser
{
    public sealed class UserRegistrator : IUserRegistrator
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public UserRegistrator(
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator,
            IUserRepository userRepository, 
            ICityRepository cityRepository,
            IRefreshTokenRepository refreshTokenRepository) 
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _userRepository = userRepository;
            _cityRepository = cityRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> RegisterAsync(RegisterUserCommand command)
        {
            var email = Email.Create(command.Email);

            User? user = await _userRepository.GetUserByEmailWithCityAsync(email);

            if (user is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            var cityId = CityId.Create(command.CityId);

            var city = await _cityRepository.GetCityByIdAsync(cityId);

            if (city is null)
            {
                return Errors.City.NotFoundById;
            }

            var firstName = FirstName.Create(command.FirstName);
            var lastName = LastName.Create(command.LastName);
            var password = Password.Create(command.Password);

            user = User.Create(
                firstName,
                lastName,
                email,
                password,
                city.Id);

            await _userRepository.AddUserAsync(user);

            var createdByIp = CreatedByIp.Create(command.IpAddress);
            var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(user.Id, createdByIp);
            await _refreshTokenRepository.AddTokenAsync(refreshToken);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token, refreshToken.Token.Value);
        }

        public async Task<ErrorOr<AuthenticationResult>> RegisterAdminAsync(RegisterUserCommand command)
        {
            var email = Email.Create(command.Email);

            User? user = await _userRepository.GetUserByEmailWithCityAsync(email);

            if (user is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            var cityId = CityId.Create(command.CityId);

            var city = await _cityRepository.GetCityByIdAsync(cityId);

            if (city is null)
            {
                return Errors.City.NotFoundById;
            }

            var firstName = FirstName.Create(command.FirstName);
            var lastName = LastName.Create(command.LastName);
            var password = Password.Create(command.Password);

            user = User.CreateAdmin(
                firstName,
                lastName,
                email,
                password,
                city.Id);

            await _userRepository.AddUserAsync(user);

            var createdByIp = CreatedByIp.Create(command.IpAddress);
            var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(user.Id, createdByIp);
            await _refreshTokenRepository.AddTokenAsync(refreshToken);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token, refreshToken.Token.Value);
        }
    }
}