using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public sealed class WeatherDescription : ValueObject
    {
        public string Name { get; }

        public string Description { get; }

        private WeatherDescription(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static WeatherDescription Create(string name, string description)
        {
            return new(name, description);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Description;
        }
    }
}