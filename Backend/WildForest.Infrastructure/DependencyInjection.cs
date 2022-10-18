using Microsoft.Extensions.DependencyInjection;
using WildForest.Application.Common.Interfaces.Persistence;

namespace WildForest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, IUserRepository>();

            return services;
        }
    }
}