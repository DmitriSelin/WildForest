using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace WildForest.Frontend.ViewModels
{
    internal class LoginViewModel : ObservableObject
    {
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

        public ICommand OpenRegisterViewCommand { get; }

        private void OpenRegisterView()
        {
            if (mainViewModel is null)
                mainViewModel = (MainViewModel)App.Current.Services.GetService(typeof(MainViewModel))!;

            mainViewModel.ShowRegisterView();
        }

        #endregion

        public LoginViewModel()
        {
            #region Commands

            OpenRegisterViewCommand = new RelayCommand(OpenRegisterView);

            #endregion
        }
    }
}