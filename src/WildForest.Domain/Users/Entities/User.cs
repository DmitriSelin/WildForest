using System.Net.Mime;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Comments.Entities;
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
    public FirstName FirstName { get; private set; } = null!;

    public LastName LastName { get; private set; } = null!;

    public Role Role { get; private set; }

    public Email Email { get; } = null!;

    public Password Password { get; private set; } = null!;

    public byte[]? Image { get; private set; }

    public CityId CityId { get; private set; } = null!;

    public City City { get; private set; } = null!;

    public LanguageId LanguageId { get; private set; } = null!;

    public Language Language { get; private set; } = null!;

    private readonly List<RefreshToken> _refreshTokens = new();

    public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    private readonly List<Vote> _votes = new();

    public IReadOnlyList<Vote> Votes => _votes.AsReadOnly();

    private readonly List<Comment> _comments = new();

    public IReadOnlyList<Comment> Comments => _comments.AsReadOnly();

    private User(
        UserId id, FirstName firstName, LastName lastName,
        Role role, Email email, Password password, byte[]? image,
        CityId cityId, LanguageId languageId) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        Email = email;
        Password = password;
        Image = image;
        CityId = cityId;
        LanguageId = languageId;
    }

    public static User CreateUser(
        FirstName firstName,
        LastName lastName,
        Email email,
        Password password,
        CityId cityId,
        LanguageId languageId,
        byte[]? image)
    {
        return new(UserId.Create(), firstName, lastName,
                    Role.User, email, password, image,
                    cityId, languageId);
    }

    public static User CreateAdmin(
        FirstName firstName,
        LastName lastName,
        Email email,
        Password password,
        CityId cityId,
        LanguageId languageId,
        byte[]? image)
    {
        return new(UserId.Create(), firstName, lastName,
                    Role.Admin, email, password, image,
                    cityId, languageId);
    }

    public void Update(User newUserCredentials, City? city, Language? language)
    {
        FirstName = newUserCredentials.FirstName;
        LastName = newUserCredentials.LastName;
        Password = newUserCredentials.Password;

        if (city is not null)
            City = city;

        if (language is not null)
            Language = language;
    }

#pragma warning disable IDE0051 // Remove unused private members
    private User(UserId id) : base(id) { }
#pragma warning restore IDE0051 // Remove unused private members
}