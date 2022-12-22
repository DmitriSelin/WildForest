using Microsoft.Extensions.DependencyInjection;
using WildForest.Application.Common.Interfaces.Persistence;

namespace WildForest.Infrastructure.Persistence
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IDayWeatherRepository, DayWeatherRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();

            return services;
        }
    }
}