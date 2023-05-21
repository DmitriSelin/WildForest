using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using WildForest.Frontend.Contracts.Authentication;
using WildForest.Frontend.Contracts.Maps;
using WildForest.Frontend.Services.Authentication.Interfaces;
using WildForest.Frontend.Validators.Authentication;

namespace WildForest.Frontend.ViewModels
{
    internal class LoginViewModel : ObservableObject
    {
        private readonly IMapService _mapsService;
        private readonly IAuthenticationValidator _authenticationValidator;
        private readonly IAuthenticationService _authenticationService;
        private MainViewModel? _mainViewModel;

        #region Properties

        private string email = null!;

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string password = null!;

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        #endregion

        #region Commands

        #region OpenCountryViewCommand

        public IAsyncRelayCommand OpenCountryViewCommand { get; }

        private async Task OpenCountryView()
        {
            List<CountryDto> countries = await _mapsService.GetCountriesAsync();

            if (_mainViewModel is null)
                _mainViewModel = (MainViewModel)App.Current.Services.GetService(typeof(MainViewModel))!;

            _mainViewModel.ShowCountryView(countries);
        }

        #endregion

        #region LoginCommand

        public IAsyncRelayCommand LoginCommand { get; }

        private async Task LoginAsync()
        {
            var validationResult = _authenticationValidator.Validate(Email, Password);

            if (!validationResult.IsValid)
            {
                MessageBox.Show($"{validationResult.CancelReason}", "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            var loginRequest = new LoginRequest(Email, Password);

            var response = await _authenticationService.LoginAsync(loginRequest);

            if (response.Response is not null)
            {
                if (_mainViewModel is null)
                    _mainViewModel = (MainViewModel)App.Current.Services.GetService(typeof(MainViewModel))!;

                _mainViewModel.ShowHomeView($"{response.Response.LastName} {response.Response.FirstName}", response.Response.CityName);
            }
            else
            {
                MessageBox.Show(response.Title, "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        #endregion

        #endregion

        public LoginViewModel(
            IMapService mapsService,
            IAuthenticationValidator authenticationValidator,
            IAuthenticationService authenticationService)
        {
            _mapsService = mapsService;
            _authenticationValidator = authenticationValidator;
            _authenticationService = authenticationService;

            #region Commands
            OpenCountryViewCommand = new AsyncRelayCommand(OpenCountryView);
            LoginCommand = new AsyncRelayCommand(LoginAsync);
            _authenticationService = authenticationService;
            #endregion
        }
    }
}