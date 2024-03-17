using ErrorOr;

namespace WildForest.Domain.Common.Errors;

public static partial class Errors
{
    public static class Comment
    {
        public static Error NotFoundByWeatherForecastId => Error.NotFound(
            code: "Comment.NotFoundByWeatherForecastId",
            description: "Comments with this weatherForecastId do not exist");

        public static Error NotFound => Error.NotFound(
            code: "Comment.NotFound",
            description: "Comment not found");
    }
}