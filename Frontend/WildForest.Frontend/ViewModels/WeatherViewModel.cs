using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WildForest.Frontend.Contracts.Weather.Models;

namespace WildForest.Frontend.ViewModels
{
    internal class WeatherViewModel : ObservableObject
    {
        public ObservableCollection<WeatherForecastDto> WeatherForecasts { get; set; } = new();

        public double MediumMark { get; set; }

        public WeatherViewModel()
        {

        }
    }
}