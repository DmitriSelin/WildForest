﻿using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Users.Entities;

namespace WildForest.Domain.Cities.Entities
{
    public class City : Entity<CityId>
    {
        public string Name { get; } = null!;

        public Location Location { get; } = null!;

        public virtual ICollection<User> Users { get; set; } = null!;

        public City(CityId id, string name, Location location) : base(id)
        {
            Name = name;
            Location = location;
        }

        public City(CityId cityId) : base(cityId) { }
    }
}