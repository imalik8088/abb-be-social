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

namespace ABBConnect___Windows_Phone
{
    public partial class SearchUser : PhoneApplicationPage
    {

        UserManager um;
        public SearchUser()
        {
            InitializeComponent();
            um = new UserManager();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private async void txtbSearch_TextInputUpdate(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            List<User> humans = await um.SearchUserByName(txtbSearch.Text);

            lstbSearchResult.ItemsSource = humans;
        }

        private async void txtbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<User> users = await um.SearchUserByName(txtbSearch.Text);

            lstbSearchResult.Items.Clear();

            foreach (User u in users)
            {
                if (u is Human)
                    lstbSearchResult.Items.Add(new SearchResultControl(((Human)u).FirstName, ((Human)u).LastName, ((Human)u).UserName, ((Human)u).ID));
                else
                    lstbSearchResult.Items.Add(new SearchResultControl("", "", ((Sensor)u).UserName, ((Sensor)u).ID));
            
            }
        }
    }
}