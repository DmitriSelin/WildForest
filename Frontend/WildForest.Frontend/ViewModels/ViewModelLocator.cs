using Microsoft.Extensions.DependencyInjection;

namespace WildForest.Frontend.ViewModels
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel =>
            App.Current.Services.GetRequiredService<MainViewModel>();
    }
}