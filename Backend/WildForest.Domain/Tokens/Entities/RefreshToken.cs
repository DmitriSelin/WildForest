using WildForest.Domain.Common.Models;
using WildForest.Domain.Tokens.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Domain.Tokens.Entities;

public sealed class RefreshToken : Entity<RefreshTokenId>
{
    public Token Token { get; }
    
    public Expiration Expiration { get; }
    
    public CreationDate CreationDate { get; }
    
    public CreatedByIp CreatedByIp { get; }
    
    public UserId UserId { get; }

    public User User { get; } = null!;
    
    public Revoked Revoked { get; }
    
    public RevokedByIp RevokedByIp { get; }
    
    public ReplacedByToken ReplacedByToken { get; }
    
    public ReasonRevoked ReasonRevoked { get; }

    public bool IsExpired => DateTime.UtcNow >= Expiration.Value;

    public bool IsRevoked => Revoked.Value is not null;

    public bool IsActive => !IsRevoked && !IsExpired;
    
    private RefreshToken(
        RefreshTokenId id,
        Token token,
        Expiration expiration,
        CreationDate creationDate,
        CreatedByIp createdByIp,
        UserId userId,
        Revoked revoked,
        RevokedByIp revokedByIp,
        ReplacedByToken replacedByToken,
        ReasonRevoked reasonRevoked) : base(id)
    {
        Token = token;
        Expiration = expiration;
        CreationDate = creationDate;
        CreatedByIp = createdByIp;
        UserId = userId;
        Revoked = revoked;
        RevokedByIp = revokedByIp;
        ReplacedByToken = replacedByToken;
        ReasonRevoked = reasonRevoked;
    }

    public static RefreshToken Create(
        Token token,
        CreatedByIp createdByIp,
        UserId userId)
    {
        return new(
            RefreshTokenId.Create(),
            token,
            Expiration.Create(DateTime.UtcNow.AddDays(7)),
            CreationDate.Create(DateTime.UtcNow),
            createdByIp,
            userId,
            Revoked.Create(null),
            RevokedByIp.Create(null),
            ReplacedByToken.Create(null),
            ReasonRevoked.Create(null));
    }
    
    private RefreshToken(RefreshTokenId id) : base(id) { }
}