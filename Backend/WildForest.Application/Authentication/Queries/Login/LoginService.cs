using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Clients.Admins.Entites;
using WildForest.Domain.Clients.Users.Entities;
using WildForest.Domain.Clients.ValueObjects;
using WildForest.Domain.Common.Errors;

namespace WildForest.Application.Authentication.Queries.LoginUser;

public sealed class LoginService : ILoginService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LoginService(
        IJwtTokenGenerator jwtTokenGenerator, 
        IRefreshTokenGenerator refreshTokenGenerator,
        IUserRepository userRepository,
        IAdminRepository adminRepository,
        IRefreshTokenRepository refreshTokenRepository) 
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _userRepository = userRepository;
        _adminRepository = adminRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<ErrorOr<AuthenticationResult<User>>> LoginUserAsync(LoginQuery query)
    {
        var email = Email.Create(query.Email);
        var password = Password.Create(query.Password);

        User? user = await _userRepository.GetUserByEmailWithCityAsync(email);

        if (user is null || password != user.Password)
            return Errors.Authentication.InvalidCredentials;

        var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(user.Id, query.IpAddress);
        await _refreshTokenRepository.AddTokenAsync(refreshToken, false);

        await _refreshTokenRepository.RemoveOldRefreshTokensByUserIdAsync(user.Id);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult<User>(user, token, refreshToken.Token);
    }

    public async Task<ErrorOr<AuthenticationResult<Admin>>> LoginAdminAsync(LoginQuery query)
    {
        var email = Email.Create(query.Email);
        var password = Password.Create(query.Password);

        Admin? admin = await _adminRepository.GetAdminByEmailAsync(email);

        if (admin is null || password != admin.Password)
            return Errors.Authentication.InvalidCredentials;

        var refreshToken = await _refreshTokenGenerator.GenerateTokenAsync(admin.Id, query.IpAddress);
        await _refreshTokenRepository.AddTokenAsync(refreshToken, false);

        await _refreshTokenRepository.RemoveOldRefreshTokensByUserIdAsync(admin.Id);

        var token = _jwtTokenGenerator.GenerateToken(admin);

        return new AuthenticationResult<Admin>(admin, token, refreshToken.Token);
    }
}