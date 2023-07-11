using ErrorOr;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Common.Interfaces.Weather;
using WildForest.Application.Authentication.Commands.Registration.Commands;
using WildForest.Domain.Clients.Users.Entities;
using WildForest.Domain.Clients.Admins.Entites;
using WildForest.Domain.Clients.ValueObjects;
using WildForest.Application.Authentication.Common;

namespace WildForest.Application.Authentication.Commands.RegisterUser
{
    public sealed class RegistrationService : IRegistrationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RegistrationService(
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator,
            IUserRepository userRepository,
            IAdminRepository adminRepository,
            ICityRepository cityRepository,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _cityRepository = cityRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<ErrorOr<AuthenticationResult<User>>> RegisterUserAsync(RegisterUserCommand command)
        {
            var email = Email.Create(command.Email);

            User? user = await _userRepository.GetUserByEmailAsync(email);

            if (user is not null)
                return Errors.Person.DuplicateEmail;

            var cityId = CityId.Create(command.CityId);

            var city = await _cityRepository.GetCityByIdAsync(cityId);

            if (city is null)
                return Errors.City.NotFoundById;

            user = CreateUser(command, email, city.Id);

            await _userRepository.AddUserAsync(user);

            var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(user.Id, command.IpAddress);

            await _refreshTokenRepository.AddTokenAsync(refreshToken);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult<User>(user, token, refreshToken.Token);
        }

        public async Task<ErrorOr<AuthenticationResult<Admin>>> RegisterAdminAsync(RegisterCommand command)
        {
            var email = Email.Create(command.Email);

            Admin? admin = await _adminRepository.GetAdminByEmailAsync(email);

            if (admin is null)
                return Errors.Person.DuplicateEmail;

            admin = CreateAdmin(command, email);

            await _adminRepository.AddAdminAsync(admin);

            var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(admin.Id, command.IpAddress);

            await _refreshTokenRepository.AddTokenAsync(refreshToken);

            var token = _jwtTokenGenerator.GenerateToken(admin);

            return new AuthenticationResult<Admin>(admin, token, refreshToken.Token);
        }

        private static User CreateUser(RegisterUserCommand command, Email email, CityId cityId)
        {
            var credentials = CreateCredentials(command.FirstName, command.LastName, command.Password);

            return User.Create(
                firstName: credentials.Item1,
                lastName: credentials.Item2,
                email: email,
                password: credentials.Item3,
                cityId: cityId);
        }

        private static Admin CreateAdmin(RegisterCommand command, Email email)
        {
            var credentials = CreateCredentials(command.FirstName, command.LastName, command.Password);

            return Admin.Create(
                firstName: credentials.Item1,
                lastName: credentials.Item2,
                email: email,
                password: credentials.Item3);
        }

        private static Tuple<FirstName, LastName, Password> CreateCredentials(string fName, string lName, string personPassword)
        {
            var firstName = FirstName.Create(fName);
            var lastName = LastName.Create(lName);
            var password = Password.Create(personPassword);

            return new(firstName, lastName, password);
        }
    }
}