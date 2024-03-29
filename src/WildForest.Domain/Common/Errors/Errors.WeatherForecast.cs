﻿using ErrorOr;

namespace WildForest.Domain.Common.Errors;

public static partial class Errors
{
    public static class WeatherForecast
    {
        public static Error NotFound => Error.NotFound(
            code: "WeatherForecast.NotFound",
            description: "Not found weather forecast for these city and date");

        public static Error NotFoundById => Error.NotFound(
            code: "WeatherForecast.NotFoundById",
            description: "Not found weather forecast for by id");
    }
}