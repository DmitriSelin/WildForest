using ErrorOr;

namespace WildForest.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DupplicateEmail => Error.Conflict(
                code: "User.DuplicateEmail",
                description: "User with this email is already exists");

            public static Error NotFoundById => Error.NotFound(
                code: "User.NotFoundById",
                description: "User with this id does not exist");
        }
    }
}