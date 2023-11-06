using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Languages.Entities;
using WildForest.Domain.Languages.ValueObjects;
using WildForest.Domain.Ratings.Entities;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Domain.Users.Entities;

public class User : Entity<UserId>
{
    public FirstName FirstName { get; } = null!;

    public LastName LastName { get; } = null!;

    public Role Role { get; }

    public Email Email { get; } = null!;

    public Password Password { get; } = null!;

    public CityId CityId { get; } = null!;

    public City City { get; } = null!;

    public LanguageId LanguageId { get; } = null!;

    public Language Language { get; } = null!;

    private readonly List<RefreshToken> _refreshTokens = new();

    public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    private readonly List<Vote> _votes = new();

    public IReadOnlyList<Vote> Votes => _votes.AsReadOnly();

    private User(
        UserId id, FirstName firstName, LastName lastName,
        Role role, Email email, Password password,
        CityId cityId, LanguageId languageId) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        Email = email;
        Password = password;
        CityId = cityId;
        LanguageId = languageId;
    }

    public static User CreateUser(
        FirstName firstName,
        LastName lastName,
        Email email,
        Password password,
        CityId cityId,
        LanguageId languageId)
    {
        return new(UserId.Create(), firstName, lastName,
                    Role.User, email, password, cityId,
                    languageId);
    }

    public static User CreateAdmin(
        FirstName firstName,
        LastName lastName,
        Email email,
        Password password,
        CityId cityId,
        LanguageId languageId)
    {
        return new(UserId.Create(), firstName, lastName,
                    Role.Admin, email, password,
                    cityId, languageId);
    }

#pragma warning disable IDE0051 // Remove unused private members
    private User(UserId id) : base(id) { }
#pragma warning restore IDE0051 // Remove unused private members
}