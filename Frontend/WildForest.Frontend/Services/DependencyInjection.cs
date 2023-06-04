﻿using Microsoft.Extensions.DependencyInjection;
using WildForest.Frontend.Services.Authentication;
using WildForest.Frontend.Services.Authentication.Interfaces;
using WildForest.Frontend.Services.Marks;
using WildForest.Frontend.Services.Marks.Interfaces;
using WildForest.Frontend.Services.Weather;
using WildForest.Frontend.Services.Weather.Interfaces;

namespace WildForest.Frontend.Services
{
    internal static class DependencyInjection
    {
        internal static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpClient<IMapService, MapService>();
            services.AddHttpClient<IAuthenticationService, AuthenticationService>();
            services.AddHttpClient<IWeatherService, WeatherService>();
            services.AddHttpClient<IMarkService, MarkService>();

            return services;
        }
    }
}