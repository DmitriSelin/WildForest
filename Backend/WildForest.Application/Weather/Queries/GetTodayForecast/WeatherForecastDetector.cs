﻿using WildForest.Application.Weather.Common;

namespace WildForest.Application.Weather.Queries.GetTodayForecast
{
    public class WeatherForecastDetector : IWeatherForecastDetector
    {
        public async Task<WeatherForecust> GetTodayWeatherForecast(TodayForecastQuery query)
        {
            //find weather with cityId
            throw new Exception();
            //get today weather
        }
    }
}