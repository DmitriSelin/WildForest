using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Tokens.ValueObjects;

public sealed class RefreshTokenId :  ValueObject
{
    public Guid Value { get; }

    private RefreshTokenId(Guid value)
        => Value = value;

    public static RefreshTokenId Create()
    {
        return new(Guid.NewGuid());
    }

    public static RefreshTokenId Create(Guid value)
    {
        return new(value);
    }

    public static RefreshTokenId Parse(string refreshTokenId)
    {
        return Create(Guid.Parse(refreshTokenId));
    } 

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}