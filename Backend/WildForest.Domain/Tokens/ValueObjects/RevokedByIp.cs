using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Tokens.ValueObjects;

public sealed class RevokedByIp : ValueObject
{
    public string? Value { get; private set; }

    private RevokedByIp(string? value)
        => Value = value;

    public static RevokedByIp Create(string? value)
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