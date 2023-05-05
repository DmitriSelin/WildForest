using Microsoft.Extensions.DependencyInjection;

namespace WildForest.Frontend.ViewModels
{
    public static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LoginViewModel>();

            return services;
        }
    }
}