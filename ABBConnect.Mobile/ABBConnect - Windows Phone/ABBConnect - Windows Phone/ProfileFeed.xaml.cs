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
        Human currentUser;

        public ProfileFeed()
        {
            InitializeComponent();
            currentUser = new Human();
        }

        private void FillFeedList(Test t)
        {
           // NoImageFeedControl nfc = new NoImageFeedControl(t.Author, "rgn09003", t.Content, t.Tags.Count, t.Comments.Count, t.Location, t.Timestamp);
            FeedControl fc = new FeedControl(0, "rgn09003", t.Content, t.Tags.Count, t.Comments.Count, t.Location, t.Timestamp, "");

            lstbFeeds.Items.Add(fc);

        }

        protected  async override void OnNavigatedTo(NavigationEventArgs e)
        {
            UserManager um = new UserManager();
            string strUserId = NavigationContext.QueryString["userID"];

            int userId = int.Parse(strUserId);

            if (userId == App.CurrentUser.ID) //info already read from DB
                currentUser = App.CurrentUser;
            else
                currentUser = await um.LoadHumanInformation(userId);

            lblEmailClick.Text = currentUser.Email;
            lblNameClick.Text = currentUser.FirstName + " " + currentUser.LastName;
            lblPhoneClick.Text = currentUser.PhoneNumber;
            lblLocationClick.Text = currentUser.WorkRoom;

            pivHead.Title = lblNameClick.Text;
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