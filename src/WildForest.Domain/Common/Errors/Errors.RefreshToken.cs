using ErrorOr;

namespace WildForest.Domain.Common.Errors;

public static partial class Errors
{
    public static class RefreshToken
    {
        public static Error NotFound => Error.NotFound(
            code: "RefreshToken.NotFound",
            description: "The same refresh token can not be found");

        public static Error InvalidToken => Error.Validation(
            code: "RefreshToken.InvalidToken",
            description: "Invalid refresh token");
    }
}