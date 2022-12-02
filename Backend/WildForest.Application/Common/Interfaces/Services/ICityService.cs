using ErrorOr;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Services
{
    public interface ICityService
    {
        Task<ErrorOr<CityId>> GetCityIdAsync(UserId userId);
    }
}