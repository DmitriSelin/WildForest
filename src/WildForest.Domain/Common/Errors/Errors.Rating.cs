using ErrorOr;

namespace WildForest.Domain.Common.Errors;

public static partial class Errors
{
    public static class Rating
    {
        public static Error DuplicateVote => Error.Conflict(
            code: "Rating.DuplicateVote",
            description: "The vote of this rating by this user already exists");

        public static Error VoteNotFound => Error.NotFound(
            code: "Rating.VoteNotFound",
            description: "The vote is not found by this id, userId");

        public static Error NotFoundById => Error.NotFound(
            code: "Rating.NotFoundById",
            description: "The rating is not found by this id");
    }
}