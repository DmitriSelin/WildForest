using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WildForest.Frontend.Contracts.Weather.Models;
using WildForest.Frontend.Services.Weather.Extensions;
using WildForest.Frontend.Services.Weather.Interfaces;

namespace WildForest.Frontend.ViewModels
{
    internal partial class WeatherViewModel : ObservableObject
    {
        private readonly IWeatherService _weatherService;

        private WeatherForecastDto currentWeatherForecast = null!;

        #region Properties

        [ObservableProperty]
        private string windSpeed;

        [ObservableProperty]
        private string direction;

        [ObservableProperty]
        private string windGust;

        [ObservableProperty]
        private string pressure;

        [ObservableProperty]
        private string humidity;

        [ObservableProperty]
        private string precipitationProbability;

        [ObservableProperty]
        private string cloudiness;

        [ObservableProperty]
        private string visibility;

        [ObservableProperty]
        private string precipitationVolume;

        [ObservableProperty]
        private string temperature;

        [ObservableProperty]
        private string temperatureFeelsLike;

        [ObservableProperty]
        private string weatherDescription;

        [ObservableProperty]
        private string firstTemperature;

        [ObservableProperty]
        private string secondTemperature;

        [ObservableProperty]
        private string thirdTemperature;

        [ObservableProperty]
        private string fourthTemperature;

        [ObservableProperty]
        private string fifthTemperature;

        [ObservableProperty]
        private string sixthTemperature;

        [ObservableProperty]
        private string seventhTemperature;

        [ObservableProperty]
        private string eighthTemperature;

        private void FillObservableProperties()
        {
            WindSpeed = currentWeatherForecast.Wind.Speed.ToString();
            Direction = currentWeatherForecast.Wind.Direction.ToString();
            WindGust = currentWeatherForecast.Wind.Gust.ToString();
            Pressure = currentWeatherForecast.Pressure.ToString();
            Humidity = currentWeatherForecast.Humidity.ToString();
            PrecipitationProbability = currentWeatherForecast.PrecipitationProbability.ToString();
            Cloudiness = currentWeatherForecast.Cloudiness.ToString();
            Visibility = currentWeatherForecast.Visibility.ToString();
            PrecipitationVolume = currentWeatherForecast.PrecipitationVolume.ToString();
            Temperature = currentWeatherForecast.Temperature.Value.ToString();
            TemperatureFeelsLike = currentWeatherForecast.Temperature.ValueFeelsLike.ToString();
            WeatherDescription = currentWeatherForecast.WeatherDescription.Description;
        }

        private void FillTemperature(List<WeatherForecastDto> forecasts)
        {
            FirstTemperature = forecasts[0].Temperature.Value.ToString();
            SecondTemperature = forecasts[1].Temperature.Value.ToString();
            ThirdTemperature = forecasts[2].Temperature.Value.ToString();
            FourthTemperature = forecasts[3].Temperature.Value.ToString();
            FifthTemperature = forecasts[4].Temperature.Value.ToString();
            SixthTemperature = forecasts[5].Temperature.Value.ToString();
            SeventhTemperature = forecasts[6].Temperature.Value.ToString();
            EighthTemperature = forecasts[7].Temperature.Value.ToString();
        }

        #endregion

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
                var currentHour = TimeOnly.FromTimeSpan(DateTime.Now.TimeOfDay)
                    .PutIntoTimeInterval();

                currentWeatherForecast = response.WeatherForecast.WeatherForecasts.FirstOrDefault(x => x.Time == currentHour)!;

                FillObservableProperties();
                FillTemperature(response.WeatherForecast.WeatherForecasts);
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