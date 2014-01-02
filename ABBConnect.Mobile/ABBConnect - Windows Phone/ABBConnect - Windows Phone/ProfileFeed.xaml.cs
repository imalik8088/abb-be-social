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
using System.Windows.Media.Imaging;

/*
 * Written by: Robert Gustavsson
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */


namespace ABBConnect___Windows_Phone
{
    /// <summary>
    /// A page the enables showing contact information of a user, his feeds and also the activity connected to him.
    /// </summary>
    public partial class ProfileFeed : PhoneApplicationPage
    {
        User currentUser;
        const int NUMOFFEEDS = 10;
        FeedManager fm;
        List<Feed> feeds;

        #region Activity page


        /// <summary>
        /// Gets the activies that is bound to the user
        /// </summary>
        private async void GetUserActivites()
        {
            try
            {
                UserManager um = new UserManager();

                List<Activity> activities = await um.GetUserActivity(currentUser.ID);

                foreach (Activity a in activities)
                    lstbActivities.Items.Add(new ActivityControl(a));
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't load user activities");
            }

        }


        #endregion

        #region Profile page

        /// <summary>
        /// Loads the userinfromation from the DB and add it to the screen
        /// </summary>
        private async void AddUserInformation()
        {
            UserManager um = new UserManager();
            string strUserId = NavigationContext.QueryString["userID"];

            int userId = int.Parse(strUserId);

            if (userId == App.CurrentUser.ID) //info already read from DB, since the user is logged in
                currentUser = App.CurrentUser;
            else
            {
                try
                {
                    currentUser = await um.LoadUserInformation(userId);
                }
                catch (Exception)
                {
                    MessageBox.Show("Couldn't load user information");
                    return;
                }
            }


            if (currentUser is Human)
                SetHumanInfo();
            else if (currentUser is Sensor)
                SetSensorInfo();
            else
            {
                MessageBox.Show("Invalid User ");
                return;
            }


            lblLocationClick.Text = currentUser.Location;
            pivHead.Title = "      " + lblNameClick.Text;

            LoadFeeds();
        }

        private void SetSensorInfo()
        {
            lblNameClick.Text = currentUser.UserName + " Unit (" + ((Sensor)currentUser).UnitMetric + ")";
            lblEmail.Text = "Lower Boundery";
            lblEmailClick.Text = ((Sensor)currentUser).LowerBoundary.ToString();
            imgMail.Source = new BitmapImage(new Uri("/Icons/icon-sensor.png", UriKind.Relative)); //TODO: add image for upper and lower boundery

            lblPhone.Text = "UpperBoundery";
            lblPhoneClick.Text = ((Sensor)currentUser).UpperBoundary.ToString();
            imgPhone.Source = new BitmapImage(new Uri("/Icons/icon-sensor.png", UriKind.Relative)); //TODO: add image for upper and lower boundery


            pivHead.Items.Remove(pivotActivity);
            pivotActivity.Visibility = Visibility.Collapsed;

            imgUser.Visibility = Visibility.Visible;    // show the user icon
            imgUser.Source = new BitmapImage(new Uri("/Icons/icon-sensor.png", UriKind.Relative));

            //disable clicking
            lblPhoneClick.MouseLeftButtonDown -= lblPhoneClick_MouseLeftButtonUp;
            lblEmailClick.MouseLeftButtonDown -= lblEmailClick_MouseLeftButtonDown;
        }

        private void SetHumanInfo()
        {
            lblEmailClick.Text = ((Human)currentUser).Email;
            lblNameClick.Text = ((Human)currentUser).FirstName + " " + ((Human)currentUser).LastName;
            lblPhoneClick.Text = ((Human)currentUser).PhoneNumber;

            imgUser.Visibility = Visibility.Visible;    // show the user icon
        }

        /// <summary>
        /// If email is clicked a mail client is opened and the user can send a mail to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblEmailClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EmailComposeTask emailComposer = new EmailComposeTask();
            emailComposer.To = lblEmailClick.Text;
            emailComposer.Show();
        }

        /// <summary>
        /// If phonenumber is clicked the user calls the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPhoneClick_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PhoneCallTask phonecall = new PhoneCallTask();
            phonecall.PhoneNumber = lblPhoneClick.Text;
            phonecall.Show();
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblLocationClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //maybe redirect to a new page and show the feeds from that location
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblNameClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        #endregion

        #region Feed page

        /// <summary>
        /// Add a HumanFeed to the list of feeds 
        /// </summary>
        /// <param name="hf"></param>
        private void FillFeedList(PortableBLL.HumanFeed hf)
        {
            // NoImageFeedControl nfc = new NoImageFeedControl(t.Author, "rgn09003", t.Content, t.Tags.Count, t.Comments.Count, t.Location, t.Timestamp);
            NoImageFeedControl fc = new NoImageFeedControl(hf);

            lstbFeeds.Items.Add(fc);
        }

        /// <summary>
        /// Add a sensor feed to the list of feeds
        /// </summary>
        /// <param name="sf"></param>
        private void FillFeedList(PortableBLL.SensorFeed sf)
        {
            // NoImageFeedControl nfc = new NoImageFeedControl(t.Author, "rgn09003", t.Content, t.Tags.Count, t.Comments.Count, t.Location, t.Timestamp);
            SensorFeedControl fc = new SensorFeedControl(sf);

            lstbFeeds.Items.Add(fc);
        }

        /// <summary>
        /// load the feeds from the user
        /// </summary>
        private async void LoadFeeds()
        {
            try
            {
                pgbLoadFeed.Visibility = System.Windows.Visibility.Visible;
                feeds = await fm.LoadFeedsByUser(currentUser.ID, NUMOFFEEDS);

                if (feeds.Count > 0)
                {
                    AddFeedToList(feeds);
                    CreateButton(feeds[feeds.Count - 1].ID);
                }
                pgbLoadFeed.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot load feeds, please check your connection");
            }

        }

        /// <summary>
        /// add the loaded feeds to the feed-list
        /// </summary>
        /// <param name="feeds"></param>
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

        /// <summary>
        /// Load more feeds, this method is triggerd when user clicks "load-more" button.
        /// </summary>
        /// <param name="numOfFeeds"></param>
        /// <param name="id"></param>
        private async void LoadMoreFeedsFromId(int numOfFeeds, int id)
        {
            List<PortableBLL.Feed> newFeeds;
            try
            {
                newFeeds = await fm.LoadFeedsByUser(currentUser.ID, numOfFeeds, id);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot load more feeds, please check your connection");
                return;
            }

            //remove button
            lstbFeeds.Items.RemoveAt(lstbFeeds.Items.Count - 1);

            AddFeedToList(newFeeds);

            //add the new feeds to the common list
            feeds.AddRange(newFeeds);
            CreateButton(feeds[feeds.Count - 1].ID);

            pgbLoadFeed.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Creates a button in the end of the feed list, to enable the user to load more feeds
        /// </summary>
        /// <param name="id"></param>
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


        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public ProfileFeed()
        {
            InitializeComponent();
            currentUser = new Human();
            fm = new FeedManager();
        }

        /// <summary>
        /// When the user get redirected here, add feeds, userinfo and activity
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AddUserInformation();
            GetUserActivites();

        }



    }
}