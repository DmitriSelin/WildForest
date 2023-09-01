using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.UnitTests.TestUtils;

namespace WildForest.UnitTests.Authentication.TestUtils;

public sealed class UserFactory
{
    public static IEnumerable<User> Create(CityId cityId)
    {
        var users = new List<User>();

        var firstUser = User.CreateUser(Constants.User.FirstName, Constants.User.LastName,
            Constants.User.FirstEmail, Constants.User.Password, cityId);

        users.Add(firstUser);

        var secondUser = User.CreateUser(Constants.User.FirstName, Constants.User.LastName,
            Constants.User.SecondEmail, Constants.User.Password, cityId);

        users.Add(secondUser);
        return users;
    }
}