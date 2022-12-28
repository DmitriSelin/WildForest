using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Infrastructure.Context;

namespace WildForest.Console.Countries.Services
{
    public class CountryService : ICountryService
    {
        private readonly IConfiguration _configuration;

        public CountryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task AddCountryAsync(string countryName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WildForestDbContext>();
            var options = optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL")).Options;
            var context = new WildForestDbContext(options);

            var country = new Country(CountryId.CreateCountryId(), countryName);

            await context.Countries.AddAsync(country);
            await context.SaveChangesAsync();
        }
    }
}