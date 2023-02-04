using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public sealed class PrecipitationProbability : ValueObject
    {
        public byte Value { get; }

        private PrecipitationProbability(byte value)
            => Value = value;

        public static PrecipitationProbability CreatePrecipitationProbability(byte value)
        {
            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}