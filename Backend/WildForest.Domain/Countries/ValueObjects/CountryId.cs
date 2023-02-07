using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Countries.ValueObjects
{
    public class CountryId : ValueObject
    {
        public Guid Value { get; }

        private CountryId(Guid value)
        {
            Value = value;
        }

        public static CountryId Create()
        {
            return new(Guid.NewGuid());
        }

        public static CountryId Create(Guid value)
        {
            return new(value);
        }

        public static CountryId Parse(string countryId)
        {
            return Create(Guid.Parse(countryId));
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