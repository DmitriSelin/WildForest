using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Countries.Entities;
using WildForest.Infrastructure.Context;

namespace WildForest.Infrastructure.Persistence
{
    public class CountryRepository : ICountryRepository
    {
        private readonly WildForestDbContext _context;

        public CountryRepository(WildForestDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Country>> GetAllCountriesAsync()
        {
            return _context.Countries;
        }
    }
}