using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence
{
    public interface ICityRepository
    {
        Task<City?> GetCityByIdAsync(CityId cityId);
    }
}