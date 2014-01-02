using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using PortableBLL;

/*
 * Written by: Robert Gustavsson
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */

namespace ABBConnect___Windows_Phone
{
    /// <summary>
    /// The class that represents the whole information of a feed (tags, content and comments)
    /// </summary>
    public partial class HumanFeed : PhoneApplicationPage
    {

        PortableBLL.HumanFeed hf;
      
        /// <summary>
        /// Constructor
        /// </summary>
        public HumanFeed()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the application redirects to this page, the feed is loaded
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            hf = App.HFeed;

            SetLabels();
            AddTags();
            AddComments(hf.Comments);
            AddTime();
     
        }

        /// <summary>
        /// add the tags to the tag-list
        /// </summary>
        private void AddTags()
        {
            lstbTags.ItemsSource = hf.Tags;
        }

        /// <summary>
        /// set the lables of the actual feed
        /// </summary>
        private void SetLabels()
        {
            Author.Text = hf.Owner.FirstName + " " + hf.Owner.LastName;

            //Image.Source = hf.MediaFilePath;
            Location.Text = hf.Location;
            Content.Text = hf.Content;
        }

        /// <summary>
        /// Sets the time that has passed since the feed was published
        /// </summary>
        private void AddTime()
        {
            DateTime dateTime = hf.TimeStamp;
            DateTime now = DateTime.Now;

            double hours = (now - dateTime).TotalHours;

            if (hours < 1)
                Timestamp.Text = Math.Round((now - dateTime).TotalMinutes).ToString() + "m";
            else if (hours > 24)
                Timestamp.Text = Math.Round((now - dateTime).TotalDays).ToString() + "d";
            else
                Timestamp.Text = Math.Round(hours).ToString() + "h";
        }

        /// <summary>
        /// adds all the comments to the comment list, using CommentControl
        /// </summary>
        /// <param name="comments"></param>
        private void AddComments(List<Comment> comments)
        {

            //add the comments to the listbox
            foreach (PortableBLL.Comment c in comments)
            {
                CommentControl cc = new CommentControl(c.Owner.FirstName + " " + c.Owner.LastName, c.TimeStamp, c.ID, c.Content);
                lstbComments.Items.Add(cc);
            }
        }

        /// <summary>
        /// Occurs when the user has clicked publish in the Pivot of comments, this enables the commeting on feeds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnPublish_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtbComment.Text) || String.IsNullOrWhiteSpace(txtbComment.Text))
            {
                MessageBox.Show("No comment attached");
                return;
            }
            btnPublish.IsEnabled = false;

            FeedManager fm = new FeedManager();
            Comment c = new Comment();

            c.Owner = App.CurrentUser;
            c.Content = txtbComment.Text;
            bool result;

            try
            {
                 result = await fm.PublishComment(hf.ID, c);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnPublish.IsEnabled = true;
                return;
            }
           

            if (!result)
                MessageBox.Show("Something went wrong, try again later!");
            else
            {
                List<Comment> comments = await fm.LoadFeedComments(hf.ID);
                lstbComments.Items.Insert(3, new CommentControl(comments[0]));
                lstbComments.UpdateLayout();

                txtbComment.Text = String.Empty;
                MessageBox.Show("Comment published");
            }
            btnPublish.IsEnabled = true;

        }

        /// <summary>
        /// Removes the already written text in the comment textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbComment_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtbComment.Text = String.Empty;
        }

        /// <summary>
        /// Occurs when the user clicks a username in the tag list, this redirects him to that users profile page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstbTags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?userID=" + hf.Tags[lstbTags.SelectedIndex].ID, UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}