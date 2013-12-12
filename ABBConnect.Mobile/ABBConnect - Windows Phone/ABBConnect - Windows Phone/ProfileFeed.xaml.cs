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
using PortableBLL;

namespace ABBConnect___Windows_Phone
{
    public partial class ProfileFeed : PhoneApplicationPage
    {
        User currentUser;
        const int NUMOFFEEDS = 10;
        FeedManager fm;
        List<Feed> feeds;

        public ProfileFeed()
        {
            InitializeComponent();
            currentUser = new Human();
            fm = new FeedManager();
        }

        private void FillFeedList(PortableBLL.HumanFeed hf)
        {
           // NoImageFeedControl nfc = new NoImageFeedControl(t.Author, "rgn09003", t.Content, t.Tags.Count, t.Comments.Count, t.Location, t.Timestamp);
            NoImageFeedControl fc = new NoImageFeedControl(hf);

            lstbFeeds.Items.Add(fc);
        }

        private void FillFeedList(PortableBLL.SensorFeed sf)
        {
            // NoImageFeedControl nfc = new NoImageFeedControl(t.Author, "rgn09003", t.Content, t.Tags.Count, t.Comments.Count, t.Location, t.Timestamp);
            SensorFeedControl fc = new SensorFeedControl(sf);

            lstbFeeds.Items.Add(fc);
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AddUserInformation();
        }

        private async void LoadFeeds()
        {
            pgbLoadFeed.Visibility = System.Windows.Visibility.Visible;
            feeds = await fm.LoadFeedsByUser(currentUser.ID, NUMOFFEEDS);

            if(feeds.Count > 0)
            {
                AddFeedToList(feeds);
                CreateButton(feeds[feeds.Count - 1].ID);
            }
            pgbLoadFeed.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void AddFeedToList(List<Feed> feeds)
        {
            foreach (Feed f in feeds)
            {
                if (f is PortableBLL.HumanFeed)
                    FillFeedList((PortableBLL.HumanFeed)f);
                else
                    FillFeedList((PortableBLL.SensorFeed)f);
            }
        }

        private async void LoadMoreFeedsFromId(int numOfFeeds, int id)
        {
            

            List<PortableBLL.Feed> newFeeds = await fm.LoadFeedsByUser(currentUser.ID, numOfFeeds, id);

            //remove button
            lstbFeeds.Items.RemoveAt(lstbFeeds.Items.Count - 1);

            AddFeedToList(newFeeds);

            //add the new feeds to the common list
            feeds.AddRange(newFeeds);
            CreateButton(feeds[feeds.Count - 1].ID);

            pgbLoadFeed.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void CreateButton(int id)
        {
            Button b = new Button();
            b.Name = "btnLoadNewFeeds";
            b.Width = 456;
            b.Height = 80;
            b.Content = "Load more feeds";

            b.Click += (s, e) => { LoadMoreFeedsFromId(NUMOFFEEDS, id); };
            lstbFeeds.Items.Add(b);
        }
        private async void AddUserInformation()
        {
            UserManager um = new UserManager();
            string strUserId = NavigationContext.QueryString["userID"];

            int userId = int.Parse(strUserId);

            if (userId == App.CurrentUser.ID) //info already read from DB, since the user is logged in
                currentUser = App.CurrentUser;
            else
                currentUser = await um.LoadUserInformation(userId);


            if (currentUser is Human)
            {
                lblEmailClick.Text = ((Human)currentUser).Email;
                lblNameClick.Text = ((Human)currentUser).FirstName + " " + ((Human)currentUser).LastName;
                lblPhoneClick.Text = ((Human)currentUser).PhoneNumber;

                imgSensor.Visibility = Visibility.Collapsed;  // hide the sensor icon
                imgUser.Visibility = Visibility.Visible;    // show the user icon

            }
            else if (currentUser is Sensor)
            {
                lblNameClick.Text = currentUser.UserName + " Unit ( " + ((Sensor)currentUser).UnitMetric + " )";
                lblEmail.Text = "Lower Boundery";
                lblEmailClick.Text = ((Sensor)currentUser).LowerBoundary.ToString();

                lblPhone.Text = "UpperBoundery";
                lblPhoneClick.Text = ((Sensor)currentUser).UpperBoundary.ToString();

                // throws a error in runtime.....
                //imgSensor.Visibility = Visibility.Visible; // show the sensor icon
                //imgUser.Visibility = Visibility.Collapsed;  // hide the user icon

                //disable clicking
                lblPhoneClick.MouseLeftButtonDown -= lblPhoneClick_MouseLeftButtonUp;
                lblEmailClick.MouseLeftButtonDown -= lblEmailClick_MouseLeftButtonDown;
            }
            else
            {
                MessageBox.Show("Invalid User ");
                return;
            }


            lblLocationClick.Text = currentUser.Location;

            pivHead.Title = "      " + lblNameClick.Text;

            LoadFeeds();
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