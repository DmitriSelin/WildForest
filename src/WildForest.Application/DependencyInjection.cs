﻿using Microsoft.Extensions.DependencyInjection;
using WildForest.Application.Authentication.Commands.Profile;
using WildForest.Application.Authentication.Commands.RefreshTokens;
using WildForest.Application.Authentication.Commands.RegisterUser;
using WildForest.Application.Authentication.Commands.RevokeTokens;
using WildForest.Application.Authentication.Queries.LoginUser;
using WildForest.Application.Authentication.Queries.Registration;
using WildForest.Application.Comments.Commands.Services;
using WildForest.Application.Maps.Commands.AddCountry;
using WildForest.Application.Maps.Queries.GetCitiesList;
using WildForest.Application.Maps.Queries.GetCountriesList;
using WildForest.Application.Ratings.Commands.Votes;
using WildForest.Application.Weather.Commands.AddWeatherForecasts;
using WildForest.Application.Weather.Commands.AddWeatherForecasts.Fabrics;
using WildForest.Application.Weather.Queries.GetHomeWeatherForecast;
using WildForest.Application.Weather.Queries.GetHomeWeatherForecast.Fabrics;

namespace WildForest.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAuthenticationServices();
        services.AddMapServices();
        services.AddWeatherServices();
        services.AddRatings();

        return services;
    }

    private static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IRefreshTokenCommandHandler, RefreshTokenCommandHandler>();
        services.AddScoped<IRevokeTokenCommandHandler, RevokeTokenCommandHandler>();
        services.AddScoped<IAuthCredentialsQueryHandler, AuthCredentialsQueryHandler>();
        services.AddScoped<IProfileService, ProfileService>();

        return services;
    }

    private static IServiceCollection AddMapServices(this IServiceCollection services)
    {
        services.AddScoped<ICountriesListQueryHandler, CountriesListQueryHandler>();
        services.AddScoped<ICitiesListQueryHandler, CitiesListQueryHandler>();
        services.AddScoped<ICountryCommandHandler, CountryCommandHandler>();

        return services;
    }

    private static IServiceCollection AddWeatherServices(this IServiceCollection services)
    {
        services.AddScoped<IHomeWeatherForecastService, HomeWeatherForecastService>();
        services.AddScoped<IWeatherForecastFactory, WeatherForecastFactory>();
        services.AddScoped<IWeatherForecastDbService, WeatherForecastDbService>();
        services.AddScoped<IWeatherForecastResponseFactory, WeatherForecastResponseFactory>();

        return services;
    }

    private static IServiceCollection AddRatings(this IServiceCollection services)
    {
        services.AddScoped<IVoteService, VoteService>();
        services.AddScoped<ICommentService, CommentService>();

        return services;
    }
}