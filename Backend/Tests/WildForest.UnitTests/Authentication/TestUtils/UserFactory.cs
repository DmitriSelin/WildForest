using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Languages.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.UnitTests.TestUtils;

namespace WildForest.UnitTests.Authentication.TestUtils;

public sealed class UserFactory
{
    public static IEnumerable<User> Create(CityId cityId, LanguageId languageId)
    {
        var users = new List<User>();

        var user = User.CreateUser(Constants.User.FirstName, Constants.User.LastName,
            Constants.User.UserDuplicateEmail, Constants.User.Password, cityId, languageId);

        users.Add(user);

        var admin = User.CreateAdmin(Constants.User.FirstName, Constants.User.LastName,
            Constants.User.AdminDuplicateEmail, Constants.User.Password, cityId, languageId);

        users.Add(admin);
        return users;
    }
}