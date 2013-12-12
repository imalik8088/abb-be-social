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

namespace ABBConnect___Windows_Phone
{
    public partial class HumanFeed : PhoneApplicationPage
    {

        PortableBLL.HumanFeed hf;
      
        public HumanFeed()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            hf = App.HFeed;

            SetLabels();
            AddTags();
            AddComments(hf.Comments);
            AddTime();
     
        }

        private void AddTags()
        {
            lstbTags.ItemsSource = hf.Tags;
        }

        private void SetLabels()
        {
            Author.Text = hf.Owner.FirstName + " " + hf.Owner.LastName;

            //Image.Source = hf.MediaFilePath;
            Location.Text = hf.Location;
            Content.Text = hf.Content;
        }

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

        private void AddComments(List<Comment> comments)
        {

            //add the comments to the listbox
            foreach (PortableBLL.Comment c in comments)
            {
                CommentControl cc = new CommentControl(c.Owner.FirstName + " " + c.Owner.LastName, c.TimeStamp, c.ID, c.Content);
                lstbComments.Items.Add(cc);
            }
        }

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

        private void txtbComment_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtbComment.Text = String.Empty;
        }

        private void lstbTags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?userID=" + hf.Tags[lstbTags.SelectedIndex].ID, UriKind.Relative));
        }
    }
}