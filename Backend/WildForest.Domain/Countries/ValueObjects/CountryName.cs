using System.ComponentModel.DataAnnotations;
using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Countries.ValueObjects
{
    public sealed class CountryName : ValueObject
    {
        public string Value { get; }

        private CountryName(string value)
            => Value = value;

        public static CountryName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            string countryName = value.Trim();

            if (countryName.Length < 3 || countryName.Length > 50)
                throw new ValidationException("Invalid country's name");

            return new(countryName);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}