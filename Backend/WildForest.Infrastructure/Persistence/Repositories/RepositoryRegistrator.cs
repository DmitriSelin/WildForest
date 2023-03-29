using Microsoft.Extensions.DependencyInjection;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;

namespace WildForest.Infrastructure.Persistence.Repositories
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IMarkRepository, MarkRepository>();

            return services;
        }
    }
}