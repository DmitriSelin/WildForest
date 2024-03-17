using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Ratings.ValueObjects;

public sealed class RatingId : ValueObject
{
    public Guid Value { get; }

    private RatingId(Guid value)
        => Value = value;

    public static RatingId Create()
        => new(Guid.NewGuid());

    public static RatingId Create(Guid value)
        => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}