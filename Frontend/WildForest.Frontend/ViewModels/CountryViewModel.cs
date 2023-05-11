using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace WildForest.Frontend.ViewModels
{
    internal class CountryViewModel : ObservableObject
    {
        private MainViewModel mainViewModel;

        #region Commands

        public ICommand TestCommand { get; }

        private void Test()
        {
            if (mainViewModel is null)
                mainViewModel = (MainViewModel)App.Current.Services.GetService(typeof(MainViewModel))!;

            mainViewModel.ShowHomeView();
        }

        #endregion

        public CountryViewModel()
        {
            #region Commands

            TestCommand = new RelayCommand(Test);

            #endregion
        }
    }
}