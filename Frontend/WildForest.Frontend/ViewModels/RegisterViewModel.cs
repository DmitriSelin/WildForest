using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace WildForest.Frontend.ViewModels
{
    internal class RegisterViewModel : ObservableObject
    {
        private MainViewModel? _mainViewModel;

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

        #endregion

        public RegisterViewModel()
        {
            #region Commands

            OpenLoginViewCommand = new RelayCommand(OpenLoginView);

            #endregion
        }
    }
}