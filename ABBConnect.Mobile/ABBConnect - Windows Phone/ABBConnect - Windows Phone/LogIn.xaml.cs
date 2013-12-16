using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PortableBLL;
using System.IO.IsolatedStorage;

namespace ABBConnect___Windows_Phone
{
    public partial class LogIn : PhoneApplicationPage
    {
        public LogIn()
        {
            InitializeComponent();

            CheckCredentials();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string username, pw;

            var settings = IsolatedStorageSettings.ApplicationSettings;

            if (settings.TryGetValue<string>("userName", out username))
            {
                txtUsername.Text = username;
                if (settings.TryGetValue<string>("password", out pw))
                {

                    txtPassword.Password = pw;
                    txtUsername.Text = username;
                }
            }
            else
            {
                txtPassword.Password = "";
                txtUsername.Text = "";
            }


       

        }


        private async  void CheckCredentials()
        {
            UserManager um = new UserManager();
            var settings = IsolatedStorageSettings.ApplicationSettings;
            string username, pw;

            if (settings.TryGetValue<string>("userName", out username))
            {
                txtUsername.Text = username;
                if (settings.TryGetValue<string>("password", out pw))
                {
                    txtPassword.Password = pw;

                    try
                    {
                        if (await um.Login(username, pw))
                            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml?userName=" + txtUsername.Text, UriKind.Relative));
                
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("To use this application internet connection is needed!");
                        return;
                    }
}
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UserManager um = new UserManager();
           
            try
            {
                if (await um.Login(txtUsername.Text, txtPassword.Password))
                {
                    if (chbRemeber.IsChecked == true)
                    {
                        SaveStringObject(txtUsername.Text, txtPassword.Password);
                    }

                    (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainPage.xaml?userName=" + txtUsername.Text, UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }



        public void SaveStringObject(string username, string pw)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Add("userName", username);
            settings.Add("password", pw);
        }
    }
}