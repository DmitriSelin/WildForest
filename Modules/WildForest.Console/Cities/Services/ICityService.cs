using WildForest.Domain.Cities.Entities;

namespace WildForest.Console.Cities.Services
{
    internal interface ICityService
    {
        Task AddCitiesAsync(List<City> cities);

        Task<List<City>> GetCitiesFromJsonFileAsync(string fileName);
    }
}