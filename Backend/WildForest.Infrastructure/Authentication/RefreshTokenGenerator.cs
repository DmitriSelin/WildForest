using System.Security.Cryptography;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Infrastructure.Authentication;

public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenGenerator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RefreshToken> GenerateTokenAsync(UserId personId, string createdByIp)
    {
        var token = await GenerateUniqueTokenAsync();
        
        var refreshToken = RefreshToken.Create(token, createdByIp, personId);
        return refreshToken;
    }

    private async Task<string> GenerateUniqueTokenAsync()
    {
        string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        bool isTokenUnique = await _unitOfWork.RefreshTokenRepository.IsTokenUnique(token);

        if (!isTokenUnique)
        {
            return await GenerateUniqueTokenAsync();
        }

        return token;
    }
}