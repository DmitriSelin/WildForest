using Microsoft.Extensions.DependencyInjection;
using WildForest.Application.Authentication.Commands.RegisterUser;

namespace WildForest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserRegistrator, UserRegistrator>();
            
            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services)
        {
            services.AddScoped<IUserRegistrator, UserRegistrator>();

            return services;
        }
    }
}