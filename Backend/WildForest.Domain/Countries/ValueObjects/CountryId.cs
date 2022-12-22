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

        public static CountryId CreateCountryId()
        {
            return new(Guid.NewGuid());
        }

        public static CountryId CreateCountryId(Guid value)
        {
            return new(value);
        }

        public static CountryId Parse(string countryId)
        {
            return CreateCountryId(Guid.Parse(countryId));
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