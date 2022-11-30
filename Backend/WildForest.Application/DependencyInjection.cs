using Microsoft.Extensions.DependencyInjection;
using WildForest.Application.Authentication.Commands.RegisterUser;
using WildForest.Application.Authentication.Queries.LoginUser;
using WildForest.Application.Weather.Queries.GetTodayForecast;

namespace WildForest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserRegistrator, UserRegistrator>();
            services.AddScoped<IUserLogger, UserLogger>();

            services.AddScoped<IWeatherForecastDetector, WeatherForecastDetector>();
            
            return services;
        }
    }
}