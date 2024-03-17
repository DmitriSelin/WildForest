using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Infrastructure.Persistence.Repositories.Base;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class CountryRepository : Repository<Country>, ICountryRepository
{
    public CountryRepository(WildForestDbContext context) : base(context) { }

    public async Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        return await Context.Countries
            .OrderBy(x => x.Name.Value)
            .ToListAsync();
    }

    public async Task<Country?> GetCountryByNameAsync(CountryName countryName)
    {
        return await Context.Countries
            .FirstOrDefaultAsync(x => x.Name.Value == countryName.Value);
    }

    public async Task<Country?> GetCountryByIdAsync(CountryId countryId)
    {
        return await Context.Countries
            .FirstOrDefaultAsync(x => x.Id == countryId);
    }
}