using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WildForest.Frontend.Common;
using WildForest.Frontend.Contracts.Maps;
using WildForest.Frontend.Services.Authentication.Interfaces;

namespace WildForest.Frontend.Services.Authentication
{
    internal class MapService : IMapService
    {
        private readonly HttpClient _httpClient;

        public MapService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CountryDto>> GetCountriesAsync()
        {
            var countries = await _httpClient.GetFromJsonAsync<List<CountryDto>?>($"{ApiItemKeys.BaseUrl}/auth/countries");

            return countries!;
        }

        public async Task<List<CityDto>> GetCitiesAsync(Guid countryId)
        {
            var cities = await _httpClient.GetFromJsonAsync<List<CityDto>?>($"{ApiItemKeys.BaseUrl}/auth/cities/{countryId}");

            return cities!;
        }
    }
}