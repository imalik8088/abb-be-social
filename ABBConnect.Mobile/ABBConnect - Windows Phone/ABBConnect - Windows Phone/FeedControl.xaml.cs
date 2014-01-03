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

/*
 * Written by: Robert Gustavsson
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */


namespace ABBConnect___Windows_Phone
{
    /// <summary>
    /// A Controls that hold all data needed for the Feeds, this also is they why to represent the data
    /// </summary>
    public partial class FeedControl : UserControl
    {
        private PortableBLL.HumanFeed hFeed;

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public FeedControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Constructor with params instead of oject
        /// </summary>
        /// <param name="author"></param>
        /// <param name="username"></param>
        /// <param name="content"></param>
        /// <param name="noOfTags"></param>
        /// <param name="noOfComments"></param>
        /// <param name="location"></param>
        /// <param name="timestamp"></param>
        /// <param name="filePath"></param>
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

        /// <summary>
        /// Constructor with object HumanFeed from the BLL as param, this is currently used
        /// </summary>
        /// <param name="hf"></param>
        public FeedControl(PortableBLL.HumanFeed hf)
        {
            InitializeComponent();
            SetAuthor(hf.Owner.ID, hf.Owner.FirstName + " " + hf.Owner.LastName);
            SetContent(hf.Content);
            SetNumberOfTags(hf.Tags.Count);
            SetNumberOfComments(hf.Comments.Count);
            SetLocation(hf.Location);
            SetTimeStamp(hf.TimeStamp);
            SetImage(hf.MediaFilePath);

            hFeed = hf;
        }

        #endregion

        /// <summary>
        /// update the number of comments regarding the feed
        /// </summary>
        /// <param name="comments"></param>
        internal void UpdateComments(List<PortableBLL.Comment> comments)
        {
            //set the new comments
            hFeed.Comments = comments;
            SetNumberOfComments(comments.Count);
        }

        /// <summary>
        /// Occours when used clicks the author of the feed and redirects him to the users profile page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Author_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //redirect to the profile page
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?username=" + Author.Tag, UriKind.Relative));

        }

        /// <summary>
        /// Occours when somewhere else then author name is clicked to redirect the user to the feeds page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Content_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //redirect to the feed page
            App.HFeed = hFeed;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/HumanFeed.xaml", UriKind.Relative));
        }

        #region Setters

        /// <summary>
        /// Not in used ATM
        /// </summary>
        /// <param name="filePath"></param>
        private void SetImage(string filePath)
        {

            try
            {
                string[] type = filePath.Split(',');
                Byte[] imageBytes = Convert.FromBase64String(type[1]);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                BitmapImage bmp = new BitmapImage();

                if (type[0].Contains("gif"))
                {
                    return;
                }
                bmp.SetSource(ms);
                imgImage.Source = bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

             
        }

        /// <summary>
        /// Sets the Author
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        internal void SetAuthor(int id, string username)
        {
            Author.Text = username;
            Author.Tag = id;
        }

        /// <summary>
        /// Sets the feed text
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
        }

        /// <summary>
        /// Sets the number of tags of the feed
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
        /// Set the time that has passed since the feed was posted
        /// </summary>
        /// <param name="dateTime"></param>
        internal void SetTimeStamp(DateTime dateTime)
        {
            DateTime now = DateTime.Now;

            //get the hours that has passed since the feed was posted
            double hours = (now - dateTime).TotalHours;

            //check if it should be represented as min, hours or days
            if (hours < 1)
                Timestamp.Text = Math.Round((now - dateTime).TotalMinutes).ToString() + "m";
            else if (hours > 24)
                Timestamp.Text = Math.Round((now - dateTime).TotalDays).ToString() + "d";
            else
                Timestamp.Text = Math.Round(hours).ToString() + "h";
        }

        /// <summary>
        /// set the location of the feed
        /// </summary>
        /// <param name="p"></param>
        internal void SetLocation(string p)
        {
            Location.Text = p;
        }

        #endregion


    }
}
