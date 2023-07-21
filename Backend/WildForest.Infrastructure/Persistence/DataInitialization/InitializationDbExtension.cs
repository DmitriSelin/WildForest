using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Infrastructure.Persistence.DataInitialization.Converters;

namespace WildForest.Infrastructure.Persistence.DataInitialization;

public static class InitializationDbExtension
{
    public static IServiceCollection InitializeData(this IServiceCollection services)
    {
        using var context = services.BuildServiceProvider().GetService<WildForestDbContext>();
        
        bool isDataInitialized = context!.Countries.Any();

        if (!isDataInitialized)
            InitializeData(context);

        return services;
    }

    private static void InitializeData(WildForestDbContext context)
    {
        var country = InitializeCountry(context);

        InitializeCities(context, country.Id);

        context.SaveChanges();
    }

    private static Country InitializeCountry(WildForestDbContext context)
    {
        var countryName = CountryName.Create("Russia");
        var country = Country.Create(countryName);
        context.Countries.Add(country);

        return country;
    }

    private static void InitializeCities(WildForestDbContext context, CountryId countryId)
    {
        var jsonOptions = new JsonSerializerOptions();
        jsonOptions.Converters.Add(new CityConverter(countryId));

        using var fs = new FileStream("ru.json", FileMode.Open);
        var cities = JsonSerializer.Deserialize(fs, typeof(List<City>), jsonOptions) as List<City>;

        if (cities is null)
            throw new ArgumentNullException(nameof(cities), "Invalid json file");

        context.Cities.AddRange(cities);
    }
}