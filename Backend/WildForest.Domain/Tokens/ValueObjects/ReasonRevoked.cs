using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Tokens.ValueObjects;

public sealed class ReasonRevoked : ValueObject
{
    public string? Value { get; }

    private ReasonRevoked(string? value)
        => Value = value;

    public static ReasonRevoked Create(string? value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value!;
    }
}