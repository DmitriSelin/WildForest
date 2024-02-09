using ErrorOr;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Authentication.Commands.Registration;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Domain.Languages.ValueObjects;
using WildForest.Domain.Tokens.Entities;

namespace WildForest.Application.Authentication.Commands.RegisterUser;

public sealed class RegistrationService : IRegistrationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RegistrationService(
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthenticationResult>> RegisterAsync(RegisterCommand command, bool isUserRole = true)
    {
        var email = Email.Create(command.Email);
        User? user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);

        if (user is not null)
            return Errors.User.DuplicateEmail;

        var cityId = CityId.Create(command.CityId);
        var city = await _unitOfWork.CityRepository.GetCityByIdAsync(cityId);

        if (city is null)
            return Errors.City.NotFoundById;

        var languageId = LanguageId.Create(command.LanguageId);
        var language = await _unitOfWork.LanguageRepository.GetLanguageByIdAsync(languageId);

        if (language is null)
            return Errors.Language.NotFound;

        user = CreateUserByRole(command, email, isUserRole);

        await _unitOfWork.UserRepository.AddUserAsync(user);
        RefreshToken refreshToken = await GetRefreshTokenAsync(user, command.IpAddress);
        await _unitOfWork.SaveChangesAsync();

        var accessToken = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, accessToken, refreshToken.Token);
    }

    private static User CreateUserByRole(RegisterCommand command, Email email, bool isUserRole)
    {
        if (isUserRole)
            return CreateUser(command, email);

        return CreateAdmin(command, email);
    }

    private async Task<RefreshToken> GetRefreshTokenAsync(User user, string ipAddress)
    {
        RefreshToken token = await _refreshTokenGenerator.GenerateTokenAsync(user.Id, ipAddress);
        await _unitOfWork.RefreshTokenRepository.AddTokenAsync(token);
        return token;
    }

    private static User CreateUser(RegisterCommand command, Email email)
    {
        var credentials = CreateCredentials(command);

        return User.CreateUser(
            firstName: credentials.Item1,
            lastName: credentials.Item2,
            email: email,
            password: credentials.Item3,
            cityId: credentials.Item4,
            languageId: credentials.Item5,
            image: credentials.Item6);
    }

    private static User CreateAdmin(RegisterCommand command, Email email)
    {
        var credentials = CreateCredentials(command);

        return User.CreateAdmin(
            firstName: credentials.Item1,
            lastName: credentials.Item2,
            email: email,
            password: credentials.Item3,
            cityId: credentials.Item4,
            languageId: credentials.Item5,
            image: credentials.Item6);
    }

    private static Tuple<FirstName, LastName, Password, CityId, LanguageId, byte[]?> CreateCredentials(RegisterCommand command)
    {
        var firstName = FirstName.Create(command.FirstName);
        var lastName = LastName.Create(command.LastName);
        var password = Password.Create(command.Password);
        var cityId = CityId.Create(command.CityId);
        var languageId = LanguageId.Create(command.LanguageId);

        return new(firstName, lastName, password, cityId, languageId, command.Image);
    }
}