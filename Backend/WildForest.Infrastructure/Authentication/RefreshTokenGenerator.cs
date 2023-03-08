using System.Security.Cryptography;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Tokens.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Infrastructure.Authentication;

public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RefreshTokenGenerator(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<RefreshToken> GenerateTokenAsync(UserId userId, CreatedByIp createdByIp)
    {
        var token = await GenerateUniqueTokenAsync();
        
        var refreshToken = RefreshToken.Create(token, createdByIp, userId);
        return refreshToken;
    }

    private async Task<Token> GenerateUniqueTokenAsync()
    {
        string randomSequence = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var token = Token.Create(randomSequence);

        bool isTokenUnique = await _refreshTokenRepository.IsTokenUnique(token);

        if (!isTokenUnique)
        {
            return await GenerateUniqueTokenAsync();
        }

        return token;
    }
}