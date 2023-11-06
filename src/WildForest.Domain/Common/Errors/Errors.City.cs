using ErrorOr;

namespace WildForest.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class City
        {
            public static Error NotFoundById => Error.NotFound(
                code: "City.NotFoundById",
                description: "City with this id does not exist");

            public static Error NotFoundCitiesByCountry => Error.NotFound(
                code: "City.NotFoundCitiesByCountry",
                description: "Could not find a single city in this country");
        }
    }
}