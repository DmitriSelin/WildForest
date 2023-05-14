using Microsoft.Extensions.DependencyInjection;
using WildForest.Frontend.Validators.Authentication;

namespace WildForest.Frontend.Validators
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationValidator, AuthenticationValidator>();

            return services;
        }
    }
}