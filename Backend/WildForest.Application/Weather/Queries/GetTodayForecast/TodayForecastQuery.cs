﻿using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Weather.Queries.GetTodayForecast
{
    public record TodayForecastQuery(UserId UserId, CityId CityId);
}