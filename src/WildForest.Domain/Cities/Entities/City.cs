﻿using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Weather;

namespace WildForest.Domain.Cities.Entities;

public class City : Entity<CityId>
{
    public CityName Name { get; } = null!;

    public Location Location { get; } = null!;

    public CountryId CountryId { get; } = null!;

    public Country Country { get; } = null!;

    private readonly List<User> _users = new();

    public IReadOnlyList<User> Users => _users.AsReadOnly();

    private readonly List<WeatherForecast> _weatherForecasts = new();

    public IReadOnlyList<WeatherForecast> WeatherForecasts => _weatherForecasts.AsReadOnly();

    private City(
        CityId id,
        CityName name,
        Location location,
        CountryId countryId) : base(id)
    {
        Name = name;
        Location = location;
        CountryId = countryId;
    }

    public static City Create(CityName name, Location location, CountryId countryId)
    {
        return new(
            CityId.Create(),
            name,
            location,
            countryId);
    }

#pragma warning disable IDE0051 // Remove unused private members
    private City(CityId id) : base(id) { }
#pragma warning restore IDE0051 // Remove unused private members
}