using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

/*
 * Written by: Robert Gustavsson
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */

namespace ABBConnect___Windows_Phone
{
    /// <summary>
    /// A control to hold the Comment object for a better presentation of the data, but also logic
    /// </summary>
    public partial class CommentControl : UserControl
    {

        /// <summary>
        /// Defualt constructor
        /// </summary>
        public CommentControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="creationTime"></param>
        /// <param name="userId"></param>
        /// <param name="text"></param>
        public CommentControl(string fullName, DateTime creationTime, int userId, string text)
        {
            InitializeComponent();

            lblUserName.Text = fullName;
            lblUserName.Tag = userId;
            lblText.Text = text;
            SetTime(creationTime);
        }

        /// <summary>
        /// Constructor with the Object Comment as param, this is currently used
        /// </summary>
        /// <param name="comment"></param>
        public CommentControl(PortableBLL.Comment comment)
        {
            InitializeComponent();

            lblUserName.Text = comment.Owner.FirstName + " " + comment.Owner.LastName;
            lblUserName.Tag =  comment.Owner.UserName;
            lblText.Text = comment.Content;
            SetTime(comment.TimeStamp);
        }

        /// <summary>
        /// Set the time of how long it has passed since the comment was madw
        /// </summary>
        /// <param name="dateTime"></param>
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

        /// <summary>
        /// Not used for now
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblUserName_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
