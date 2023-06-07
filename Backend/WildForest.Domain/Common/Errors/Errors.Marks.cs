using ErrorOr;

namespace WildForest.Domain.Common.Errors;

public static partial class Errors
{
    public static class Mark
    {
        public static Error NotFound => Error.NotFound(
            code: "Mark.NotFound",
            description: "There are no comments by this forecast");

        public static Error DuplicateComment => Error.Conflict(
            code: "Mark.DuplicateComment",
            description: "User can send only one comment at day");
    }
}