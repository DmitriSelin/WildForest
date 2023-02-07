using System.ComponentModel.DataAnnotations;
using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Users.ValueObjects
{
    public sealed class FirstName : ValueObject
    {
        public string Value { get; }

        private FirstName(string value)
            => Value = value;

        public static FirstName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            string firstName = value.Trim();

            if (firstName.Length < 2 || firstName.Length > 50)
                throw new ValidationException("Invalid firstName");

            return new(firstName);
        }

        public override string ToString()
        {
            return Value;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}