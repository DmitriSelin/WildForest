using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WildForest.Domain.Cities.Entities;
using WildForest.Infrastructure.Context;

namespace WildForest.Console.Cities.Services
{
    public class CityService : ICityService
    {
        public async Task AddCitiesAsync(List<City> cities)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WildForestDbContext>();

            var options = optionsBuilder.UseNpgsql("").Options;

            var context = new WildForestDbContext(options);

            await context.AddRangeAsync(cities);
            await context.SaveChangesAsync();
        }

        public async Task<List<City>> GetCitiesFromJsonFileAsync(string fileName)
        {
            List<City>? cities;

            using (var fs = new FileStream($"Modules/WildForest.Console/Data/{fileName}.json", FileMode.Open))
            {
                cities = await JsonSerializer.DeserializeAsync(fs, typeof(List<City>)) as List<City>;
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