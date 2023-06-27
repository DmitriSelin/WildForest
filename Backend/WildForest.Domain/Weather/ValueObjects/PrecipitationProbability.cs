using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    /// <summary>
    /// Value in percentages
    /// </summary>
    public sealed class PrecipitationProbability : ValueObject
    {
        public byte Value { get; }

        private PrecipitationProbability(byte value)
            => Value = value;

        public static PrecipitationProbability Create(byte value)
        {
            if (value > 100)
                throw new ArgumentOutOfRangeException(nameof(value), "Invalid precipitation probability");

            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}