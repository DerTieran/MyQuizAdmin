using MyQuizAdmin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyQuizAdmin.Controls
{
    public sealed partial class LoginDialog : ContentDialog
    {
        public LoginDialog()
        {
            this.InitializeComponent();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            loginButton.IsEnabled = false;
            passwordBox.IsEnabled = false;
            Request request = new Request();
            RegistrationResponse register = await request.register(new RegistrationData
            {
                password = passwordBox.Password
            });

            if (register != null && register.IsAdmin == 1)
            {
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values["deviceID"] = register.Id.ToString();
                Hide();
            }
            else
            {
                helpText.Text = "Falsches Passwort! Bitte nochmal eingeben.";
                helpText.Foreground = new SolidColorBrush(Colors.Red);
            }
            loginButton.IsEnabled = true;
            passwordBox.IsEnabled = true;
            passwordBox.Focus(FocusState.Programmatic);
            passwordBox.SelectAll();
        }

        private void passwordBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                loginButton_Click(sender, e);
                e.Handled = true;
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            loginButton.IsEnabled = passwordBox.Password.Length > 0;
        }
    }
}
