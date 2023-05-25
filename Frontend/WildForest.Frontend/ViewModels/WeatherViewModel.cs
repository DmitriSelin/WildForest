using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
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

            var weatherForecast = await _weatherService.GetTodayWeatherForecastAsync(Token);
            isFirstLoaded = false;            

            await Task.CompletedTask;
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