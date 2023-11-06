using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Languages.ValueObjects;

public sealed class LanguageId : ValueObject
{
    public Guid Value { get; }

    private LanguageId(Guid value)
        => Value = value;

    public static LanguageId Create()
        => new(Guid.NewGuid());

    public static LanguageId Create(Guid value)
        => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}