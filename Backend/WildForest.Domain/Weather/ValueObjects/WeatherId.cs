using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public sealed class WeatherId : ValueObject
    {
        public Guid Value { get; }

        private WeatherId(Guid value)
        {
            Value = value;
        }

        public static WeatherId Create()
        {
            return new(Guid.NewGuid());
        }

        public static WeatherId Create(Guid value)
        {
            return new(value);
        }

        public static WeatherId Parse(string weatherId)
        {
            return Create(Guid.Parse(weatherId));
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}