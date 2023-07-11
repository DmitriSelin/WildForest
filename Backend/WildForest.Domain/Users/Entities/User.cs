using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Domain.Users.Entities;

public class User : Person<UserId>
{
    public CityId CityId { get; } = null!;

    public City City { get; } = null!;

    private User(
        UserId id, FirstName firstName,
        LastName lastName, Role role,
        Email email, Password password,
        CityId cityId) : base(id, firstName, lastName,
                                role, email, password)
    {
        CityId = cityId;
    }

    public static User Create(
        FirstName firstName,
        LastName lastName,
        Email email,
        Password password,
        CityId cityId)
    {
        return new(UserId.Create(), firstName,
            lastName, Role.User,
            email,password, cityId);
    }
}