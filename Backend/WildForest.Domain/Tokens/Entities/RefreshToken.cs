using WildForest.Domain.Common.Models;
using WildForest.Domain.Tokens.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Domain.Tokens.Entities;

public sealed class RefreshToken : Entity<RefreshTokenId>
{
    public string Token { get; } = null!;

    public DateTime Expiration { get; }

    public DateTime CreationDate { get; }

    public string CreatedByIp { get; } = null!;

    public UserId UserId { get; } = null!;

    public User User { get; } = null!;

    public DateTime? RevokedDate { get; private set; }

    public string? RevokedByIp { get; private set; }

    public string? ReplacedByToken { get; private set; }

    public string? ReasonRevoked { get; private set; }

    public bool IsExpired => DateTime.UtcNow >= Expiration;

    public bool IsRevoked => RevokedDate is not null;

    public bool IsActive => !IsRevoked && !IsExpired;

    private RefreshToken(
        RefreshTokenId id,
        string token,
        DateTime expiration,
        DateTime creationDate,
        string createdByIp,
        UserId userId,
        DateTime? revokedDate,
        string? revokedByIp,
        string? replacedByToken,
        string? reasonRevoked) : base(id)
    {
        Token = token;
        Expiration = expiration;
        CreationDate = creationDate;
        CreatedByIp = createdByIp;
        UserId = userId;
        RevokedDate = revokedDate;
        RevokedByIp = revokedByIp;
        ReplacedByToken = replacedByToken;
        ReasonRevoked = reasonRevoked;
    }

    public static RefreshToken Create(
        string token,
        string createdByIp,
        UserId userId)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentNullException(nameof(token));

        var utcNow = DateTime.UtcNow;

        return new(
            RefreshTokenId.Create(), token, utcNow.AddDays(7),
            utcNow, createdByIp, userId,
            null, null, null, null);
    }

    public void Revoke(string revokedByIp, string reasonRevoked, string? replacedByToken = null)
    {
        if (replacedByToken == null)
        {
            Update(revokedByIp, reasonRevoked);
        }
        else
        {
            Update(revokedByIp, reasonRevoked, replacedByToken);
        }
    }

    public void Update(string revokedByIp, string reasonRevoked, string replacedByToken)
    {
        Update(revokedByIp, reasonRevoked);
        ReplacedByToken = replacedByToken;
    }


    private void Update(string revokedByIp, string reasonRevoked)
    {
        RevokedDate = DateTime.UtcNow;
        RevokedByIp = revokedByIp;
        ReasonRevoked = reasonRevoked;
    }

#pragma warning disable IDE0051 // Remove unused private members
    private RefreshToken(RefreshTokenId id) : base(id) { }
#pragma warning restore IDE0051 // Remove unused private members
}