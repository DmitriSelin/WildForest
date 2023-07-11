using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Clients.Models;
using WildForest.Domain.Clients.ValueObjects;
using WildForest.Domain.Common.Enums;

namespace WildForest.Domain.Clients.Users.Entities;

public class User : Person
{
    public CityId CityId { get; } = null!;

    public City City { get; } = null!;

    private User(
        PersonId id, FirstName firstName,
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
        return new(PersonId.Create(), firstName,
            lastName, Role.User,
            email, password, cityId);
    }
}