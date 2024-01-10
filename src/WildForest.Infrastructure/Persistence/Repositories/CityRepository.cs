using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class CityRepository : ICityRepository
{
    private readonly WildForestDbContext _context;

    public CityRepository(WildForestDbContext context)
    {
        _context = context;
    }

    public async Task<City?> GetCityByIdAsync(CityId cityId)
    {
        return await _context.Cities
            .FirstOrDefaultAsync(x => x.Id == cityId);
    }

    public async Task<IEnumerable<City>?> GetCitiesByCountryIdAsync(CountryId countryId)
    {
        return await _context.Cities
            .Where(x => x.CountryId == countryId)
            .OrderBy(x => x.Name.Value)
            .ToListAsync();
    }

    public async Task<IEnumerable<City>> GetDistinctCitiesFromUsersAsync()
    {
        return await _context.Cities.FromSqlRaw("""
                SELECT DISTINCT c."Id", c."Name", c."Latitude",
                c."Longitude", c."CountryId" FROM public."Cities" c
                INNER JOIN public."Users" u
                ON c."Id" = u."CityId"
                """).ToListAsync();
    }

    public async Task AddCitiesAsync(List<City> cities)
    {
        await _context.Cities.AddRangeAsync(cities);
    }

    public async Task<IEnumerable<City>> GetCitiesByUserIdAsync(UserId userId)
    {
        var sqlQuery = $"""
                SELECT c."Id", c."Name", c."Latitude", c."Longitude", c."CountryId"
                FROM "Cities" c
                INNER JOIN "Countries" co ON c."CountryId" = co."Id"
                WHERE c."CountryId" IN
                    (SELECT "CountryId" FROM "Cities"
                    WHERE "Id" IN
                        (SELECT "CityId" FROM "Users"
                        WHERE "Id" = '{userId.Value}'))
                ORDER BY c."Name"
                """;

        return await _context.Cities.FromSqlRaw(sqlQuery).ToListAsync();
    }
}