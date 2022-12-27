﻿using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Domain.Users.Entities;

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

        public City(CityId id, string name, Location location) : base(id)
        {
            Name = name;
            Location = location;
        }

        public City(CityId id) : base(id) { }
    }
}