using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WildForest.Frontend.Common;
using WildForest.Frontend.Contracts.Maps;
using WildForest.Frontend.Services.Authentication.Interfaces;

namespace WildForest.Frontend.Services.Authentication;

/// <summary>
/// Service for getting lists of countries and cities
/// </summary>
internal class MapService : IMapService
{
    private readonly HttpClient _httpClient;

    public MapService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Method for getting list of countries
    /// </summary>
    /// <returns>List<CountryDto></returns>
    public async Task<List<CountryDto>> GetCountriesAsync()
    {
        var countries = await _httpClient.GetFromJsonAsync<List<CountryDto>?>($"{ApiItemKeys.BaseUrl}/auth/countries");

        return countries!;
    }

    /// <summary>
    /// Method for getting list of cities by id of country
    /// </summary>
    /// <param name="countryId">Id of country</param>
    /// <returns>List<CityDto></returns>
    public async Task<List<CityDto>> GetCitiesAsync(Guid countryId)
    {
        var cities = await _httpClient.GetFromJsonAsync<List<CityDto>?>($"{ApiItemKeys.BaseUrl}/auth/cities/{countryId}");

        return cities!;
    }
}