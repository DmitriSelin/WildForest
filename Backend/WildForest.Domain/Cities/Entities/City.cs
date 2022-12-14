using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Domain.Cities.Entities
{
    public class City : Entity<CityId>
    {
        public string Name { get; } = null!;

        public Location Location { get; } = null!;

        public CountryId CountryId { get; } = null!;

        public virtual Country Country { get; } = null!;

        private readonly List<User> _users = new();

        public IReadOnlyList<User> Users => _users.AsReadOnly();

        private readonly List<DayWeather> _dayWeather = new();

        public IReadOnlyList<DayWeather> DayWeather => _dayWeather.AsReadOnly();

        private City(CityId id, string name, Location location) : base(id)
        {
            Name = name;
            Location = location;
        }

        private City(CityId id) : base(id) { }

        public static City CreateCity(string name, Location location)
        {
            return new(
                CityId.CreateCityId(),
                name,
                location);
        }

        public static City CreateCity() => new(CityId.CreateCityId());
    }
}