using ErrorOr;

namespace WildForest.Domain.Common.Errors;

public static partial class Errors
{
    public static class Comment
    {
        public static Error NotFoundByWeatherForecastId => Error.NotFound(
            code: "Comment.NotFoundByWeatherForecastId",
            description: "Country with this name is already exists");
    }
}