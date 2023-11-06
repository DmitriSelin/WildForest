using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Tokens.ValueObjects;

public sealed class RefreshTokenId :  ValueObject
{
    public Guid Value { get; }

    private RefreshTokenId(Guid value)
        => Value = value;

    public static RefreshTokenId Create()
        => new(Guid.NewGuid());

    public static RefreshTokenId Create(Guid value)
        => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
        => Value.ToString();
}