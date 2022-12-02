using ErrorOr;

namespace WildForest.Domain.Common.Exceptions
{
    public static partial class Errors
    {
        public static class City
        {
            public static Error NotFoundById => Error.NotFound(
                code: "City.NotFoundById",
                description: "City with this id does not exist");
        }
    }
}