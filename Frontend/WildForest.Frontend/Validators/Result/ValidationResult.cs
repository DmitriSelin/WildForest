namespace WildForest.Frontend.Validators.Result
{
    public sealed record ValidationResult<T>(
        T Value,
        bool IsValid,
        string? CancelReason);
}