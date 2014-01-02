using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

/*
 * Written by: Robert Gustavsson
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */

namespace ABBConnect___Windows_Phone
{
    /// <summary>
    /// This class is a control to order the data that is returned when a user is searching for users
    /// </summary>
    public partial class SearchResultControl : UserControl
    {
        int userID;
        bool redirect;
        string userName;

        /// <summary>
        /// Set and Get the username that is belonging to the search result
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// Set and Get the user ID that is belonging to the search result
        /// </summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="username"></param>
        /// <param name="ID"></param>
        /// <param name="redirect"></param>
        public SearchResultControl(string firstname, string lastname, string username, int ID, bool redirect)
        {
            InitializeComponent();
           

            string display = firstname + " " + lastname + "   (" + username + ")";
            userID = ID;
            this.redirect = redirect;
            this.userName = username;

            lblUserName.Text = display;
        }

        /// <summary>
        /// When the control is clicked, the user gets redirected to the profile page that is connected with the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblUserName_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(redirect)
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?userID=" + userID, UriKind.Relative));
        }

    }
}
