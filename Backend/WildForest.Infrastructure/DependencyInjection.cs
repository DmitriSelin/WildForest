using Microsoft.Extensions.DependencyInjection;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Infrastructure.Persistence;

namespace WildForest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}