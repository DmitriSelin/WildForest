using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Tokens.ValueObjects;

public sealed class CreationDate : ValueObject
{
    public DateTime Value { get; }

    private CreationDate(DateTime value)
        => Value = value;

    public static CreationDate Create(DateTime value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}