using Microsoft.Extensions.DependencyInjection;

namespace WildForest.Frontend.ViewModels;

public static class ViewModelRegistrator
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<RegisterViewModel>();
        services.AddSingleton<CountryViewModel>();
        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<WeatherViewModel>();
        services.AddSingleton<CommentsViewModel>();
        services.AddSingleton<ChartViewModel>();

        return services;
    }
}