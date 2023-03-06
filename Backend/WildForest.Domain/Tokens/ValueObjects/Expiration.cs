using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Tokens.ValueObjects;

public sealed class Expiration : ValueObject
{
    public DateTime Value { get; }

    private Expiration(DateTime value)
        => Value = value;

    public static Expiration Create(DateTime value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}