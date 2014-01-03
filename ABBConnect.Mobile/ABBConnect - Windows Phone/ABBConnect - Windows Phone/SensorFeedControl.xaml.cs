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
    /// The control that is handling feeds that are published from a sensor
    /// </summary>
    public partial class SensorFeedControl : UserControl
    {
        private PortableBLL.SensorFeed sf;

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public SensorFeedControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SensorFeedControl(int id, string username, string content, string location, DateTime dateTime)
        {
            InitializeComponent();

            SetAuthor(id, username);
            SetContent(content);
            SetLocation(location);
            SetTimeStamp(dateTime);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SensorFeedControl(PortableBLL.SensorFeed sf)
        {
            InitializeComponent();

            SetAuthor(sf.Owner.ID, sf.Owner.UserName);
            SetContent(sf.Content);
            SetLocation(sf.Location);
            SetTimeStamp(sf.TimeStamp);
            this.sf = sf;
        }

        #endregion

        /// <summary>
        /// If the author name is clicked, redirect to the sensors profile page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Author_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //redirect to sensor profile page
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?userID=" + Author.Tag, UriKind.Relative));

        }

        /// <summary>
        /// If the feed is clicked, redirect the user to the feed page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Content_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Redirect to the feed page
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/HumanFeed.xaml", UriKind.Relative));
        }

        /// <summary>
        /// update the comments and time since the feed was posted
        /// </summary>
        /// <param name="comments"></param>
        internal void UpdateComments(List<PortableBLL.Comment> comments)
        {
            //udpate the feed
            sf.Comments = comments;
            //TODO: ADD COMMENTS TO SENSOR CONTROL  SetNumberOfComments(comments.Count);
        }

        #region Setters

        /// <summary>
        /// set the author of the feed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        internal void SetAuthor(int id, string username)
        {
            Author.Text = username;
            Author.Tag = id;
        }
      
        /// <summary>
        /// Set the content to the feed
        /// </summary>
        /// <param name="p"></param>
        internal void SetContent(string p)
        {
            Text.Text = p;
        }

        /// <summary>
        /// set how long since the feed was posted
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
        /// Set the location of the sensor
        /// </summary>
        /// <param name="p"></param>
        internal void SetLocation(string p)
        {
            Location.Text = p;
        }

        #endregion
    }
}
