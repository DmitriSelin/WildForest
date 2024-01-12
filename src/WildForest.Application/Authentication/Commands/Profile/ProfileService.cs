using ErrorOr;
using WildForest.Domain.Common.Errors;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Languages.ValueObjects;
using WildForest.Application.Common.Interfaces.Authentication;

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
        var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);

        if (user is null || user.Id != userId || !user.Password.IsEqual(command.Password))
            return Errors.User.NotFound;

        var newUserCredentials = CreateUser(command);
        user.Update(newUserCredentials);

        var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(user.Id, command.IpAddress);

        await _unitOfWork.RefreshTokenRepository.AddTokenAsync(refreshToken);
        await _unitOfWork.RefreshTokenRepository.RemoveOldRefreshTokensByUserIdAsync(user.Id);
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

        if (command.NewPassword is null)
            password = Password.Create(command.Password);
        else
            password = Password.Create(command.NewPassword);

        var cityId = CityId.Create(command.CityId);
        var languageId = LanguageId.Create(command.LanguageId);

        var user = User.CreateUser(firstName, lastName, email, password, cityId, languageId);
        return user;
    }
}