using System.ComponentModel.DataAnnotations;
using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Users.ValueObjects
{
    public sealed class LastName : ValueObject
    {
        public string Value { get; }

        private LastName(string value)
            => Value = value;

        public static LastName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            string lastName = value.Trim();

            if (lastName.Length < 2 || lastName.Length > 50)
                throw new ValidationException("Invalid lastName");

            return new(lastName);
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