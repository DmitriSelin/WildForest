using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Domain.Languages.Entities;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Infrastructure.Persistence.DataInitialization.Converters;

namespace WildForest.Infrastructure.Persistence.DataInitialization;

public static class InitializationDbExtension
{
    public static IServiceCollection InitializeData(this IServiceCollection services, bool isAppInDocker)
    {
        using var context = services.BuildServiceProvider().GetRequiredService<WildForestDbContext>();
        
        bool isDataInitialized = context!.Countries.Any();

        if (!isDataInitialized)
            InitializeData(context, isAppInDocker);

        return services;
    }

    private static void InitializeData(WildForestDbContext context, bool isAppInDocker)
    {
        var country = InitializeCountry(context);
        InitializeCities(context, country.Id, isAppInDocker);
        InitializeLanguages(context);

        context.SaveChanges();
    }

    private static Country InitializeCountry(WildForestDbContext context)
    {
        var countryName = CountryName.Create("Russia");
        var country = Country.Create(countryName);
        context.Countries.Add(country);

        return country;
    }

    private static void InitializeCities(WildForestDbContext context, CountryId countryId, bool isAppInDocker)
    {
        var jsonOptions = new JsonSerializerOptions();
        jsonOptions.Converters.Add(new CityConverter(countryId));

        string relativePath = string.Empty;

        if (isAppInDocker)
            relativePath = "/app/ru.json";
        else
            relativePath = "ru.json";

        using var fs = new FileStream(relativePath, FileMode.Open);
        var cities = JsonSerializer.Deserialize(fs, typeof(List<City>), jsonOptions) as List<City>;

        if (cities is null)
            throw new ArgumentNullException(nameof(cities), "Invalid json file");

        context.Cities.AddRange(cities);
    }

    private static void InitializeLanguages(WildForestDbContext context)
    {
        var english = Language.Create("English");
        var russian = Language.Create("Русский");
        context.Languages.Add(english);
        context.Languages.Add(russian);
    }
}