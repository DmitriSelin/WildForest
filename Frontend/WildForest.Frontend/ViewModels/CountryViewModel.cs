using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using WildForest.Frontend.Contracts.Maps;
using WildForest.Frontend.Services.Authentication.Interfaces;

namespace WildForest.Frontend.ViewModels
{
    internal class CountryViewModel : ObservableObject
    {
        private readonly IMapService _mapService;
        private MainViewModel mainViewModel;

        public List<CountryDto> Countries { get; private set; }
        
        public object? SelectedCountry { get; set; }

        #region Commands

        public IAsyncRelayCommand OpenRegisterViewCommand { get; }

        private async Task OpenRegisterViewAsync()
        {
            if (mainViewModel is null)
                mainViewModel = (MainViewModel)App.Current.Services.GetService(typeof(MainViewModel))!;

            if (SelectedCountry is not null)
            {
                try
                {
                    var country = (CountryDto)SelectedCountry;
                    var cities = await _mapService.GetCitiesAsync(country.CountryId);

                    mainViewModel.ShowRegisterView(cities);
                }
                catch (Exception)
                {
                    MessageBox.Show("Server is not available", "Wild forest", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Choose your country", "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        #endregion

        internal void SetCountries(List<CountryDto> countries)
        {
            Countries = countries;
        }

        public CountryViewModel(IMapService mapService)
        {
            _mapService = mapService;

            OpenRegisterViewCommand = new AsyncRelayCommand(OpenRegisterViewAsync);
        }
    }
}