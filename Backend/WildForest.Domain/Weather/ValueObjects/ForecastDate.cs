using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    /// <summary>
    /// Value in UTC
    /// </summary>
    public sealed class ForecastDate : ValueObject
    {
        public DateOnly Value { get; }

        private ForecastDate(DateOnly value)
            => Value = value;

        public static ForecastDate Create(DateOnly value)
        {
            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}