using ErrorOr;

namespace WildForest.Domain.Common.Errors;

public static partial class Errors
{
    public static class Mark
    {
        public static Error DuplicateVote => Error.Conflict(
            code: "Mark.DuplicateVote",
            description: "The vote of this mark by this user already exists");

        public static Error NotFoundById => Error.NotFound(
            code: "Mark.NotFoundById",
            description: "The mark is not found by this id");
    }
}