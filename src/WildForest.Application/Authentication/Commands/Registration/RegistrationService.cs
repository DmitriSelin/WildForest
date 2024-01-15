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

        if (isUserRole)
            user = CreateUser(command, email);
        else
            user = CreateAdmin(command, email);

        await _unitOfWork.UserRepository.AddUserAsync(user);

        if (user.Language is null)
        {
            var language = await _unitOfWork.LanguageRepository.GetLanguageByIdAsync(user.LanguageId);
            user.SetLanguage(language!);
        }

        var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(user.Id, command.IpAddress);

        await _unitOfWork.RefreshTokenRepository.AddTokenAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync();

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
            cityId: credentials.Item4,
            languageId: credentials.Item5);
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
            languageId: credentials.Item5);
    }

    private static Tuple<FirstName, LastName, Password, CityId, LanguageId> CreateCredentials(RegisterCommand command)
    {
        var firstName = FirstName.Create(command.FirstName);
        var lastName = LastName.Create(command.LastName);
        var password = Password.Create(command.Password);
        var cityId = CityId.Create(command.CityId);
        var languageId = LanguageId.Create(command.LanguageId);

        return new(firstName, lastName, password, cityId, languageId);
    }
}