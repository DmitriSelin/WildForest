using WildForest.Domain.Clients.ValueObjects;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Tokens.Entities;

namespace WildForest.Domain.Clients.Models;

public abstract class Person : Entity<PersonId>
{
    public FirstName FirstName { get; } = null!;

    public LastName LastName { get; } = null!;

    public Role Role { get; }

    public Email Email { get; } = null!;

    public Password Password { get; } = null!;

    private readonly List<RefreshToken> _refreshTokens = new();

    public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    protected Person(
        PersonId id,
        FirstName firstName,
        LastName lastName,
        Role role,
        Email email,
        Password password) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        Email = email;
        Password = password;
    }

    private Person(PersonId id) : base(id) { }
}