using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public sealed class ForecastTime : ValueObject
    {
        public TimeOnly Value { get; }

        private ForecastTime(TimeOnly value)
            => Value = value;

        public static ForecastTime CreateForecastTime(TimeOnly value)
        {
            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}