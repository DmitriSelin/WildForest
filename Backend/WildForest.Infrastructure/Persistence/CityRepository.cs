using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;

namespace WildForest.Infrastructure.Persistence
{
    public class CityRepository : ICityRepository
    {
        private List<City> cities = new List<City>();

        public async Task<City?> GetCityByIdAsync(CityId cityId)
        {
            return await Task.Run(() => GetCityById(cityId));
        }

        private City? GetCityById(CityId cityId)
        {
            return cities.FirstOrDefault(x => x.Id == cityId);
        }
    }
}