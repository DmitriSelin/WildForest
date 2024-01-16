using ErrorOr;

namespace WildForest.Domain.Common.Errors;

public static partial class Errors
{
    public static class Language
    {
        public static Error NotFound => Error.NotFound(
            code: "Language.NotFound",
            description: "Language was not found");
    }
}