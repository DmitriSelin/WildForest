using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using WildForest.Frontend.Contracts.Maps;
using WildForest.Frontend.Services.Authentication.Interfaces;

namespace WildForest.Frontend.ViewModels
{
    internal class LoginViewModel : ObservableObject
    {
        private readonly IMapService _mapsService;
        private MainViewModel? mainViewModel;

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

        private string loginText = "Log in";

        public string LoginText
        {
            get => loginText;
            set => SetProperty(ref loginText, value);
        }

        #endregion

        #region Commands

        public IAsyncRelayCommand OpenCountryViewCommand { get; }

        private async Task OpenCountryView()
        {
            List<CountryDto> countries = await _mapsService.GetCountriesAsync();

            if (mainViewModel is null)
                mainViewModel = (MainViewModel)App.Current.Services.GetService(typeof(MainViewModel))!;

            mainViewModel.ShowCountryView(countries);
        }

        #endregion

        public LoginViewModel(IMapService mapsService)
        {
            #region Commands

            OpenCountryViewCommand = new AsyncRelayCommand(OpenCountryView);
            _mapsService = mapsService;

            #endregion
        }
    }
}