using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WildForest.Console.Common.JsonSettings;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Console.Common.Exceptions;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Console.Cities.Services
{
    public class CityService : ICityService
    {
        private readonly IConfiguration _configuration;

        public CityService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task AddCitiesAsync(List<City> cities)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WildForestDbContext>();
            var options = optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL")).Options;
            var context = new WildForestDbContext(options);

            await context.Cities.AddRangeAsync(cities);
            await context.SaveChangesAsync();
        }

        public async Task<List<City>> GetCitiesFromJsonFileAsync(string fileName, string countryName)
        {
            List<City>? cities;

            var countryId = GetCountryIdByName(countryName);

            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.Converters.Add(new CityConverter(countryId));

            string? path = _configuration["Paths:JsonFilePath"];

            using (var fs = new FileStream($"{path}/{fileName}.json", FileMode.Open))
            {
                cities = await JsonSerializer.DeserializeAsync(fs, typeof(List<City>), jsonOptions) as List<City>;
            }

            if (cities is null)
            {
                throw new FileLoadException($"Can not download data from {fileName}.json");
            }
            else
            {
                return cities;
            }
        }

        private CountryId GetCountryIdByName(string countryName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WildForestDbContext>();
            var options = optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL")).Options;
            var context = new WildForestDbContext(options);

            var country = context.Countries.FirstOrDefault(x => x.CountryName.Value == countryName);

            if (country is null)
            {
                throw new CountryException("There is not a single country with this name");
            }

            return country.Id;
        }
    }
}