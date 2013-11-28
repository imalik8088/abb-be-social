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
using Microsoft.Phone.Tasks;
using System.Windows.Navigation;

namespace ABBConnect___Windows_Phone
{
    public partial class ProfileFeed : PhoneApplicationPage
    {
        public ProfileFeed()
        {
            InitializeComponent();
        }

        private void FillFeedList(Test t)
        {
           // NoImageFeedControl nfc = new NoImageFeedControl(t.Author, "rgn09003", t.Content, t.Tags.Count, t.Comments.Count, t.Location, t.Timestamp);
            FeedControl fc = new FeedControl(t.Author, "rgn09003", t.Content, t.Tags.Count, t.Comments.Count, t.Location, t.Timestamp, "");

            lstbFeeds.Items.Add(fc);

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string name = NavigationContext.QueryString["username"];

            for (int i = 0; i < 20; i++)
            {

                Test t = new Test();
                t.ID1 = i;
                t.Content = "This is a very beautiful content post! I love Robert, he is awesome and I really hope this work now!!";
                t.Location = "Control Room 1";
                t.Priority = 4;
                t.Timestamp = DateTime.Now;
                t.Category = "Sticky Note";
                t.Author = "Robert Gustavsson";

                for (int j = 0; j < 4; j++)
                {
                    t.Comments.Add("hej hej " + i + j);
                    t.Tags.Add("Tag " + j);
                }

                FillFeedList(t);
            }
        }

        private void lblEmailClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EmailComposeTask emailComposer = new EmailComposeTask();
            emailComposer.To = lblEmailClick.Text;
            emailComposer.Show();
        }

        private void lblPhoneClick_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PhoneCallTask phonecall = new PhoneCallTask();
            phonecall.PhoneNumber = lblPhoneClick.Text;
            phonecall.Show();
        }

        private void lblLocationClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //maybe redirect to a new page and show the feeds from that location
        }

        private void lblNameClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/myPivotPage.xaml?id=0", UriKind.Relative));
        }
    }
}