using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.UnitTests.TestUtils;

namespace WildForest.UnitTests.Authentication.TestUtils;

public sealed class UserFactory
{
    public static IEnumerable<User> Create(CityId cityId)
    {
        var users = new List<User>();

        var user = User.CreateUser(Constants.User.FirstName, Constants.User.LastName,
            Constants.User.UserDuplicateEmail, Constants.User.Password, cityId);

        users.Add(user);

        var admin = User.CreateAdmin(Constants.User.FirstName, Constants.User.LastName,
            Constants.User.AdminDuplicateEmail, Constants.User.Password, cityId);

        users.Add(admin);
        return users;
    }
}