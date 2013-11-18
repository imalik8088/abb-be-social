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
    public partial class SensorFeedControl : UserControl
    {


        public SensorFeedControl()
        {
            InitializeComponent();
        }

        public SensorFeedControl(string author, int id, string content, string location, DateTime dateTime)
        {
            InitializeComponent();

            SetAuthor(author, id);
            SetContent(content);
            SetLocation(location);
            SetTimeStamp(dateTime);
        }
        private void Author_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?username=" + Author.Tag, UriKind.Relative));

        }

        private void Content_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Feed.xaml", UriKind.Relative));
        }


        internal void SetAuthor(string p, int id)
        {
            Author.Text = p;
            Author.Tag = id;
        }

        internal void SetContent(string p)
        {
            Text.Text = p;
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
