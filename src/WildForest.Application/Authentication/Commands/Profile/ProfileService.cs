using ErrorOr;
using WildForest.Domain.Common.Errors;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Languages.ValueObjects;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Languages.Entities;

namespace WildForest.Application.Authentication.Commands.Profile;

public sealed class ProfileService : IProfileService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public ProfileService(
        IUnitOfWork unitOfWork,
        IRefreshTokenGenerator refreshTokenGenerator,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _refreshTokenGenerator = refreshTokenGenerator;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> UpdateAsync(UpdateProfileCommand command)
    {
        var userId = UserId.Create(command.Id);
        var email = Email.Create(command.Email);
        var cityId = CityId.Create(command.CityId);
        var languageId = LanguageId.Create(command.LanguageId);
        City? city = null;
        Language? language = null;

        User? user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);

        if (user is null || user.Id != userId || !user.Password.IsEqual(command.Password))
            return Errors.User.NotFound;

        if (user.CityId != cityId)
            city = await _unitOfWork.CityRepository.GetCityByIdAsync(cityId);

        if (user.LanguageId != languageId)
            language = await _unitOfWork.LanguageRepository.GetLanguageByIdAsync(languageId);

        var newUserCredentials = CreateUser(command);
        user.Update(newUserCredentials, city, language);

        var refreshToken = await GetRefreshTokenAsync(user.Id, command.IpAddress);
        await _unitOfWork.SaveChangesAsync();

        string accessToken = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, accessToken, refreshToken.Token);
    }

    private static User CreateUser(UpdateProfileCommand command)
    {
        var firstName = FirstName.Create(command.FirstName);
        var lastName = LastName.Create(command.LastName);
        var email = Email.Create(command.Email);
        Password password;

        if (string.IsNullOrWhiteSpace(command.NewPassword))
            password = Password.Create(command.Password);
        else
            password = Password.Create(command.NewPassword);

        var cityId = CityId.Create(command.CityId);
        var languageId = LanguageId.Create(command.LanguageId);

        var user = User.CreateUser(firstName, lastName, email, password, cityId, languageId, command.Image);
        return user;
    }

    private async Task<RefreshToken> GetRefreshTokenAsync(UserId userId, string ipAddress)
    {
        RefreshToken refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(userId, ipAddress);
        await _unitOfWork.RefreshTokenRepository.AddTokenAsync(refreshToken);
        await _unitOfWork.RefreshTokenRepository.RemoveOldRefreshTokensByUserIdAsync(userId);
        return refreshToken;
    }
}