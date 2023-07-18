using Microsoft.Extensions.DependencyInjection;
using WildForest.Application.Authentication.Commands.RefreshTokens;
using WildForest.Application.Authentication.Commands.RegisterUser;
using WildForest.Application.Authentication.Commands.RevokeTokens;
using WildForest.Application.Authentication.Queries.LoginUser;
using WildForest.Application.Common.Mapping;
using WildForest.Application.Maps.Commands.AddCities;
using WildForest.Application.Maps.Commands.AddCountry;
using WildForest.Application.Maps.Queries.GetCitiesList;
using WildForest.Application.Maps.Queries.GetCountriesList;
using WildForest.Application.Weather.Commands.AddWeatherForecasts.Fabrics;
using WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

namespace WildForest.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IRefreshTokenCommandHandler, RefreshTokenCommandHandler>();
        services.AddScoped<IRevokeTokenCommandHandler, RevokeTokenCommandHandler>();
        services.AddScoped<ICountriesListQueryHandler, CountriesListQueryHandler>();
        services.AddScoped<ICitiesListQueryHandler, CitiesListQueryHandler>();
        services.AddScoped<IHomeWeatherForecastService, HomeWeatherForecastService>();
        services.AddScoped<ICountryCommandHandler, CountryCommandHandler>();
        services.AddScoped<ICityCommandHandler, CityCommandHandler>();

        services.AddScoped<IWeatherForecastFactory, WeatherForecastFactory>();

        services.AddMappings();

        return services;
    }
}