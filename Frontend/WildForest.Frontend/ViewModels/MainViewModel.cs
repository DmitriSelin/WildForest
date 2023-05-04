using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;

namespace WildForest.Frontend.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
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

        public MainViewModel()
        {
            #region Commands

            CloseAppCommand = new RelayCommand(CloseApp);
            CollapseAppCommand = new RelayCommand(CollapseApp);
            CollapseAppInWindowCommand = new RelayCommand(CollapseAppInWindow);

            #endregion
        }
    }
}