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

namespace ABBConnect___Windows_Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        PortableBLL.Human currentUser;
        PortableBLL.FeedManager fm;
        const int UPDATETIME = 15, SHOWLABELTIME = 4;
        const int NUMBEROFFEEDS = 10;
        DispatcherTimer timerNewFeed, timerLabel;
        List<PortableBLL.Feed> feeds;
        bool ini;

        FeedType.FeedSource currentFeedType;

        string chosenImg;

        /// <summary>
        /// Constructot
        /// </summary>
        public MainPage()
        {

            InitializeComponent();
            ini = false;
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            fm = new FeedManager();
            feeds = new List<Feed>();

            timerNewFeed = new DispatcherTimer { Interval = new TimeSpan(0,0,UPDATETIME) };
            timerNewFeed.Tick += new EventHandler(timerNewFeed_tick);
            timerNewFeed.Start();
            timerLabel = new DispatcherTimer { Interval = new TimeSpan(0, 0, SHOWLABELTIME) };
            timerNewFeed.Tick += new EventHandler(timerLabel_tick);

            LoadUser();
            LoadNewFeeds(NUMBEROFFEEDS);

            lblNewFeeds.Text = "";

            currentFeedType = new FeedType.FeedSource();
            currentFeedType = FeedType.FeedSource.Human;

            this.ApplicationBar = this.Resources["appBar"] as ApplicationBar;
           // CreateControlsUsingObjects();

            ini = true;
        }

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
            //if no feeds has been added yet
            if (feeds.Count == 0)
                return;

            //load the newest feed
            List<PortableBLL.Feed> latestFeed = await fm.LoadFeedsByType(currentFeedType, 1);
           // List<PortableBLL.Feed> latestFeed = await fm.LoadLatestXFeeds(1);

            //get the latest ID and the amount between the first ID and this
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
        }

        /// <summary>
        /// Load the logged in user to display his personal info
        /// </summary>
        private async void LoadUser()
        {
            PortableBLL.UserManager um = new PortableBLL.UserManager();
            currentUser = await um.LoadHumanInformationByUsername("rgn09003");

            App.CurrentUser = currentUser;
            /*
            lblEmailClick.Text = currentUser.Email;
            lblPhoneClick.Text = currentUser.PhoneNumber;
            lblNameClick.Text = currentUser.FirstName + " " + currentUser.LastName;
            lblLocationClick.Text = currentUser.WorkRoom;
             * */

        }

        /// <summary>
        /// Load the newest feeds, amount is the number of feeds to load
        /// </summary>
        /// <param name="amount"></param>
        private async void LoadNewFeeds(int amount)
        {
            pgbLoadFeed.Visibility = System.Windows.Visibility.Visible;

            feeds = await fm.LoadFeedsByType(currentFeedType, amount);
            //feeds = await fm.LoadLatestXFeeds(amount);
            AddFeedsToList(feeds);

            CreateButton(feeds[feeds.Count - 1].ID);

            pgbLoadFeed.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Load more feeds, amount is number of feeds and id is the feed id to start loading from
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="id"></param>
        private async void LoadMoreFeedsFromId(int amount, int id)
        {
            pgbLoadFeed.Visibility = System.Windows.Visibility.Visible;


            List<PortableBLL.Feed> newFeeds = await fm.LoadFeedsByType(currentFeedType, amount, id);

            //List<Feed> newFeeds = await fm.LoadLatestXFeedsFromId(id, amount);

            //remove button
            lstbFeeds.Items.RemoveAt(lstbFeeds.Items.Count - 1);

            AddFeedsToList(newFeeds);

            //add the new feeds to the common list
            feeds.AddRange(newFeeds);
            CreateButton(feeds[feeds.Count - 1].ID);

            pgbLoadFeed.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Same as LoadMoreFeeds but this method adds the feed on top insead of in the end.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="id"></param>
        private async void LoadNewFeedsFromId(int amount, int id)
        {
            pgbLoadFeed.Visibility = System.Windows.Visibility.Visible;

            List<PortableBLL.Feed> newFeeds = await fm.LoadFeedsByType(currentFeedType, amount, id + 1);
            //  List<PortableBLL.Feed> newFeeds = await fm.LoadLatestXFeedsFromId(id + 1, amount);

            for (int i = 0; i < newFeeds.Count; i++)
                feeds.Insert(i, newFeeds[i]);


            InsertFeedsToList(newFeeds);
             
            pgbLoadFeed.Visibility = System.Windows.Visibility.Collapsed;
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
                    FillFeedList((PortableBLL.HumanFeed)feeds[i], -1);
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
            Button b = new Button();
            b.Name = "btnLoadNewFeeds";
            b.Width = 456;
            b.Height = 80;
            b.Content = "Load more feeds";

            b.Click += (s, e) => { LoadMoreFeedsFromId(NUMBEROFFEEDS, id); };
            lstbFeeds.Items.Add(b);
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
  
        /// <summary>
        /// When the user has finished taken a photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CameraCapture_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
             //   MessageBox.Show(e.ChosenPhoto.Length.ToString());
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

                //chosenImg = Convert.ToBase64String(photoBytes);

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

        /// <summary>
        /// NOT USED?!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClickalbe_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml", UriKind.Relative));
        }
      
        /// <summary>
        /// Fill the list with human feeds, send -1 as index if it should be added in the end
        /// </summary>
        /// <param name="hf"></param>
        /// <param name="index"></param>
        private void FillFeedList(PortableBLL.HumanFeed hf, int index)
        {
            //CHECK IF THE FEED CONTATINS A PICTURE!!

            if (hf.MediaFilePath == "none" || hf.MediaFilePath == "")
            {
                NoImageFeedControl nfc = new NoImageFeedControl(hf);

                if (index == -1)
                    lstbFeeds.Items.Add(nfc);
                else
                    lstbFeeds.Items.Insert(index, nfc);
            }
            else
            {
                FeedControl fc = new FeedControl(hf);

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

            SensorFeedControl sfc = new SensorFeedControl(t.Owner.ID, t.Owner.UserName, t.Content,  t.Location, t.TimeStamp);
            lstbFeeds.Items.Add(sfc);
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

            //feed.Owner.ID, feed.Content, feed.MediaFilePath, feed.Category.Id
            if (txtbContent.Text == "" || currentUser.ID == -1)
                return;

            hf.Owner.ID = currentUser.ID;
            hf.Content = txtbContent.Text;
            hf.MediaFilePath = (String.IsNullOrEmpty(chosenImg) ? "none" : chosenImg);
            hf.Category.Id = 2;

            //DEBUG STUFF
           // lblTags.Text = hf.MediaFilePath;

            bool res = await fm.PublishFeed(hf);

            if (res)
            {
                txtbContent.Text = "";
                MessageBox.Show("Feed Published");
            }
            else
                MessageBox.Show("Something went wrong");
        }


        private void brdrHuman_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            brdrHuman.Background = GetColorFromHexa("#FFB5BBBB");
            brdrSensor.Background = GetColorFromHexa("#FF515B5B");

            ChangeFeedType(FeedType.FeedSource.Human);
        }

        private void brdrSensor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            brdrSensor.Background = GetColorFromHexa("#FFB5BBBB");
            brdrHuman.Background = GetColorFromHexa("#FF515B5B");

            ChangeFeedType(FeedType.FeedSource.Sensor);
            
        }

        private void ChangeFeedType(FeedType.FeedSource feedType)
        {
             if (!ini)
                return;

             try
             {
                 currentFeedType = feedType;

                 lstbFeeds.Items.Clear();
                 feeds.Clear();
                 LoadNewFeeds(NUMBEROFFEEDS);
             }
             catch (Exception e)
             {

                 MessageBox.Show(e.Message);
             }

        }
   
        private SolidColorBrush GetColorFromHexa(string hexaColor)
        {
            return new SolidColorBrush(
                Color.FromArgb(
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16),
                    Convert.ToByte(hexaColor.Substring(7, 2), 16)
                )
            );
        }

        private void TagIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void GoToMyProfile(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml?userID=" + currentUser.ID, UriKind.Relative));
        }

        private void OnLogOut(object sender, EventArgs e)
        {
            MessageBox.Show("You will log out if you click here");
        }

        private void OnSearchUser(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/SearchUser.xaml", UriKind.Relative));            
        }

     
    }
}