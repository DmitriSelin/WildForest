using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows;
using WildForest.Frontend.Services.Weather.Interfaces;

namespace WildForest.Frontend.ViewModels
{
    internal class WeatherViewModel : ObservableObject
    {
        private readonly IWeatherService _weatherService;

        #region Token

        public string Token { get; private set; }

        internal void SetToken(string token)
        {
            Token = token;
        }

        #endregion

        #region Commands

        #region WeatherCommand

        public IAsyncRelayCommand WeatherCommand { get; }

        private bool isFirstLoaded = true;

        private async Task GetWeatherAsync()
        {
            if (!isFirstLoaded)
                return;

            var response = await _weatherService.GetTodayWeatherForecastAsync(Token);

            if (response.WeatherForecast is not null)
            {

            }
            else
            {
                MessageBox.Show(response.Title, "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            isFirstLoaded = false;
        }

        #endregion

        #endregion

        public WeatherViewModel(IWeatherService weatherService)
        {
            _weatherService = weatherService;

            #region Commands

            WeatherCommand = new AsyncRelayCommand(GetWeatherAsync);

            #endregion
        }
    }
}