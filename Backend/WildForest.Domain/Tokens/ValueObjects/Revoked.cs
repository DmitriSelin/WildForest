using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Tokens.ValueObjects;

public sealed class Revoked : ValueObject
{
    public DateTime Value { get; }

    private Revoked(DateTime value)
        => Value = value;

    public static Revoked Create(DateTime value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}