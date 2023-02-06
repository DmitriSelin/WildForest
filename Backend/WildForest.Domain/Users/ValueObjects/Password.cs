using System.ComponentModel.DataAnnotations;
using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Users.ValueObjects
{
    public sealed class Password : ValueObject
    {
        public string Value { get; }

        private Password(string value)
            => Value = value;

        public static Password CreatePassword(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            if (value.Length < 6 || value.Length > 50)
                throw new ValidationException("Invalid Password");

            return new(value);
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