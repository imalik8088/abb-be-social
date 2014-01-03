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
using Microsoft.Devices;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using PortableBLL;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;
using System.Text;
using System.Windows.Threading;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Shell;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

/*
 * Written by: Robert Gustavsson
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */


namespace ABBConnect___Windows_Phone
{
    /// <summary>
    /// This class is the main page that handles the most of the loading of feeds and filter handling, also the application bar is visible inside of this page
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        #region Field
        PortableBLL.Human currentUser;
        PortableBLL.FeedManager fm;

        DispatcherTimer timerNewFeed, timerLabel;
        List<PortableBLL.Feed> feeds;
        List<Filter> filters;
        FeedType.FeedSource currentFeedType;

        bool ini, timerReady;
        int NoCache;
        const int UPDATETIME = 30, SHOWLABELTIME = 4, NUMBEROFFEEDS = 15;
        string chosenImg;

        #endregion

        #region General

        /// <summary>
        /// Constructot
        /// </summary>
        public MainPage()
        {

            InitializeComponent();
            ini = false;

            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            //ini object
            fm = new FeedManager();
            feeds = new List<Feed>();
            filters = new List<Filter>();

            InitializeTimers();

            //set current feed type to retreive feeds from both sensors and humans
            currentFeedType = new FeedType.FeedSource();
            currentFeedType = FeedType.FeedSource.None;
            
            LoadNewFeeds(NUMBEROFFEEDS);

            lblNewFeeds.Text = "";
            NoCache = 1;


            //add application bar
            this.ApplicationBar = this.Resources["appBar"] as ApplicationBar;

            //Initilaztation is ready
            ini = true;
        }

        /// <summary>
        /// Initilizes the timers
        /// </summary>
        private void InitializeTimers()
        {
            timerNewFeed = new DispatcherTimer { Interval = new TimeSpan(0, 0, UPDATETIME) };
            timerNewFeed.Tick += new EventHandler(timerNewFeed_tick);
            timerNewFeed.Start();

            timerLabel = new DispatcherTimer { Interval = new TimeSpan(0, 0, SHOWLABELTIME) };
            timerLabel.Tick += new EventHandler(timerLabel_tick);
            timerReady = true;
        }

        /// <summary>
        /// An event that is triggered when a user get redirected to the main page (right now only on login)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //get the username from the query
            string userName = NavigationContext.QueryString["userName"];
            LoadUser(userName);

            //remove so the user can not go back to log in page by clicking "back"
            NavigationService.RemoveBackEntry();

        }

        /// <summary>
        /// When the main page is loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }

            //reset timer
            timerNewFeed.Stop();
            timerNewFeed.Start();

        }

        #endregion

        #region Feed page

        /// <summary>
        /// Hides the label showing that new feeds has been added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerLabel_tick(object sender, EventArgs e)
        {
            lblNewFeeds.Text = "";
            timerLabel.Stop();
        }

        /// <summary>
        /// Ticks every UPDATETIME second and adds all new added feeds to the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void timerNewFeed_tick(object sender, EventArgs e)
        {
            if (timerReady)
            {
                try
                {
                    timerReady = false;

                    //update the comments for all feeds
                    await UpdateComments();
                    
                    CheckNewFeeds();

                    timerReady = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Couldn't update the comments, please check your connection");
                }

            }
        }

        /// <summary>
        /// Updates the feeds, both comments and time since they were posted
        /// </summary>
        /// <returns></returns>
        private async Task UpdateComments()
        {
            FeedManager fmm = new FeedManager();

            for (int i = 0; i < feeds.Count; i++) //for all the feeds in the feedlist
            {
                feeds[i].Comments = await fmm.LoadFeedComments(feeds[i].ID); //load the comments for that feed

                if (lstbFeeds.Items[i] is FeedControl)
                    (lstbFeeds.Items[i] as FeedControl).UpdateComments(feeds[i].Comments);
                else if (lstbFeeds.Items[i] is NoImageFeedControl)
                    (lstbFeeds.Items[i] as NoImageFeedControl).UpdateFeed(feeds[i].Comments);
                else
                    continue; //TODO: ADD THE SAME FOR SENSOR
            }
        }

        /// <summary>
        /// A method that check wheter new feeds is available in the DB to read to the system. 
        /// If there are feeds available, they are loaded into the application
        /// </summary>
        private async void CheckNewFeeds()
        {
            try
            {
                //if no feeds has been added yet
                if (feeds.Count == 0)
                    return;

                FeedManager fml = new FeedManager();


                //load the newest feed
                List<PortableBLL.Feed> latestFeed = await fml.LoadFeedsByType(currentFeedType, NoCache);

                //get the latest ID and the amount between the last newest ID and this
                int id = latestFeed[0].ID;
                int amount = id - this.feeds[0].ID;

                if (amount > 0) //add new feeds if the id differs
                {
                    lblNewFeeds.Text = "New feeds has been loaded (+" + amount + ")";

                    //Start timer to just show the label for 10 secs
                    timerLabel.Start();

                    //call all feeeds between them and add them first in the list
                    LoadNewFeedsFromId(amount, id);
                }

                NoCache++;

                if (NoCache > 5)
                    NoCache = 1;
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't load new feeds, please check you connection");
            }

        }

        /// <summary>
        /// Load the logged in user to display his personal info
        /// </summary>
        private async void LoadUser(string userName)
        {
            try
            {
                PortableBLL.UserManager um = new PortableBLL.UserManager();
                //get the user info
                currentUser = await um.LoadHumanInformationByUsername(userName);

                //set the current user
                App.CurrentUser = currentUser;

                GetSavedFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Load the newest feeds, amount is the number of feeds to load
        /// </summary>
        /// <param name="amount"></param>
        private async void LoadNewFeeds(int amount)
        {
            pgbLoadFeed.Visibility = System.Windows.Visibility.Visible;

            try
            {
                //get the feeds
                feeds = await fm.LoadFeedsByType(currentFeedType, amount);

                if (feeds.Count == 0) //if no feeds was fetched
                {
                    MessageBox.Show("No feeds loaded");
                    return;
                }
                //clear feed-page
                lstbFeeds.Items.Clear();

                //add the new feeds with a button in the end
                AddFeedsToList(feeds);
                CreateButton(feeds[feeds.Count - 1].ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            pgbLoadFeed.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Load more feeds, amount is number of feeds and id is the feed id to start loading from
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="id"></param>
        private async void LoadMoreFeedsFromId(int amount, int id)
        {
            try
            {
                pgbLoadFeed.Visibility = System.Windows.Visibility.Visible;

                //get new feeds
                List<PortableBLL.Feed> newFeeds = await fm.LoadFeedsByType(currentFeedType, amount, id);


                //remove button
                lstbFeeds.Items.RemoveAt(lstbFeeds.Items.Count - 1);

                AddFeedsToList(newFeeds);

                //add the new feeds to the common list and a button in the end
                feeds.AddRange(newFeeds);
                CreateButton(feeds[feeds.Count - 1].ID);

                pgbLoadFeed.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't load more feeds, please check you connection");
            }

        }

        /// <summary>
        /// Same as LoadMoreFeeds but this method adds the feed on top insead of in the end.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="id"></param>
        private async void LoadNewFeedsFromId(int amount, int id)
        {
            try
            {
                pgbLoadFeed.Visibility = System.Windows.Visibility.Visible;

                //load up new feeds
                List<PortableBLL.Feed> newFeeds = await fm.LoadFeedsByType(currentFeedType, amount, id + 1);

                for (int i = 0; i < newFeeds.Count; i++) //add them to the current list
                    feeds.Insert(i, newFeeds[i]);

                //insert the feeds on top
                InsertFeedsToList(newFeeds);

                pgbLoadFeed.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't load new feeds, please check your connection");
            }

        }

        /// <summary>
        /// Add all the feeds to the list to be shown (Adds in the end)
        /// </summary>
        /// <param name="feeds"></param>
        private void AddFeedsToList(List<PortableBLL.Feed> feeds)
        {
            for (int i = 0; i < feeds.Count; i++)
            {
                if (feeds[i] is PortableBLL.HumanFeed)
                    FillFeedList((PortableBLL.HumanFeed)feeds[i], -1); //-1 so they are inserted last
                else
                    FillFeedList((PortableBLL.SensorFeed)feeds[i], -1);
            }
        }

        /// <summary>
        /// Inserts feeds in the beginning of the list that is shown
        /// </summary>
        /// <param name="feeds"></param>
        private void InsertFeedsToList(List<PortableBLL.Feed> feeds)
        {
            for (int i = 0; i < feeds.Count; i++)
            {
                if (feeds[i] is PortableBLL.HumanFeed)
                    FillFeedList((PortableBLL.HumanFeed)feeds[i], i);
                else
                    FillFeedList((PortableBLL.SensorFeed)feeds[i], i);
            }
        }

        /// <summary>
        /// Creates a button and adds it in the nd of the list, so the user can load more feeds.
        /// </summary>
        /// <param name="id"></param>
        private void CreateButton(int id)
        {
            //create a new button
            Button b = new Button();
            b.Name = "btnLoadNewFeeds";
            b.Width = 456;
            b.Height = 80;
            b.Content = "Load more feeds";

            //add an event to the button
            b.Click += (s, e) => { LoadMoreFeedsFromId(NUMBEROFFEEDS, id); };

            //add the button the to feed page
            lstbFeeds.Items.Add(b);
        }

        /// <summary>
        /// Fill the list with human feeds, send -1 as index if it should be added in the end
        /// </summary>
        /// <param name="hf"></param>
        /// <param name="index"></param>
        private void FillFeedList(PortableBLL.HumanFeed hf, int index)
        {
            //CHECK IF THE FEED CONTATINS A PICTURE!!

            if (hf.MediaFilePath == "none" || hf.MediaFilePath == "") //if no image is present
            {
                //create a feed with no image
                NoImageFeedControl nfc = new NoImageFeedControl(hf);

                //add it to the feed page
                if (index == -1)
                    lstbFeeds.Items.Add(nfc);
                else
                    lstbFeeds.Items.Insert(index, nfc);
            }
            else //if feed has image
            {
                FeedControl fc = new FeedControl(hf);

                //add it to the feed page
                if (index == -1)
                    lstbFeeds.Items.Add(fc);
                else
                    lstbFeeds.Items.Insert(index, fc);
            }


        }

        /// <summary>
        /// Fill the list with sensor feeds, send -1 as index if it should be added in the end
        /// </summary>
        /// <param name="t"></param>
        /// <param name="index"></param>
        private void FillFeedList(SensorFeed t, int index)
        {
            //add a sensor control to the feedpage
            SensorFeedControl sfc = new SensorFeedControl(t.Owner.ID, t.Owner.UserName, t.Content, t.Location, t.TimeStamp);
            lstbFeeds.Items.Add(sfc);
        }

        #endregion

        #region Filter page

        /// <summary>
        /// Reads the saved filters that the logged in user has saved
        /// </summary>
        private async void GetSavedFilters()
        {
            try
            {
                UserManager um = new UserManager();
                this.filters = await um.GetUserSavedFilters(currentUser.ID);

                //add the filters to the filter page
                lstbSavedFilters.ItemsSource = filters;
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't load the saved filters, please restart the application to access them");
            }

        }

        /// <summary>
        /// An event that hanldes when the user is clicking the Human-square in the filter Pivot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void brdrHuman_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //unticking human         
            if (brdrHuman.Tag.Equals("true"))
            {
                if (brdrSensor.Tag.Equals("true"))
                {
                    ChangeFeedType(FeedType.FeedSource.Sensor);
                    brdrHuman.Tag = "false";
                    brdrHuman.Background = GetColorFromHexa("#FF515B5B");
                }
                else
                {
                    ChangeFeedType(FeedType.FeedSource.None);
                    brdrHuman.Tag = "false";
                    brdrHuman.Background = GetColorFromHexa("#FF515B5B");
                }
            }
            else //ticking human
            {
                if (brdrSensor.Tag.Equals("true")) //if sensor is ticked
                {
                    ChangeFeedType(FeedType.FeedSource.None);
                    brdrHuman.Background = GetColorFromHexa("#FFB5BBBB");
                    brdrHuman.Tag = "true";
                }
                else //if not sensor is ticked
                {
                    ChangeFeedType(FeedType.FeedSource.Human);
                    brdrHuman.Background = GetColorFromHexa("#FFB5BBBB");
                    brdrHuman.Tag = "true";
                }
            }
            
        }

        /// <summary>
        /// An event that hanldes when the user is clicking the Sensor-square in the filter Pivot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void brdrSensor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //unticking senosr         
            if (brdrSensor.Tag.Equals("true"))
            {
                if (brdrHuman.Tag.Equals("true"))
                {
                    ChangeFeedType(FeedType.FeedSource.Human);
                    brdrSensor.Tag = "false";
                    brdrSensor.Background = GetColorFromHexa("#FF515B5B");
                }
                else
                {
                    ChangeFeedType(FeedType.FeedSource.None);
                    brdrSensor.Tag = "false";
                    brdrSensor.Background = GetColorFromHexa("#FF515B5B");
                }
            }
            else //ticking sensor
            {
                if (brdrHuman.Tag.Equals("true")) //if human is ticked
                {
                    ChangeFeedType(FeedType.FeedSource.None);
                    brdrSensor.Background = GetColorFromHexa("#FFB5BBBB");
                    brdrSensor.Tag = "true";
                }
                else //if not human is ticked
                {
                    ChangeFeedType(FeedType.FeedSource.Sensor);
                    brdrSensor.Background = GetColorFromHexa("#FFB5BBBB");
                    brdrSensor.Tag = "true";
                }
            }
        }

        /// <summary>
        /// This method is setting the type of feeds that should be loaded in to the system (human, sensor or both)
        /// </summary>
        /// <param name="feedType"></param>
        private void ChangeFeedType(FeedType.FeedSource feedType)
        {
            if (!ini)
                return;

            //reset timer
            timerNewFeed.Stop();
            timerNewFeed.Start();

            try
            {
                //Set the new feed type
                currentFeedType = feedType;

                //load the new feeds dependent on the new feed type
                lstbFeeds.Items.Clear();
                feeds.Clear();
                LoadNewFeeds(NUMBEROFFEEDS);
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// Converst a hex color code into a brush
        /// </summary>
        /// <param name="hexaColor"></param>
        /// <returns></returns>
        private SolidColorBrush GetColorFromHexa(string hexaColor)
        {
            //convert hexa colors to a brush
            return new SolidColorBrush(
                Color.FromArgb(
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16),
                    Convert.ToByte(hexaColor.Substring(7, 2), 16)
                )
            );
        }

        /// <summary>
        /// This event is handling when the user is clicking any of his saved filters that are available in the filter pivot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lstbSavedFilters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstbSavedFilters.SelectedIndex == -1) //invalid selection handled
                return;

            try
            {
                pgbLoadFeed.Visibility = System.Windows.Visibility.Visible;

                timerNewFeed.Stop();

                //Deselect the human / filter
                brdrHuman.Background = GetColorFromHexa("#FF51BBBB");
                brdrSensor.Background = GetColorFromHexa("#FF51BBBB");

                //load the feeds from the saved filter
                feeds = await fm.LoadFeedsFromSavedFilter(filters[lstbSavedFilters.SelectedIndex], 10);

                //reset the feed page and add the new feeds
                lstbFeeds.Items.Clear();
                AddFeedsToList(feeds);


                lstbSavedFilters.SelectedIndex = -1; //reset the selection to be able to click the same filtering again

                pgbLoadFeed.Visibility = System.Windows.Visibility.Collapsed;

            }
            catch (Exception)
            {
                MessageBox.Show("Cannot load the feeds from the saved filters, please check your connection!");
            }

        }

        /// <summary>
        /// Loads all the feeds from the previous shift
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pgbLoadFeed.Visibility = System.Windows.Visibility.Visible;

                //get the feeds from last shofr
                feeds = await fm.LoadFeedsFromLastShift(NUMBEROFFEEDS);
   
                if (feeds.Count > 0) //if there were feeds
                {
                    //reset the feed page and add the new feeds
                    lstbFeeds.Items.Clear();
                    AddFeedsToList(feeds);
                    CreateButton(feeds[feeds.Count - 1].ID);
                }
                else
                    MessageBox.Show("No feeds was published in the previous shift");

                pgbLoadFeed.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Cannot load the feeds from the previous shift, please check your connection!");

            }



        } 

        #endregion

        #region Post page

        /// <summary>
        /// This is a event that occurs when the user wants to tag users to a feed and its redirect the user to the tagging page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //redirect to the tagging page
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/TagControl.xaml", UriKind.Relative));

        }

        /// <summary>
        /// This event occurs when the user clicks the textbox for inserting feed text, the method deletes the predefined text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbContent_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtbContent.Text = String.Empty;
        }

        /// <summary>
        /// Starts the camera application of the phone when user clicks the image 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgCapture_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //Start the build-in camera application and show it
                CameraCaptureTask cameraCapture = new CameraCaptureTask();
                cameraCapture.Completed += CameraCapture_Completed;
                cameraCapture.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured");
            }

        }

        /// <summary>
        /// Publih the feed to the DB, occours when user is clicking publish button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnPublish_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            PortableBLL.FeedManager fm = new FeedManager();
            PortableBLL.HumanFeed hf = new PortableBLL.HumanFeed();

            if (txtbContent.Text == "" || currentUser.ID == -1)
                return;

            if (App.Tags != null) //if there are tags, add them to the list
                foreach (string s in App.Tags)
                    hf.Tags.Add(new Human() { UserName = s });

            chosenImg = ""; //REMOVE WHÈN SENDING IMG SHALL BE ENABLED!!!

            //Set the content of the feed
            hf.Owner.ID = currentUser.ID;
            hf.Content = txtbContent.Text;
            hf.MediaFilePath = (String.IsNullOrEmpty(chosenImg) ? "none" : chosenImg);
            hf.Category.Id = 2;

            try
            {
                //plublish it
                bool res = await fm.PublishFeed(hf);

                if (res)
                {
                    txtbContent.Text = "";
                    MessageBox.Show("Feed Published");
                }
                else
                    MessageBox.Show("Something went wrong");
            }
            catch (Exception)
            {

                MessageBox.Show("Couldn't publish the new feed, please check your connection");

            }

        }

        /// <summary>
        /// When the user has finished taken a photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CameraCapture_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                lbl_TapToTakePhoto.Text = "";

                //Code to display the photo on the page in an image control named myImage.
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                imgCapture.Source = bmp;

                //Convert the photo to bytes
                Byte[] photoBytes = new byte[e.ChosenPhoto.Length];

                // rewind first
                e.ChosenPhoto.Position = 0;

                // now succeeds
                e.ChosenPhoto.Read(photoBytes, 0, photoBytes.Length);


                //UNCOMMENT TO ENABLE SENDING IMAGE TO DB

                // chosenImg = Convert.ToBase64String(photoBytes);

                // MessageBox.Show(chosenImg.Length.ToString());
                //chosenImg = "";

                //TESTING
                /*
                BLL.HumanFeed hf = new BLL.HumanFeed();
                hf.MediaFilePath = chosenImg;
                hf.ID = 190;
                hf.Location = "hej";
                hf.TimeStamp = new DateTime();
                hf.Owner.ID = 1;
                hf.Owner.UserName = "rgn09003";
                hf.Comments = new List<Comment>();
                hf.Tags = new List<Human>();

                FeedControl fc = new FeedControl(hf);


                IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
                appSettings.Add("img", chosenImg);

                lstbFeeds.Items.Add(fc);
                   

                int hej = 10;
                 */
            }
        }

        #endregion

        #region Applicationbar

        /// <summary>
        /// This event occers when the user is clicking the applicationbar "my profile" button and is redirecting the user to his profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToMyProfile(object sender, EventArgs e)
        {
            if (currentUser == null)
                MessageBox.Show("Unable to redirect since no user is detected, please check your internet connection");
            else
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?userID=" + currentUser.ID, UriKind.Relative));
        }

        /// <summary>
        /// This event occers when the user is clicking the applicationbar "log out" button and is logging the user out of the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLogOut(object sender, EventArgs e)
        {
            MessageBox.Show("You will log out if you click here");
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Clear();
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/LogIn.xaml", UriKind.Relative));

        }

        /// <summary>
        /// This event occers when the user is clicking the applicationbar "search user" button and is redirecting the user to the searching page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSearchUser(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/SearchUser.xaml", UriKind.Relative));
        }

        /// <summary>
        /// This event occers when the user is clicking the applicationbar "refresh" button and is forcing the system to check for new feeds and update the previous feeds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRefresh(object sender, EventArgs e)
        {
            if (!ini)
                return;

            pgbLoadFeed.Visibility = System.Windows.Visibility.Visible;
            timerNewFeed.Stop();
            CheckNewFeeds();
            timerNewFeed.Start();
            pgbLoadFeed.Visibility = System.Windows.Visibility.Collapsed;

        }

        #endregion






        //NOT USED I THINK? NEED TO CHECK IT WHEN I HAVE TIME!!
        /// <summary>
        /// NOT USED?!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClickalbe_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml", UriKind.Relative));
        }


    }
}