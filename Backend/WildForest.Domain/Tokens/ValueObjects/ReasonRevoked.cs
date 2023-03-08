using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Tokens.ValueObjects;

public sealed class ReasonRevoked : ValueObject
{
    public string? Value { get; private set; }

    private ReasonRevoked(string? value)
        => Value = value;

    public static ReasonRevoked Create(string? value)
    {
        return new(value);
    }

    public void Update(string value)
        => Value = value;
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value!;
    }
}