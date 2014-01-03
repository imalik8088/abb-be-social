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
using System.Windows.Media.Imaging;
using System.IO;

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

            lstbContent.Items.Clear();

            //set the content of the feed
            SetLabels();
            AddTags();
            AddComments(hf.Comments);
            AddTime();
            SetImage();
            
     
        }

        private void SetImage()
        {
            if (!String.IsNullOrEmpty(hf.MediaFilePath))
            {

                BitmapImage bmp = new BitmapImage();

                string[] type = hf.MediaFilePath.Split(',');
                Byte[] imageBytes = Convert.FromBase64String(type[1]);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);

                if (type[0].Contains("gif"))
                {
                    return;
                }
                bmp.SetSource(ms);

                Image i = new Image();
                i.Height = 530;
                i.Width = 430;
                i.Source = bmp;

                lstbContent.Items.Add(i);
            }
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
            TextBlock t = new TextBlock();


            t.Text = hf.Content;

            lstbContent.Items.Add(t);

            Author.Text = hf.Owner.FirstName + " " + hf.Owner.LastName;

            //Image.Source = hf.MediaFilePath;
            Location.Text = hf.Location;
           // lblContent.Text = hf.Content;
        }

        /// <summary>
        /// Sets the time that has passed since the feed was published
        /// </summary>
        private void AddTime()
        {
            DateTime dateTime = hf.TimeStamp;
            DateTime now = DateTime.Now;

            //get the hours that has passed since the feed was posted
            double hours = (now - dateTime).TotalHours;

            //check if it should be displayed as min, hours or days
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
            if (String.IsNullOrEmpty(txtbComment.Text) || String.IsNullOrWhiteSpace(txtbComment.Text)) //no content added
            {
                MessageBox.Show("No comment attached");
                return;
            }
            //disable button
            btnPublish.IsEnabled = false;

            FeedManager fm = new FeedManager();
            Comment c = new Comment();

            //set owner
            c.Owner = App.CurrentUser;
            //set content
            c.Content = txtbComment.Text;
            bool result;

            try
            {
                //push the Comment to the DB
                 result = await fm.PublishComment(hf.ID, c);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong when publishing the comment, try again later!");
                btnPublish.IsEnabled = true;
                return;
            }
           

            if (!result)
                MessageBox.Show("Something went wrong when publishing the comment, try again later!");
            else
            {
                try
                {
                    //load the comments to display the newly added ones
                    List<Comment> comments = await fm.LoadFeedComments(hf.ID);

                    //insert it on the top
                    lstbComments.Items.Insert(3, new CommentControl(comments[0]));
                    lstbComments.UpdateLayout();

                    txtbComment.Text = String.Empty;
                    MessageBox.Show("Comment published");
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong when loading the comments, try again later!");
                }

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
                //redirect to profile page
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?userID=" + hf.Tags[lstbTags.SelectedIndex].ID, UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}