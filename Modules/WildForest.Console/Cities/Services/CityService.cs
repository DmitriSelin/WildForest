using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WildForest.Console.Common.JsonSettings;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Infrastructure.Context;

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

        public async Task<List<City>> GetCitiesFromJsonFileAsync(string fileName)
        {
            List<City>? cities;

            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.Converters.Add(new CityConverter());

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
    }
}