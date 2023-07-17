using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Cities.ValueObjects;

public sealed class CityId : ValueObject
{
    public Guid Value { get; }

    private CityId(Guid value)
        => Value = value;

    public static CityId Create()
        => new(Guid.NewGuid());

    public static CityId Create(Guid value)
        => new (value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
        => Value.ToString();
}