using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WildForest.Frontend.Contracts.Maps;

namespace WildForest.Frontend.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        #region Properties

        private ObservableObject currentViewModel;

        public ObservableObject CurrentViewModel
        {
            get => currentViewModel;
            set => SetProperty(ref currentViewModel, value);
        }

        #endregion

        #region ViewModels

        private readonly LoginViewModel _loginViewModel;
        private readonly RegisterViewModel _registerViewModel;
        private readonly CountryViewModel _countryViewModel;
        private readonly HomeViewModel _homeViewModel;

        #endregion

        #region Commands

        #region CloseAppCommand

        public ICommand CloseAppCommand { get; }

        private void CloseApp()
            => App.Current.Shutdown();

        #endregion

        #region CollapseAppCommand

        public ICommand CollapseAppCommand { get; }

        private void CollapseApp()
        {
            App.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        #endregion

        #region CollapseAppInWindowCommand

        public ICommand CollapseAppInWindowCommand { get; }

        private void CollapseAppInWindow()
        {
            var window = App.Current.MainWindow;

            if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.WindowState = WindowState.Normal;
            }
        }

        #endregion

        #endregion

        internal void ShowRegisterView(List<CityDto> cities)
        {
            if (CurrentViewModel != _registerViewModel)
                CurrentViewModel = _registerViewModel;

            _registerViewModel.SetCities(cities);
        }

        internal void ShowLoginView()
        {
            if (CurrentViewModel != _loginViewModel)
                CurrentViewModel = _loginViewModel;
        }

        internal void ShowCountryView(List<CountryDto> countries)
        {
            if (CurrentViewModel != _countryViewModel)
                CurrentViewModel = _countryViewModel;

            _countryViewModel.SetCountries(countries);
        }

        internal void ShowHomeView()
        {
            if (CurrentViewModel != _homeViewModel)
                CurrentViewModel = _homeViewModel;
        }

        public MainViewModel(
            LoginViewModel loginViewModel,
            RegisterViewModel registerViewModel,
            CountryViewModel countryViewModel,
            HomeViewModel homeViewModel)
        {
            _loginViewModel = loginViewModel;
            CurrentViewModel = _loginViewModel;
            _registerViewModel = registerViewModel;
            _countryViewModel = countryViewModel;
            _homeViewModel = homeViewModel;

            #region Commands

            CloseAppCommand = new RelayCommand(CloseApp);
            CollapseAppCommand = new RelayCommand(CollapseApp);
            CollapseAppInWindowCommand = new RelayCommand(CollapseAppInWindow);
            _countryViewModel = countryViewModel;
            _homeViewModel = homeViewModel;

            #endregion
        }
    }
}