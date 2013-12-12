using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ABBConnect___Windows_Phone
{
    public partial class CommentControl : UserControl
    {
        private PortableBLL.Comment comment;


        public CommentControl()
        {
            InitializeComponent();
        }

        public CommentControl(string fullName, DateTime creationTime, int userId, string text)
        {
            InitializeComponent();

            lblUserName.Text = fullName;
            lblUserName.Tag = userId;
            lblText.Text = text;
            SetTime(creationTime);
        }

        public CommentControl(PortableBLL.Comment comment)
        {
            InitializeComponent();

            lblUserName.Text = comment.Owner.FirstName + " " + comment.Owner.LastName;
            lblUserName.Tag =  comment.Owner.UserName;
            lblText.Text = comment.Content;
            SetTime(comment.TimeStamp);
        }

        private void SetTime(DateTime dateTime)
        {

            DateTime now = DateTime.Now;

            double hours = (now - dateTime).TotalHours;

            if (hours < 1)
                lblTime.Text = Math.Round((now - dateTime).TotalMinutes).ToString() + "m";
            else if (hours > 24)
                lblTime.Text = Math.Round((now - dateTime).TotalDays).ToString() + "d";
            else
                lblTime.Text = Math.Round(hours).ToString() + "h";
        }

        private void lblUserName_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
