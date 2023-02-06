using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Infrastructure.Context;

namespace WildForest.Infrastructure.Persistence.Repositories
{
    public sealed class CityRepository : ICityRepository
    {
        private readonly WildForestDbContext _context;

        public CityRepository(WildForestDbContext context)
        {
            _context = context;
        }

        public async Task<City?> GetCityByIdAsync(CityId cityId)
        {
            return await _context.Cities.FirstOrDefaultAsync(x => x.Id == cityId);
        }

        public async Task<IEnumerable<City>?> GetCitiesByCountryIdAsync(CountryId countryId)
        {
            return await _context.Cities
                .Where(x => x.CountryId == countryId)
                .OrderBy(x => x.CityName)
                .ToListAsync();
        }
    }
}