using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public sealed class PrecipitationVolume : ValueObject
    {
        public double? Value { get; }

        private PrecipitationVolume(double? value)
            => Value = value;

        public static PrecipitationVolume Create(double? value)
        {
            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value ?? 0;
        }
    }
}