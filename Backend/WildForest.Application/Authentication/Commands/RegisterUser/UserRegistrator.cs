using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Tokens.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Application.Common.Interfaces.Weather;

namespace WildForest.Application.Authentication.Commands.RegisterUser
{
    public sealed class UserRegistrator : IUserRegistrator
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IExistingWeatherDataService _existingWeatherDataService;

        public UserRegistrator(
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator,
            IUserRepository userRepository,
            ICityRepository cityRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IExistingWeatherDataService existingWeatherDataService)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _userRepository = userRepository;
            _cityRepository = cityRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _existingWeatherDataService = existingWeatherDataService;
        }

        public async Task<ErrorOr<AuthenticationResult>> RegisterAsync(RegisterUserCommand command, bool isUserRole = true)
        {
            var email = Email.Create(command.Email);

            User? user = await _userRepository.GetUserByEmailAsync(email);

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

            if (isUserRole)
            {
                user = CreateUser(command, email, city.Id);
            }
            else
            {
                user = CreateAdmin(command, email, city.Id);
            }

            await _userRepository.AddUserAsync(user);

            await _existingWeatherDataService.CheckWeatherDataExisting(city.Id);

            var createdByIp = CreatedByIp.Create(command.IpAddress);
            var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(user.Id, createdByIp);
            await _refreshTokenRepository.AddTokenAsync(refreshToken);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token, refreshToken.Token.Value);
        }

        private User CreateUser(RegisterUserCommand command, Email email, CityId cityId)
        {
            var firstName = FirstName.Create(command.FirstName);
            var lastName = LastName.Create(command.LastName);
            var password = Password.Create(command.Password);

            return User.Create(
                firstName,
                lastName,
                email,
                password,
                cityId);
        }

        private User CreateAdmin(RegisterUserCommand command, Email email, CityId cityId)
        {
            var firstName = FirstName.Create(command.FirstName);
            var lastName = LastName.Create(command.LastName);
            var password = Password.Create(command.Password);

            return User.CreateAdmin(
                firstName,
                lastName,
                email,
                password,
                cityId);
        }
    }
}