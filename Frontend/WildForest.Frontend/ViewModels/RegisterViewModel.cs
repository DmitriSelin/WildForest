using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WildForest.Frontend.Contracts.Maps;
using System.Collections.Generic;
using System.Threading.Tasks;
using WildForest.Frontend.Validators.Authentication;
using System.Windows;
using WildForest.Frontend.Contracts.Authentication;
using WildForest.Frontend.Services.Authentication.Interfaces;

namespace WildForest.Frontend.ViewModels
{
    internal class RegisterViewModel : ObservableObject
    {
        private MainViewModel? _mainViewModel;
        private readonly IAuthenticationValidator _authenticationValidator;
        private readonly IAuthenticationService _authenticationService;

        public List<CityDto> Cities { get; private set; }

        public object? SelectedCity { get; set; }

        internal void SetCities(List<CityDto> cities)
        {
            Cities = cities;
        }

        #region Properties

        private string firstName;

        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        private string lastName;

        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        private string email;

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string cityName;

        public string CityName
        {
            get => cityName;
            set => SetProperty(ref cityName, value);
        }

        private string password;

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private string samePassword;

        public string SamePassword
        {
            get => samePassword;
            set => SetProperty(ref samePassword, value);
        }

        #endregion

        #region Commands

        #region OpenLoginViewCommand

        public ICommand OpenLoginViewCommand { get; }

        private void OpenLoginView()
        {
            if (_mainViewModel is null)
                _mainViewModel = (MainViewModel)App.Current.Services.GetService(typeof(MainViewModel))!;

            _mainViewModel.ShowLoginView();
        }

        #endregion

        #region OpenHomeViewCommand

        public IAsyncRelayCommand OpenHomeViewCommand { get; }

        private async Task OpenHomeViewAsync()
        {
            var validationResult = _authenticationValidator.Validate(FirstName, LastName, Email, Password, SamePassword);

            if (!validationResult.isValid)
            {
                MessageBox.Show($"{validationResult.CancelReason}", "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            if (SelectedCity is null)
            {
                MessageBox.Show("Choose your city", "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            var city = (CityDto)SelectedCity;

            var registerRequest = new RegisterRequest(FirstName, LastName, Email, Password, city.CityId);

            var response = await _authenticationService.RegisterAsync(registerRequest);

            if (response.Response is not null)
            {
                if (_mainViewModel is null)
                    _mainViewModel = (MainViewModel)App.Current.Services.GetService(typeof(MainViewModel))!;

                _mainViewModel.ShowHomeView();
            }
            else
            {
                MessageBox.Show(response.Title, "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        #endregion

        #endregion

        public RegisterViewModel(IAuthenticationValidator authenticationValidator, IAuthenticationService authenticationService)
        {
            _authenticationValidator = authenticationValidator;
            _authenticationService = authenticationService;

            #region Commands

            OpenLoginViewCommand = new RelayCommand(OpenLoginView);
            OpenHomeViewCommand = new AsyncRelayCommand(OpenHomeViewAsync);

            #endregion
        }
    }
}