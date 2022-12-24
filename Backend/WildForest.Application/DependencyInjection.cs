using Microsoft.Extensions.DependencyInjection;
using WildForest.Application.Authentication.Commands.RegisterUser;
using WildForest.Application.Authentication.Queries.LoginUser;
using WildForest.Application.Common.Mapping;
using WildForest.Application.Maps.Queries.GetCitiesList;
using WildForest.Application.Maps.Queries.GetCountriesList;
using WildForest.Application.Weather.Queries.GetTodayForecast;

namespace WildForest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserRegistrator, UserRegistrator>();
            services.AddScoped<IUserLogger, UserLogger>();

            services.AddScoped<ICountriesListQueryHandler, CountriesListQueryHandler>();
            services.AddScoped<ICitiesListQueryHandler, CitiesListQueryHandler>();
            services.AddScoped<IWeatherForecastDetector, WeatherForecastDetector>();

            services.AddMappings();
            
            return services;
        }
    }
}