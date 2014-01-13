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
    /// A Control that holds the information of a feed that should be shown in the feed list in the MainPage
    /// </summary>
    public partial class NoImageFeedControl : UserControl
    {
        private PortableBLL.HumanFeed hFeed;

        /// <summary>
        /// Constructor
        /// </summary>
        public NoImageFeedControl()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Constructor with the object HumanFeed from the BLL as param
        /// </summary>
        /// <param name="hf"></param>
        public NoImageFeedControl(PortableBLL.HumanFeed hf)
        {
            InitializeComponent();


            //set the content
            SetAuthor(hf.Owner);
            SetContent(hf.Content);
            SetNumberOfTags(hf.Tags.Count);
            SetNumberOfComments(hf.Comments.Count);
            SetLocation(hf.Location);
            SetTimeStamp(hf.TimeStamp);
            hFeed = hf;
            base.Height = 140;
            base.Width = 456;
        }

        /// <summary>
        /// if the author is clicked the application redirects the user to that users profile page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Author_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //redirect to the profile page
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?userID=" + Author.Tag, UriKind.Relative));

        }

        /// <summary>
        /// If somewhere else than the author name is clicked the application reddirects to the feed page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Content_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //set this feed to the current feed and redirect to its feed page
            App.HFeed = hFeed;   
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/HumanFeed.xaml", UriKind.Relative));
        }

        /// <summary>
        ///update the feed, in sense of number of comments and how long it has passed since it was published
        /// </summary>
        /// <param name="comments"></param>
        internal void UpdateFeed(List<Comment> comments)
        {
            //update the feed comments and time
            hFeed.Comments = comments;
            SetNumberOfComments(comments.Count);
            SetTimeStamp(hFeed.TimeStamp);
        }

        #region Setters
        /// <summary>
        /// sets the author name
        /// </summary>
        /// <param name="h"></param>
        internal void SetAuthor(Human h)
        {
            Author.Text = h.FirstName + " " + h.LastName;
            Author.Tag = h.ID;
        }
        
        /// <summary>
        /// sets the feed content
        /// </summary>
        /// <param name="p"></param>
        internal void SetContent(string p)
        {
            if (p.Length > 80)
            {
                string tmp = p.Substring(0, 80);

                Text.Text =  tmp.TrimEnd() + "...";
            }
            else
                Text.Text = p;

           // gridFeedControl.Height = Text.Height;
        }

        /// <summary>
        /// set the numner of feeds
        /// </summary>
        /// <param name="p"></param>
        internal void SetNumberOfTags(int p)
        {
            Tags.Text = p.ToString();
        }
 

        /// <summary>
        /// Sets the number of comments of the feed
        /// </summary>
        /// <param name="p"></param>
        internal void SetNumberOfComments(int p)
        {
            Comments.Text = p.ToString();
        }

        /// <summary>
        /// set the amount of time that has passed since the feed was published
        /// </summary>
        /// <param name="dateTime"></param>
        internal void SetTimeStamp(DateTime dateTime)
        {
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
        /// Set the location of the feed
        /// </summary>
        /// <param name="p"></param>
        internal void SetLocation(string p)
        {
            Location.Text = p;
        }
        #endregion
    }
}
