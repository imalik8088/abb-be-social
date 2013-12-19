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

namespace ABBConnect___Windows_Phone
{
    public partial class MakeCommentControl : UserControl
    {
        private int feedID;

        public MakeCommentControl(int feedId)
        {
            InitializeComponent();
            feedID = feedId;
        }

        private async void btnPublishComment_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(txtbText.Text) || String.IsNullOrWhiteSpace(txtbText.Text))
            {
                MessageBox.Show("No comment attached");
                return;
            }

            FeedManager fm = new FeedManager();
            Comment c = new Comment();

            c.Owner = App.CurrentUser;
            c.Content = txtbText.Text;

            bool result = await fm.PublishComment(feedID, c);

            if (!result)
                MessageBox.Show("Something went wrong, try again later!");
            else
            {
                MessageBox.Show("Comment published");
                txtbText.Text = "";
            }
        }
    }
}
