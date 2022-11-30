using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Users.ValueObjects
{
    public sealed class UserId : ValueObject
    {
        public Guid Value { get; }

        private UserId(Guid value)
        {
            Value = value;
        }

        public static UserId CreateUserId()
        {
            return new(Guid.NewGuid());
        }

        public static UserId CreateUserId(Guid value)
        {
            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}