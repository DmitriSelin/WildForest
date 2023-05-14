using Microsoft.Extensions.DependencyInjection;
using WildForest.Frontend.Services.Authentication;
using WildForest.Frontend.Services.Authentication.Interfaces;

namespace WildForest.Frontend.Services
{
    internal static class DependencyInjection
    {
        internal static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpClient<IMapService, MapService>();

            return services;
        }
    }
}