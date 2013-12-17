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
using System.Windows.Media;
using System.Windows.Media.Imaging;

/*
 * Written by: Robert Gustavsson
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */

namespace ABBConnect___Windows_Phone
{
    /// <summary>
    /// A control to hold the activity data for a better view
    /// </summary>
    public partial class ActivityControl : UserControl
    {
        Activity activitiy;
        /// <summary>
        /// Constructor that takes an Activity object as param
        /// </summary>
        /// <param name="a"></param>
        public ActivityControl(Activity a)
        {
            InitializeComponent();
            activitiy = a;

            lblText.Text = activitiy.Text;
            SetImage(activitiy.Type);
            SetTime(activitiy.Timestamp);
        }

        /// <summary>
        /// Sets the imgage depending on what type of activity that have occured
        /// </summary>
        /// <param name="type"></param>
        private void SetImage(string type)
        {
            if (type == "Comment")
                imgType.Source = new BitmapImage(new Uri("/Icons/symbol_comment.png", UriKind.Relative));
            else if (type == "Tag")
                imgType.Source = new BitmapImage(new Uri("/Icons/symbol_tag.png", UriKind.Relative));
            else
                imgType.Source = new BitmapImage(new Uri("/Icons/symbol_location.png", UriKind.Relative));
        }

        /// <summary>
        /// Set the label time with how long it has passed since the activity happend
        /// </summary>
        /// <param name="dateTime"></param>
        internal void SetTime(DateTime dateTime)
        {
            DateTime now = DateTime.Now;

            double hours = (now - dateTime).TotalHours;

            if (hours < 1)
                lblTime.Text = Math.Round((now - dateTime).TotalMinutes).ToString() + "m";
            else if (hours > 24)
                lblTime.Text = Math.Round((now - dateTime).TotalDays).ToString() + "d";
            else
                lblTime.Text = Math.Round(hours).ToString() + "h";
        }

        /// <summary>
        /// When the activity is pressed, the user gets redirect to the feed (that the activity is bound to) page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LayoutRoot_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FeedManager fm = new FeedManager();
            Feed f =  await fm.GetFeedByFeedId(activitiy.FeedId);

            App.HFeed = f as PortableBLL.HumanFeed;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/HumanFeed.xaml", UriKind.Relative));
        }

    }
}
