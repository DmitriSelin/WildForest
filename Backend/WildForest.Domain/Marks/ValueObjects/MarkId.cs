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

        public static MarkId Create()
        {
            return new(Guid.NewGuid());
        }

        public static MarkId Create(Guid value)
        {
            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static MarkId Parse(string markId)
        {
            return Create(Guid.Parse(markId));
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}