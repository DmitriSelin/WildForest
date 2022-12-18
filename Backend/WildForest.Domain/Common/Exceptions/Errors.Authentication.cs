using ErrorOr;

namespace WildForest.Domain.Common.Exceptions
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation(
                code: "Authentication.InvalidCredentials",
                description: "User with such an email or password does not exist");
        }
    }
}