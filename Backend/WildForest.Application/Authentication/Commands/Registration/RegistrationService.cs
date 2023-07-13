using ErrorOr;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Authentication.Commands.Registration.Commands;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Application.Authentication.Common;

namespace WildForest.Application.Authentication.Commands.RegisterUser;

public sealed class RegistrationService : IRegistrationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RegistrationService(
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

    public async Task<ErrorOr<AuthenticationResult>> RegisterAsync(RegisterCommand command, bool isUserRole = true)
    {
        var email = Email.Create(command.Email);

        User? user = await _userRepository.GetUserByEmailAsync(email);

        if (user is not null)
            return Errors.User.DuplicateEmail;

        var cityId = CityId.Create(command.CityId);

        var city = await _cityRepository.GetCityByIdAsync(cityId);

        if (city is null)
            return Errors.City.NotFoundById;

        if (isUserRole)
            user = CreateUser(command, email);
        else
            user = CreateAdmin(command, email);

        await _userRepository.AddUserAsync(user);

        var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(user.Id, command.IpAddress);

        await _refreshTokenRepository.AddTokenAsync(refreshToken);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token, refreshToken.Token);
    }

    private static User CreateUser(RegisterCommand command, Email email)
    {
        var credentials = CreateCredentials(command);

        return User.CreateUser(
            firstName: credentials.Item1,
            lastName: credentials.Item2,
            email: email,
            password: credentials.Item3,
            cityId: credentials.Item4);
    }

    private static User CreateAdmin(RegisterCommand command, Email email)
    {
        var credentials = CreateCredentials(command);

        return User.CreateAdmin(
            firstName: credentials.Item1,
            lastName: credentials.Item2,
            email: email,
            password: credentials.Item3,
            cityId: credentials.Item4);
    }

    private static Tuple<FirstName, LastName, Password, CityId> CreateCredentials(RegisterCommand command)
    {
        var firstName = FirstName.Create(command.FirstName);
        var lastName = LastName.Create(command.LastName);
        var password = Password.Create(command.Password);
        var cityId = CityId.Create(command.CityId);

        return new(firstName, lastName, password, cityId);
    }
}