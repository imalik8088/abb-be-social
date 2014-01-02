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

/*
 * Written by: Robert Gustavsson
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */

namespace ABBConnect___Windows_Phone
{
    /// <summary>
    /// This page enables the user to search for other users that is available in the application
    /// </summary>
    public partial class SearchUser : PhoneApplicationPage
    {

        UserManager um;
        /// <summary>
        /// Construcor
        /// </summary>
        public SearchUser()
        {
            InitializeComponent();
            um = new UserManager();
        }

        /// <summary>
        /// An event that occurs when the user is redirected to this page
        /// Empty method for now..
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        /// <summary>
        /// Not used ATM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void txtbSearch_TextInputUpdate(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            List<User> humans = await um.SearchUserByName(txtbSearch.Text);

            lstbSearchResult.ItemsSource = humans;
        }

        /// <summary>
        /// Reads up the users that have a match with the inputted text, if any result is given it will be present in the list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void txtbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<User> users;
            try
            {
                users = await um.SearchUserByName(txtbSearch.Text);

            }
            catch (Exception)
            {
                MessageBox.Show("Cannot connect to the server, please check you internet connection");
                return;
            }

            lstbSearchResult.Items.Clear();

            foreach (User u in users)
            {
                if (u is Human)
                    lstbSearchResult.Items.Add(new SearchResultControl(((Human)u).FirstName, ((Human)u).LastName, ((Human)u).UserName, ((Human)u).ID, true));
                else
                    lstbSearchResult.Items.Add(new SearchResultControl("", "", ((Sensor)u).UserName, ((Sensor)u).ID, true));
            
            }
        }
    }
}