using System.Text;
using WildForest.Domain.Users.ValueObjects;
namespace WildForest.UnitTests.Authentication.TestUtils;

public static class EmailGenerator
{
    private static Random _rnd = new();

    internal const int emailLength = 8;

    internal const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public static Email GenerateRandomEmail()
    {
        var builder = new StringBuilder();

        for (var i = 0; i < emailLength; i++)
        {
            var index = _rnd.Next(chars.Length);
            builder.Append(chars[index]);
        }

        var email = builder.ToString() + "@gmail.com";
        return Email.Create(email);
    }
}