using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WildForest.Frontend.ViewModels;

namespace WildForest.Frontend
{
    public sealed partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            this.InitializeComponent();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddViewModels();

            return services.BuildServiceProvider();
        }
    }
}