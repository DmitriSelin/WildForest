using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Users.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
        => Value = value;

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        string email = value.Trim();

        if (Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") == false || email.Length > 50)
            throw new ValidationException("Invalid email");

        return new Email(email);
    }

    public override string ToString()
        => Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}