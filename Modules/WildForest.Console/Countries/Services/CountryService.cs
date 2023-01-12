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

            var country = Country.CreateCountry(countryName);

            await context.Countries.AddAsync(country);
            await context.SaveChangesAsync();
        }

        //public static CountryId GetCountryIdByName(string countryName)
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<WildForestDbContext>();
        //    var options = optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL")).Options;
        //    var context = new WildForestDbContext(options);

        //    var country = context.Countries.FirstOrDefault(x => x.Name == countryName);

        //    return country.Id;
        //}
    }
}