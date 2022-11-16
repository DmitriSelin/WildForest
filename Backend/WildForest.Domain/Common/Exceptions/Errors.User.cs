using ErrorOr;

namespace WildForest.Domain.Common.Exceptions
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DupplicateEmail => Error.Conflict(
                code: "User.DuplicateEmail",
                description: "User with this email is already exists");
        }
    }
}