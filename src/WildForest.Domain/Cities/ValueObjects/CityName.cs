using System.ComponentModel.DataAnnotations;
using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Cities.ValueObjects;

public sealed class CityName : ValueObject
{
    public string Value { get; }

    private CityName(string value)
        => Value = value;

    public static CityName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        string cityName = value.Trim();

        if (cityName.Length < 1 || cityName.Length > 50)
            throw new ValidationException("Invalid cityName");

        return new(cityName);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}