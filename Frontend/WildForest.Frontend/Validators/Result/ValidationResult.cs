namespace WildForest.Frontend.Validators.Result
{
    internal sealed record ValidationResult<T>(
        T Value,
        bool isValid,
        string? CancelReason);
}