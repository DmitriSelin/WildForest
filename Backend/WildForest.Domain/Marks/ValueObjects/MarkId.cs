using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Marks.ValueObjects
{
    public sealed class MarkId : ValueObject
    {
        public Guid Value { get; }

        private MarkId(Guid value)
        {
            Value = value;
        }

        public static MarkId CreateMarkId()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}