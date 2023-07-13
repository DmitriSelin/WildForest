﻿using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Models;
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

    private readonly List<RefreshToken> _refreshTokens = new();

    public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    private User(
        UserId userId, FirstName firstName, LastName lastName,
        Role role, Email email, Password password, CityId cityId) : base(userId)
    {
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        Email = email;
        Password = password;
        CityId = cityId;
    }

    //private User(UserId userId) : base(userId) { }

    public static User CreateUser(
        FirstName firstName,
        LastName lastName,
        Email email,
        Password password,
        CityId cityId)
    {
        return new(UserId.Create(), firstName, lastName,
                    Role.User, email, password, cityId);
    }

    public static User CreateAdmin(
        FirstName firstName,
        LastName lastName,
        Email email,
        Password password,
        CityId cityId)
    {
        return new(UserId.Create(), firstName, lastName,
                    Role.Admin, email, password, cityId);
    }
}