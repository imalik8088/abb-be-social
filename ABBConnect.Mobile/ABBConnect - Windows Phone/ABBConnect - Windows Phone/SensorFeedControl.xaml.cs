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
        private PortableBLL.SensorFeed sf;



        public SensorFeedControl()
        {
            InitializeComponent();
        }

        public SensorFeedControl(int id, string username, string content, string location, DateTime dateTime)
        {
            InitializeComponent();

            SetAuthor(id, username);
            SetContent(content);
            SetLocation(location);
            SetTimeStamp(dateTime);
        }

        public SensorFeedControl(PortableBLL.SensorFeed sf)
        {
            InitializeComponent();

            SetAuthor(sf.Owner.ID, sf.Owner.UserName);
            SetContent(sf.Content);
            SetLocation(sf.Location);
            SetTimeStamp(sf.TimeStamp);
        }
        private void Author_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?username=" + Author.Tag, UriKind.Relative));

        }

        private void Content_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/HumanFeed.xaml", UriKind.Relative));
        }


        internal void SetAuthor(int id, string username)
        {
            Author.Text = username;
            Author.Tag = id;
        }
        internal void UpdateComments(List<PortableBLL.Comment> comments)
        {
            sf.Comments = comments;
          //TODO: ADD COMMENTS TO SENSOR CONTROL  SetNumberOfComments(comments.Count);
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
