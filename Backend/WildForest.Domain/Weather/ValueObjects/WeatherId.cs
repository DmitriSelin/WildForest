using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public class WeatherId : ValueObject
    {
        public Guid Value { get; }

        private WeatherId(Guid value)
        {
            Value = value;
        }

        public static WeatherId CreateWeatherId()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}