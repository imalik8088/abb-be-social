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

        public SearchResultControl(string firstname, string lastname, string username, int ID)
        {
            InitializeComponent();

            string display = firstname + " " + lastname + "   (" + username + ")";
            userID = ID;

            lblUserName.Text = display;
        }

        private void lblUserName_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?userID=" + userID, UriKind.Relative));

        }
    }
}
