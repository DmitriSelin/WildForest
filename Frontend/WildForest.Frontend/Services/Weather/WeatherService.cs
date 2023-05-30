﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WildForest.Frontend.Common;
using WildForest.Frontend.Contracts.Authentication;
using WildForest.Frontend.Contracts.Weather;
using WildForest.Frontend.Services.Weather.Interfaces;

namespace WildForest.Frontend.Services.Weather;

public sealed class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResponseBase> GetTodayWeatherForecastAsync(string token)
    {
        var forecastDate = $"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}";

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync($"{ApiItemKeys.BaseUrl}/weather/forecast/homeCity/{forecastDate}");

        string body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var weatherForecast = JsonSerializer.Deserialize<WeatherForecastVm>(body);
            return new(weatherForecast, (int)response.StatusCode, null);
        }

        var badResponse = JsonSerializer.Deserialize<BadResponse>(body);
        return new(null, badResponse!.Status, badResponse.Title);
    }
}