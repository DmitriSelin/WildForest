using ErrorOr;

namespace WildForest.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Weather 
        {
            public static Error NotFound => Error.NotFound(
                code: "WeatherForecust.NotFound",
                description: "Not found weather forecust for these city and date");
        }
    }
}