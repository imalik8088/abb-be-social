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

/*
 * Written by: Robert Gustavsson
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */

namespace ABBConnect___Windows_Phone
{
    /// <summary>
    /// This is the login page of the application, which enables accessing the application and also to store the usered credentails for future automatic logins.
    /// </summary>
    public partial class LogIn : PhoneApplicationPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LogIn()
        {
            InitializeComponent();

            CheckCredentials();
            
        }

        /// <summary>
        /// If the login get redirected to, the textboxes is filled in with the information from the login user
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string username, pw;

            var settings = IsolatedStorageSettings.ApplicationSettings;

            if (settings.TryGetValue<string>("userName", out username)) //check if user is saved in local DB
            {
                txtUsername.Text = username;
                if (settings.TryGetValue<string>("password", out pw)) //check if password is stored
                {
                    //Set username and password in the textboxes
                    txtPassword.Password = pw;
                    txtUsername.Text = username;
                }
            }
            else //display empty
            {
                txtPassword.Password = "";
                txtUsername.Text = "";
            }
        }

        /// <summary>
        /// This method checks the inputed credentials from the user, if they are correct it redirects to mainpage, otherwise it gives an error and the user hae another try.
        /// </summary>
        private async  void CheckCredentials()
        {
            UserManager um = new UserManager();
            var settings = IsolatedStorageSettings.ApplicationSettings;
            string username, pw;

            if (settings.TryGetValue<string>("userName", out username))//check if stored in local DB
            {
                txtUsername.Text = username; 
                if (settings.TryGetValue<string>("password", out pw)) //check if stored in local DB
                {
                    txtPassword.Password = pw;

                    try
                    {
                        if (await um.Login(username, pw)) //try to log in
                            //redirect to homepage on success
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

        /// <summary>
        /// Occours when the user clicks log in, the system tries to log him in. 
        /// If Remeber me is ticket the sysem saves his credentails in local DB and next time he will be logged in automatically
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UserManager um = new UserManager();

            try
            {
                if (await um.Login(txtUsername.Text, txtPassword.Password)) //check if tthe user inserted valid information
                {
                    if (chbRemeber.IsChecked == true) //if the user wants to be remebered
                    {

                        //save information in the local DB
                        SaveStringObject(txtUsername.Text, txtPassword.Password);
                    }

                    //redirect to homepage
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

        /// <summary>
        /// Saves the credentails to the local DB
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pw"></param>
        public void SaveStringObject(string username, string pw)
        {
            //save to the local memory
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Add("userName", username);
            settings.Add("password", pw);
        }
    }
}