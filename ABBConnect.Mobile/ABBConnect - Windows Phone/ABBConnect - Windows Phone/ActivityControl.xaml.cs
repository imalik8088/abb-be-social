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

namespace ABBConnect___Windows_Phone
{
    public partial class ActivityControl : UserControl
    {
        Activity activitiy;
        public ActivityControl(Activity a)
        {
            InitializeComponent();
            activitiy = a;

            lblText.Text = activitiy.Text;
            SetImage(activitiy.Type);
            SetTime(activitiy.Timestamp);
        }

        private void SetImage(string type)
        {
            if (type == "Comment")
                imgType.Source = new BitmapImage(new Uri("/Icons/symbol_comment.png", UriKind.Relative));
            else if (type == "Tag")
                imgType.Source = new BitmapImage(new Uri("/Icons/symbol_tag.png", UriKind.Relative));
            else
                imgType.Source = new BitmapImage(new Uri("/Icons/symbol_location.png", UriKind.Relative));
        }

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

        private async void LayoutRoot_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FeedManager fm = new FeedManager();
            Feed f =  await fm.GetFeedByFeedId(activitiy.FeedId);

            App.HFeed = f as PortableBLL.HumanFeed;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/HumanFeed.xaml", UriKind.Relative));
        }

    }
}
