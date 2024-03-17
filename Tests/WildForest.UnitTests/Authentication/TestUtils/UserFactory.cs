using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Languages.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;
using WildForest.UnitTests.TestUtils;

namespace WildForest.UnitTests.Authentication.TestUtils;

public sealed class UserFactory
{
    public static IEnumerable<User> Create(CityId cityId, LanguageId languageId)
    {
        var users = new List<User>();

        var user = User.CreateUser(Constants.User.FirstName, Constants.User.LastName,
            Constants.User.UserDuplicateEmail, Password.Create(Constants.User.Password), cityId, languageId, null);

        users.Add(user);

        var admin = User.CreateAdmin(Constants.User.FirstName, Constants.User.LastName,
            Constants.User.AdminDuplicateEmail, Password.Create(Constants.User.Password), cityId, languageId, null);

        users.Add(admin);
        return users;
    }
}