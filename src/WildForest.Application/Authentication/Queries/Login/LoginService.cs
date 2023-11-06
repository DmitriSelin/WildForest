using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Authentication.Queries.LoginUser;

public sealed class LoginService : ILoginService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public LoginService(
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthenticationResult>> LoginAsync(LoginQuery query)
    {
        var email = Email.Create(query.Email);
        var password = Password.Create(query.Password);

        User? user = await _unitOfWork.UserRepository.GetUserByEmailWithCityAsync(email);

        if (user is null || !user.Password.IsEqual(password))
            return Errors.Authentication.InvalidCredentials;

        var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(user.Id, query.IpAddress);
        await _unitOfWork.RefreshTokenRepository.AddTokenAsync(refreshToken);

        await _unitOfWork.RefreshTokenRepository.RemoveOldRefreshTokensByUserIdAsync(user.Id);
        await _unitOfWork.SaveChangesAsync();

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token, refreshToken.Token);
    }
}