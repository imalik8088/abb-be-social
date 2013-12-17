using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ABBConnect___Windows_Phone
{
    public partial class SearchResultControl : UserControl
    {
        int userID;
        bool redirect;

        string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }


        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public SearchResultControl(string firstname, string lastname, string username, int ID, bool redirect)
        {
            InitializeComponent();
           

            string display = firstname + " " + lastname + "   (" + username + ")";
            userID = ID;
            this.redirect = redirect;
            this.userName = username;

            lblUserName.Text = display;
        }

        private void lblUserName_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(redirect)
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?userID=" + userID, UriKind.Relative));
        }

    }
}
