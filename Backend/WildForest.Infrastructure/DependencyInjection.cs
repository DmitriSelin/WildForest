using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Application.Common.Interfaces.Services;
using WildForest.Infrastructure.Authentication;
using WildForest.Infrastructure.Persistence;
using WildForest.Infrastructure.Services;

namespace WildForest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}