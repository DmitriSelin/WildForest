using ErrorOr;

namespace WildForest.Domain.Common.Errors;

public static partial class Errors
{
    public static class Mark
    {
        public static Error NotFound => Error.NotFound(
            code: "Mark.NotFound",
            description: "There are no marks by this forecast");
    }
}