using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace WildForest.Frontend.ViewModels
{
    internal class HomeViewModel : ObservableObject
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

        private readonly WeatherViewModel _weatherViewModel;
        private readonly CommentsViewModel _commentsViewModel;

        #endregion

        #region Commands

        #region OpenWeatherViewCommand

        public ICommand OpenWeatherViewCommand { get; }

        private void OpenWeatherView()
        {
            if (CurrentViewModel != _weatherViewModel)
                CurrentViewModel = _weatherViewModel;
        }

        #endregion

        #region OpenCommentsViewCommand

        public ICommand OpenCommentsViewCommand { get; }

        private void OpenCommentsView()
        {
            if (CurrentViewModel != _commentsViewModel)
                CurrentViewModel = _commentsViewModel;
        }

        #endregion

        #endregion

        public HomeViewModel(WeatherViewModel weatherViewModel, CommentsViewModel commentsViewModel)
        {
            _weatherViewModel = weatherViewModel;
            _commentsViewModel = commentsViewModel;

            CurrentViewModel = _weatherViewModel;

            #region Commands

            OpenWeatherViewCommand = new RelayCommand(OpenWeatherView);
            OpenCommentsViewCommand = new RelayCommand(OpenCommentsView);

            #endregion
        }
    }
}