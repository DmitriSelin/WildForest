﻿using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Authentication.Commands.RegisterUser
{
    public sealed class UserRegistrator : IUserRegistrator
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;

        public UserRegistrator(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository, 
            ICityRepository cityRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _cityRepository = cityRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> RegisterAsync(RegisterUserCommand command)
        {
            var email = Email.Create(command.Email);

            User? user = await _userRepository.GetUserByEmailAsync(email);

            if (user is not null)
            {
                return Errors.User.DupplicateEmail;
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
                CityId.Create(command.CityId));

            await _userRepository.AddUserAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}