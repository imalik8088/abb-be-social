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

namespace ABBConnect___Windows_Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
       // CameraCaptureTask cameraCapture;
        // Constructor
        public MainPage()
        {

            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);



            
           // CreateControlsUsingObjects();

     
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
        void CameraCapture_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
             //   MessageBox.Show(e.ChosenPhoto.Length.ToString());

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
        private void FillFeedList(Test t)
        {
            NoImageFeedControl nfc = new NoImageFeedControl(t.Author,"rgn09003", t.Content, t.Tags.Count, t.Comments.Count, t.Location, t.Timestamp);
            FeedControl fc = new FeedControl(t.Author,"rgn09003", t.Content, t.Tags.Count, t.Comments.Count, t.Location, t.Timestamp, "");
           
            if(new Random().Next(0,100) % 2 == 0)
                lstbFeeds.Items.Add(fc);
            else
                lstbFeeds.Items.Add(nfc);
        }

        private void FillFeedList(TestSensor t)
        {

            SensorFeedControl sfc = new SensorFeedControl(t.Author,1001, t.Content,  t.Location, t.Timestamp);
            lstbFeeds.Items.Add(sfc);
        }

        private void piFeed_Loaded(object sender, RoutedEventArgs e)
        {

          //  List<Test> feeds = new List<Test>();

            for (int i = 0; i < 20; i++)
            {
                if (i % 2 == 0)
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
                else
                {
                    TestSensor t = new TestSensor();
                    t.ID1 = i;
                    t.Content = "ALARM!!!!!!! VALUE IS CRAZY!!";
                    t.Location = "Machine 1";
                    t.Priority = 5;
                    t.Timestamp = DateTime.Now;
                    t.Category = "Sensor Alarm";
                    t.Author = "Sensor 1";

                    FillFeedList(t);
                }
                
            }

        }
    }
}