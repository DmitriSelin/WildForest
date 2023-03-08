using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Tokens.ValueObjects;

public sealed class ReplacedByToken : ValueObject
{
    public string? Value { get; private set; }

    private ReplacedByToken(string? value)
        => Value = value;

    public static ReplacedByToken Create(string? value)
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