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
    public partial class NoImageFeedControl : UserControl
    {
        public NoImageFeedControl()
        {
            InitializeComponent();
        }

        public NoImageFeedControl(string author, string username, string content, int noOfTags, int noOfComments, string location, DateTime timestamp)
        {
            InitializeComponent();
            SetAuthor(author, username);
            SetContent(content);
            SetNumberOfTags(noOfTags);
            SetNumberOfComments(noOfComments);
            SetLocation(location);
            SetTimeStamp(timestamp);
        }

        private void Author_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?username=" + Author.Tag, UriKind.Relative));

        }

        private void Content_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Feed.xaml", UriKind.Relative));
        }

        internal void SetAuthor(string p, string username)
        {
            Author.Text = p;
            Author.Tag = username;
        }

        internal void SetContent(string p)
        {
            Text.Text = p;
        }

        internal void SetNumberOfTags(int p)
        {
            Tags.Text = p.ToString();
        }

        internal void SetNumberOfComments(int p)
        {
            Comments.Text = p.ToString();
        }

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

        internal void SetLocation(string p)
        {
            Location.Text = p;
        }
    }
}
