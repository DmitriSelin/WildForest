using System;
using System.Windows;
using System.Windows.Input;

namespace WildForest.Frontend.Views;

public partial class MainView : Window
{
    public MainView() => InitializeComponent();

    private void MoveWindow(object sender, MouseButtonEventArgs e)
    {
        try
        {
            DragMove();
        }
        catch (Exception) { }
    }
}