using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Text;
using System.IO;
using System.Windows.Media.Imaging;

namespace ABBConnect___Windows_Phone
{
    public partial class FeedControl : UserControl
    {
        private BLL.HumanFeed hFeed;


        public FeedControl()
        {
            InitializeComponent();
        }
        public FeedControl(int author, string username, string content, int noOfTags, int noOfComments, string location, DateTime timestamp, string filePath)
        {
            InitializeComponent();
            SetAuthor(author, username);
            SetContent(content);
            SetNumberOfTags(noOfTags);
            SetNumberOfComments(noOfComments);
            SetLocation(location);
            SetTimeStamp(timestamp);
            SetImage(filePath);
        }

        public FeedControl(BLL.HumanFeed hf)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            SetAuthor(hf.Owner.ID, hf.Owner.UserName);
            SetContent(hf.Content);
            SetNumberOfTags(hf.Tags.Count);
            SetNumberOfComments(hf.Comments.Count);
            SetLocation(hf.Location);
            SetTimeStamp(hf.TimeStamp);
            SetImage(hf.MediaFilePath);

            hFeed = hf;
        }

        private void SetImage(string filePath)
        {
            Byte[] imageBytes = Convert.FromBase64String(filePath);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            BitmapImage bmp = new BitmapImage();
            bmp.SetSource(ms);
            imgImage.Source = bmp;
        }

        private void Author_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?username=" + Author.Tag, UriKind.Relative));

        }

        private void Content_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Feed.xaml", UriKind.Relative));
        }

        internal void SetAuthor(int id, string username)
        {
            Author.Text = username;
            Author.Tag = id;
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
