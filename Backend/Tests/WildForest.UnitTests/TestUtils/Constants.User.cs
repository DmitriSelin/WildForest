using WildForest.Domain.Users.ValueObjects;

namespace WildForest.UnitTests.TestUtils;

public static partial class Constants
{
    public static class User
    {
        public static readonly FirstName FirstName = FirstName.Create("FirstName");

        public static readonly LastName LastName = LastName.Create("LastName");

        public static readonly Password Password = Password.Create("Password");

        public static readonly Email FirstEmail = Email.Create("test1@gmail.com");

        public static readonly Email SecondEmail = Email.Create("test2@gmail.com");

        public static readonly Email ThirdEmail = Email.Create("test3@gmail.com");
    }
}