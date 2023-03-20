using WildForest.Domain.Common.Models;
using WildForest.Domain.Tokens.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Domain.Tokens.Entities;

public sealed class RefreshToken : Entity<RefreshTokenId>
{
    public Token Token { get; } = null!;

    public Expiration Expiration { get; } = null!;

    public CreationDate CreationDate { get; } = null!;

    public CreatedByIp CreatedByIp { get; } = null!;

    public UserId UserId { get; } = null!;

    public User User { get; } = null!;

    public Revoked? Revoked { get; private set; }

    public RevokedByIp? RevokedByIp { get; private set; }
    
    public ReplacedByToken? ReplacedByToken { get; private set; }
    
    public ReasonRevoked? ReasonRevoked { get; private set; }

    public bool IsExpired => DateTime.UtcNow >= Expiration.Value;

    public bool IsRevoked => Revoked is not null;

    public bool IsActive => !IsRevoked && !IsExpired;
    
    private RefreshToken(
        RefreshTokenId id,
        Token token,
        Expiration expiration,
        CreationDate creationDate,
        CreatedByIp createdByIp,
        UserId userId,
        Revoked? revoked,
        RevokedByIp? revokedByIp,
        ReplacedByToken? replacedByToken,
        ReasonRevoked? reasonRevoked) : base(id)
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
            null,
            null,
            null,
            null);
    }

    public void Update(string revokedByIp, string reasonRevoked)
    {
        Revoked = Revoked.Create(DateTime.UtcNow);
        RevokedByIp = RevokedByIp.Create(revokedByIp);
        ReasonRevoked = ReasonRevoked.Create(reasonRevoked);
    }
    
    public void Update(string revokedByIp, string reasonRevoked, string replacedByToken)
    {
        Update(revokedByIp, reasonRevoked);
        ReplacedByToken = ReplacedByToken.Create(replacedByToken);
    }
    
    private RefreshToken(RefreshTokenId id) : base(id) { }
}