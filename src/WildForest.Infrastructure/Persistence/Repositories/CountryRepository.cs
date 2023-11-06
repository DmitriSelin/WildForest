using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class CountryRepository : ICountryRepository
{
    private readonly WildForestDbContext _context;

    public CountryRepository(WildForestDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        return await _context.Countries
            .OrderBy(x => x.Name.Value)
            .ToListAsync();
    }

    public async Task AddCountryAsync(Country country)
    {
        await _context.Countries.AddAsync(country);
    }

    public async Task<Country?> GetCountryByNameAsync(CountryName countryName)
    {
        return await _context.Countries
            .FirstOrDefaultAsync(x => x.Name.Value == countryName.Value);
    }

    public async Task<Country?> GetCountryByIdAsync(CountryId countryId)
    {
        return await _context.Countries
            .FirstOrDefaultAsync(x => x.Id == countryId);
    }
}