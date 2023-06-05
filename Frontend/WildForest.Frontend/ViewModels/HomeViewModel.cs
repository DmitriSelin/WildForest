using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
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

        private string fullName;

        public string FullName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }

        private string cityName;

        public string CityName
        {
            get => cityName;
            set => SetProperty(ref cityName, value);
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

        internal void SetCredentials(
            string fullName,
            string cityName,
            string token,
            Guid userId)
        {
            FullName = fullName;
            CityName = cityName;
            _weatherViewModel.SetToken(token);
        }

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