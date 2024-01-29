using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Application.Weather.Commands.AddWeatherForecasts;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Domain.Languages.Entities;
using WildForest.Infrastructure.Common.Extensions;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Infrastructure.Persistence.DataInitialization.Converters;

namespace WildForest.Infrastructure.Persistence.DataInitialization;

public static class InitializationDbExtension
{
    private const int weekLength = 7;

    public static IServiceCollection InitializeData(this IServiceCollection services)
    {
        using var context = services.BuildServiceProvider().GetRequiredService<WildForestDbContext>();
        bool isAppInDocker = DockerExtensions.IsAppInDocker();

        bool isDataInitialized = context!.Countries.Any();

        if (!isDataInitialized)
            InitializeData(context, isAppInDocker);

        InitializeWeatherForecasts(context, services);

        return services;
    }

    private static void InitializeData(WildForestDbContext context, bool isAppInDocker)
    {
        var country = InitializeCountry(context);
        InitializeCities(context, country.Id, isAppInDocker);
        InitializeLanguages(context);

        context.SaveChanges();
    }

    private static void InitializeWeatherForecasts(WildForestDbContext context, IServiceCollection services)
    {
        bool weatherForecastsExistInDb = context.WeatherForecasts.Any();

        if (weatherForecastsExistInDb)
            return;

        var serviceProvider = services.BuildServiceProvider();
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        var weatherForecastDbService = serviceProvider.GetRequiredService<IWeatherForecastDbService>();

        var cities = (List<City>)unitOfWork.CityRepository.GetDistinctCitiesFromUsersAsync().GetAwaiter().GetResult();

        foreach (var city in cities)
        {
            weatherForecastDbService.AddWeatherForecastsInDbAsync(city.Id).GetAwaiter().GetResult();
        }

        DeleteOldWeatherData(context);
    }

    private static void DeleteOldWeatherData(WildForestDbContext context)
    {
        var oldWeatherData = context.WeatherForecasts.Where(x => x.Date < DateOnly.FromDateTime(DateTime.Now.AddDays(-weekLength))).ToList();

        if (oldWeatherData is not null && oldWeatherData.Count > 0)
        {
            context.WeatherForecasts.RemoveRange(oldWeatherData);
            context.SaveChanges();
        }
    }

    private static Country InitializeCountry(WildForestDbContext context)
    {
        var countryName = CountryName.Create("Россия");
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
        List<City>? cities = JsonSerializer.Deserialize(fs, typeof(List<City>), jsonOptions) as List<City>;

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