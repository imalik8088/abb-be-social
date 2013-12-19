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
    public partial class TagControl : PhoneApplicationPage
    {
        UserManager um;
        List<string> chosenUsers;
        List<User> users;

        public TagControl()
        {
            InitializeComponent();
            um = new UserManager();
            chosenUsers = new List<string>();
            FillList("");
        }


        private void txtbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillList(txtbSearch.Text);
        }

        private async void FillList(string query)
        {
            users = await um.SearchUserByName(query);

            lstbSearchResult.Items.Clear();

            foreach (User u in users)
            {
                if (chosenUsers.Contains(u.UserName))
                    continue;

                if (u is Human)
                    lstbSearchResult.Items.Add(new SearchResultControl(((Human)u).FirstName, ((Human)u).LastName, ((Human)u).UserName, ((Human)u).ID, false));
                else
                {
                    //Uncomment to enableing sensor tagging
                    //lstbSearchResult.Items.Add(new SearchResultControl("", "", ((Sensor)u).UserName, ((Sensor)u).ID, false));
                }
            }
        }

        private void lstbSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchResultControl u = lstbSearchResult.SelectedValue as SearchResultControl;

            if (u == null)
                return;

            if (chosenUsers.Contains(u.UserName))
            {
                chosenUsers.Remove(u.UserName);            
            }
            else
            {
                chosenUsers.Add(u.UserName);
                lstbSearchResult.Items.Remove(u);
                lstbTagedUsers.Items.Add(u);
            }
        }
        private void lstbTagedUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchResultControl u = lstbTagedUsers.SelectedValue as SearchResultControl;

            if (u == null)
                return;

            if (chosenUsers.Contains(u.UserName))
            {
                chosenUsers.Remove(u.UserName);
                lstbTagedUsers.Items.Remove(u);
                lstbSearchResult.Items.Add(u);
            }
            else
            {
                chosenUsers.Add(u.UserName);
            }
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            App.Tags = chosenUsers;
            NavigationService.GoBack();
        }

    }
}