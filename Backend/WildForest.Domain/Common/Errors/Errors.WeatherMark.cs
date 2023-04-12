using ErrorOr;

namespace WildForest.Domain.Common.Errors;

public static partial class Errors
{
    public static class WeatherMark
    {
        public static Error NotFound => Error.NotFound(
            code: "WeatherMark.NotFound",
            description: "There are no weather marks by weather id");
    }
}