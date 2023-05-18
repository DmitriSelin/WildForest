using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WildForest.Frontend.Components
{
    public partial class SmallBindablePasswordBox : UserControl
    {
        private bool _isPasswordChanging;

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(SmallBindablePasswordBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PasswordPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SmallBindablePasswordBox passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        public SmallBindablePasswordBox() => InitializeComponent();

        private void PasswordBoxChanged(object sender, RoutedEventArgs e)
        {
            if (CustomTextBox.Text != CustomPasswordBox.Password)
            {
                _isPasswordChanging = true;
                Password = CustomPasswordBox.Password;
                CustomTextBox.Text = Password;
                _isPasswordChanging = false;
            }
        }

        private void UpdatePassword()
        {
            if (!_isPasswordChanging)
            {
                CustomPasswordBox.Password = Password;
                CustomTextBox.Text = Password;
            }
        }

        private void CustomTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustomPasswordBox.Password != CustomTextBox.Text)
            {
                _isPasswordChanging = true;
                Password = CustomTextBox.Text;
                CustomPasswordBox.Password = Password;
                _isPasswordChanging = false;
            }
        }

        #region EyeCheckBox

        private bool isPasswordVisible = false;

        private void ClickEyeCheckBox(object sender, RoutedEventArgs e)
        {
            if (isPasswordVisible)
            {
                isPasswordVisible = false;
                CustomTextBox.Visibility = Visibility.Collapsed;
                CustomPasswordBox.Visibility = Visibility.Visible;
            }
            else
            {
                isPasswordVisible = true;
                CustomPasswordBox.Visibility = Visibility.Collapsed;
                CustomTextBox.Visibility = Visibility.Visible;
            }
        }

        #endregion
    }
}