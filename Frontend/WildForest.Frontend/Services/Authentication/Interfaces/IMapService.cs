using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WildForest.Frontend.Contracts.Maps;

namespace WildForest.Frontend.Services.Authentication.Interfaces
{
    /// <summary>
    /// Service for getting lists of countries and cities
    /// </summary>
    internal interface IMapService
    {
        /// <summary>
        /// Method for getting list of countries
        /// </summary>
        /// <returns>List<CountryDto></returns>
        Task<List<CountryDto>> GetCountriesAsync();

        /// <summary>
        /// Method for getting list of cities by id of country
        /// </summary>
        /// <param name="countryId">Id of country</param>
        /// <returns>List<CityDto></returns>
        Task<List<CityDto>> GetCitiesAsync(Guid countryId);
    }
}