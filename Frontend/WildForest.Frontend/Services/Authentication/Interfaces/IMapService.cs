using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WildForest.Frontend.Contracts.Maps;

namespace WildForest.Frontend.Services.Authentication.Interfaces
{
    internal interface IMapService
    {
        Task<List<CountryDto>> GetCountriesAsync();

        Task<List<CityDto>> GetCitiesAsync(Guid countryId);
    }
}