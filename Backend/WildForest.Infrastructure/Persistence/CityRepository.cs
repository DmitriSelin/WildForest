using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Infrastructure.Context;

namespace WildForest.Infrastructure.Persistence
{
    public class CityRepository : ICityRepository
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
    }
}