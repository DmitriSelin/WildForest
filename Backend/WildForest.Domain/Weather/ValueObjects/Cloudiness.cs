using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public sealed class Cloudiness : ValueObject
    {
        public byte Value { get; }

        private Cloudiness(byte value)
            => Value = value;

        public static Cloudiness Create(byte value)
        {
            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}