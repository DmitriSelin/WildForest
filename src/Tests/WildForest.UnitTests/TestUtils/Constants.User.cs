using WildForest.Domain.Users.ValueObjects;

namespace WildForest.UnitTests.TestUtils;

public static partial class Constants
{
    public static class User
    {
        public static readonly FirstName FirstName = FirstName.Create("FirstName");

        public static readonly LastName LastName = LastName.Create("LastName");

        public static readonly string Password = "Password";

        public static readonly Email FirstEmail = Email.Create("first@gmail.com");

        public static readonly Email SecondEmail = Email.Create("second@gmail.com");

        public static readonly Email ThirdEmail = Email.Create("third@gmail.com");

        public static readonly Email UserDuplicateEmail = Email.Create("user@gmail.com");

        public static readonly Email AdminDuplicateEmail = Email.Create("admin@gmail.com");

        public const string IP = "1.0.0.0";
    }
}