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

namespace ABBConnect___Windows_Phone
{
    public partial class HumanFeed : PhoneApplicationPage
    {
        public HumanFeed()
        {
            InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BLL.HumanFeed hf = App.HFeed;

            Author.Text = hf.Owner.UserName;

            DateTime dateTime = hf.TimeStamp;
            DateTime now = DateTime.Now;

            double hours = (now - dateTime).TotalHours;

            if (hours < 1)
                Timestamp.Text = Math.Round((now - dateTime).TotalMinutes).ToString() + "m";
            else if (hours > 24)
                Timestamp.Text = Math.Round((now - dateTime).TotalDays).ToString() + "d";
            else
                Timestamp.Text = Math.Round(hours).ToString() + "h";

            //Image.Source = hf.MediaFilePath;
            Location.Text = hf.Location;
            Content.Text = hf.Content;

            lstbTags.ItemsSource = hf.Tags;
            lstbComments.ItemsSource = hf.Comments;



        }
    }
}