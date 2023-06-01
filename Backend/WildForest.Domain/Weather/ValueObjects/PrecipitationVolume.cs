using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    /// <summary>
    /// Value in mm
    /// </summary>
    public sealed class PrecipitationVolume : ValueObject
    {
        public double Value { get; }

        private PrecipitationVolume(double value)
            => Value = value;

        public static PrecipitationVolume Create(double value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Invalid precipitation probability");

            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}