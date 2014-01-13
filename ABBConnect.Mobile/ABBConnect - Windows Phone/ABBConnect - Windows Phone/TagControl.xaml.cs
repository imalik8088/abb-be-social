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
    /// A page that enables adding tags to a feed that shall be posted
    /// </summary>
    public partial class TagControl : PhoneApplicationPage
    {
        UserManager um;
        List<string> chosenUsers;
        List<User> users;

        /// <summary>
        /// Constructor
        /// </summary>
        public TagControl()
        {
            InitializeComponent();
            um = new UserManager();
            chosenUsers = new List<string>();
            FillList("");
        }

        /// <summary>
        /// When the user is changing the text box, displays users depending on the inserted text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillList(txtbSearch.Text);
        }

        /// <summary>
        /// Fills the list of users depending on the text inputed and also depending on the users that are already selected
        /// </summary>
        /// <param name="query"></param>
        private async void FillList(string query)
        {
            try
            {
                //get the users matching the user input
                users = await um.SearchUserByName(query);

            }
            catch (Exception)
            {
                MessageBox.Show("Cannot connect to the server, please check your connections");
                return;
            }

            //restet previous results
            lstbSearchResult.Items.Clear();

            foreach (User u in users)
            {
                if (chosenUsers.Contains(u.UserName)) //if the user is already tagged, don't show him
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

        /// <summary>
        /// Occurs when the user wants to add a tag from the search results
        /// Adds the user to the tag list and disable searching on that user again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstbSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get the control that was clicked
            SearchResultControl u = lstbSearchResult.SelectedValue as SearchResultControl;

            if (u == null)
                return;

            if (chosenUsers.Contains(u.UserName)) //if it was already choosed
            {
                chosenUsers.Remove(u.UserName);   //remove ir          
            }
            else
            {
                //add it to the chosen users
                chosenUsers.Add(u.UserName);

                //make it disables to search for that user again
                lstbSearchResult.Items.Remove(u);
                lstbTagedUsers.Items.Add(u);
            }
        }
      
        /// <summary>
        /// Occurs when the user wants to remove a tag from the tagged users
        /// Removes the user to the tag list and enable searching on that user again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstbTagedUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get the clicked control
            SearchResultControl u = lstbTagedUsers.SelectedValue as SearchResultControl;

            if (u == null)
                return;

            if (chosenUsers.Contains(u.UserName)) //if the user was already tagged
            {
                //Untag him
                chosenUsers.Remove(u.UserName);

                //enalble searching that user again
                lstbTagedUsers.Items.Remove(u);
                lstbSearchResult.Items.Add(u);
            }
            else
            {
                chosenUsers.Add(u.UserName);
            }
        }

        /// <summary>
        /// Occurs when the user clicks "Done"
        /// Saves all the taged user in a list and is redirected back to the "post feed" page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            //set the tags 
            App.Tags = chosenUsers;
            //go back to the publish page
            NavigationService.GoBack();
        }

    }
}