﻿using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Comments.Entities;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Ratings;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Weather;

public class WeatherForecast : AggregateRoot<WeatherForecastId>
{
    public DateOnly Date { get; }

    public CityId CityId { get; } = null!;

    public City City { get; } = null!;

    public Rating Rating { get; } = null!;

    private readonly List<ThreeHourWeatherForecast> _threeHourWeatherForecasts = new();

    public IReadOnlyList<ThreeHourWeatherForecast> ThreeHourWeatherForecasts => _threeHourWeatherForecasts.AsReadOnly();

    private readonly List<Comment> _comments = new();

    public IReadOnlyList<Comment> Comments => _comments.AsReadOnly();

    public void AddThreeHourWeatherForecast(ThreeHourWeatherForecast forecast)
    {
        _threeHourWeatherForecasts.Add(forecast);
    }

    public static WeatherForecast Create(DateOnly date, CityId cityId)
        => new(WeatherForecastId.Create(), date, cityId);

    private WeatherForecast(
        WeatherForecastId id,
        DateOnly date,
        CityId cityId) : base(id)
    {
        Date = date;
        CityId = cityId;
        Rating = Rating.Create(Id);
    }

#pragma warning disable IDE0051 // Remove unused private members
    private WeatherForecast(WeatherForecastId id) : base(id) { }
#pragma warning restore IDE0051 // Remove unused private members
}