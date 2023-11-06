using ErrorOr;

namespace WildForest.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation(
                code: "Authentication.InvalidCredentials",
                description: "User with such an email or password does not exist");

            public static Error InvalidAuthorizationHeader => Error.Validation(
                code: "Authentication.InvalidAuthorizationHeader",
                description: "Not correct Authorization Header");
        }
    }
}