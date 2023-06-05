using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WildForest.Frontend.Contracts.Weather.Models;
using WildForest.Frontend.Resources.ItemKeys;
using WildForest.Frontend.Services.Weather.Extensions;
using WildForest.Frontend.Services.Weather.Interfaces;

namespace WildForest.Frontend.ViewModels
{
    internal partial class WeatherViewModel : ObservableObject
    {
        internal static Guid CurrentWeatherId { get; private set; }

        #region Properties

        [ObservableProperty]
        public List<WeatherForecastDto> forecasts = null!;

        private readonly IWeatherService _weatherService;

        [ObservableProperty]
        private WeatherForecastDto currentWeatherForecast = null!;

        [ObservableProperty]
        private string imagePath = null!;

        #endregion

        #region Token

        public string Token { get; private set; } = null!;

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
                var currentHour = TimeOnly.FromTimeSpan(DateTime.Now.TimeOfDay)
                    .PutIntoTimeInterval();

                CurrentWeatherForecast = response.WeatherForecast.WeatherForecasts.FirstOrDefault(x => x.Time == currentHour)!;
                Forecasts = response.WeatherForecast.WeatherForecasts.OrderBy(x => x.Time).ToList();
                ImagePath = WeatherImage.GetWeatherImage(CurrentWeatherForecast.WeatherDescription.Name);
                CurrentWeatherId = CurrentWeatherForecast.WeatherId;
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