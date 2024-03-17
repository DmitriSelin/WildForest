using ErrorOr;

namespace WildForest.Domain.Common.Errors;

public static partial class Errors
{
    public static class Country
    {
        public static Error Duplicate => Error.Conflict(
            code: "Country.Duplicate",
            description: "Country with this name is already exists");

        public static Error NotFound => Error.NotFound(
            code: "Country.NotFound",
            description: "There are no countries with this id");
    }
}