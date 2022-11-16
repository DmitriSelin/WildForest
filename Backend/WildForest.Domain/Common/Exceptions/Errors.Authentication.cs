using ErrorOr;

namespace WildForest.Domain.Common.Exceptions
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidEmail => Error.Conflict(
                code: "Authentication.InvalidEmail",
                description: "User with this email does not exists");

            public static Error InvalidPassword => Error.Conflict(
                code: "Authentication.InvalidPassword",
                description: "Invalid Password");
        }
    }
}