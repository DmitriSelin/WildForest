using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WildForest.Console.Cities.Services;
using WildForest.Console.Countries.Services;
using WildForest.Console.Services.ConsoleServices;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();

        BuildConfig(builder);

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddTransient<ICityService, CityService>();
                services.AddTransient<IGreatingService, GreatingService>();
                services.AddTransient<ICountryService, CountryService>();
            })
            .Build();

        var service = ActivatorUtilities.CreateInstance<GreatingService>(host.Services);
        service.StartDialog();
    }

    private static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    }
}