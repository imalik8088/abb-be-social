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
using BLL;
using System.Threading.Tasks;

namespace ABBConnect___Windows_Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        // Constructor
        public MainPage()
        {

            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);


            //  List<Test> feeds = new List<Test>();

            CallMe();
            
           // CreateControlsUsingObjects();

     
        }

        private async void CallMe()
        {
            BLL.FeedManager fm = new FeedManager();

            List<BLL.Feed> feeds = await fm.LoadLatestXFeeds(40);

            for (int i = 0; i < feeds.Count; i++)
            {
                if (feeds[i] is HumanFeed)
                    FillFeedList((HumanFeed)feeds[i]);
                else
                    FillFeedList((SensorFeed)feeds[i]);
            }
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }


        }


        private void btnTakePicture_Click(object sender, RoutedEventArgs e)
        {

 
        }
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
                
            }
        }

        private void lblClickalbe_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ProfileFeed.xaml", UriKind.Relative));
        }
        private void FillFeedList(HumanFeed t)
        {
            NoImageFeedControl nfc = new NoImageFeedControl(t.Owner.ID, t.Owner.UserName, t.Content, t.Tags.Count, t.Comments.Count, t.Location, t.TimeStamp);
            lstbFeeds.Items.Add(nfc);
        }

        private void FillFeedList(SensorFeed t)
        {

            SensorFeedControl sfc = new SensorFeedControl(t.Owner.ID, t.Owner.Name, t.Content,  t.Location, t.TimeStamp);
            lstbFeeds.Items.Add(sfc);
        }

        private async void piFeed_Loaded(object sender, RoutedEventArgs e)
        {



             

        }

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
    }
}