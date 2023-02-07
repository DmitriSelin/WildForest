﻿using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Domain.Cities.Entities
{
    public class City : Entity<CityId>
    {
        public CityName CityName { get; } = null!;

        public Location Location { get; } = null!;

        public CountryId CountryId { get; } = null!;

        public virtual Country Country { get; } = null!;

        private readonly List<User> _users = new();

        public IReadOnlyList<User> Users => _users.AsReadOnly();

        private readonly List<WeatherForecast> _weatherForecasts = new();

        public IReadOnlyList<WeatherForecast> WeatherForecasts => _weatherForecasts.AsReadOnly();

        private City(
            CityId id,
            CityName cityName,
            Location location,
            CountryId countryId) : base(id)
        {
            CityName = cityName;
            Location = location;
            CountryId = countryId;
        }

        private City(CityId id) : base(id) { }

        public static City Create(CityName cityName, Location location, CountryId countryId)
        {
            return new(
                CityId.Create(),
                cityName,
                location,
                countryId);
        }
    }
}