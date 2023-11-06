using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Countries.ValueObjects;

public class CountryId : ValueObject
{
    public Guid Value { get; }

    private CountryId(Guid value)
        => Value = value;

    public static CountryId Create()
        => new (Guid.NewGuid());

    public static CountryId Create(Guid value)
        => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
        => Value.ToString();
}