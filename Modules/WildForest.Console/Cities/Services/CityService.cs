using System.Text.Json;
using WildForest.Domain.Cities.Entities;

namespace WildForest.Console.Cities.Services
{
    internal class CityService : ICityService
    {
        public async Task AddCitiesAsync(List<City> cities)
        {
            await Task.CompletedTask;
        }

        public async Task<List<City>> GetCitiesFromJsonFileAsync(string fileName)
        {
            try
            {
                List<City>? cities;

                using (var fs = new FileStream($"{fileName}.json", FileMode.Open))
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
            catch (FileNotFoundException)
            {
                System.Console.WriteLine();
                return new();
            }
            catch (FileLoadException)
            {
                System.Console.WriteLine();
                return new();
            }
        }
    }
}