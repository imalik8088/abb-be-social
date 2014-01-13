using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;


namespace BLL
{
    /// <summary>
    /// Class that represent activity of a specific user.
    /// The activity could be like make a comment, or a feed, or a reference to another user.
    /// </summary>
    public class Activity
    {
        /// <summary>
        /// Constructor that instatiate automatically the attributes of the class with empty values
        /// </summary>
        public Activity()
        {
            this.iD = -1;
            this.userId = -1;
            this.feedId = -1;
            this.text = "";
            this.type = "";
            this.timestamp = DateTime.MinValue;
        }

        /// <summary>
        /// Constructor of the class that instantiate the attributes of the class with the given data
        /// </summary>
        /// <param name="entityActivity">Class that contain the informations about the user and the activity</param>
        public Activity(GetUserActivity_Result entityActivity)
        {
            this.iD = entityActivity.Id;
            this.userId = entityActivity.UserId;
            this.feedId = entityActivity.FeedId;
            this.text = entityActivity.Text;
            this.type = entityActivity.Type;
            this.timestamp = entityActivity.Timestamp;
        }

        /// <summary>
        /// Attribute that rappresent the unique identificator of the activity
        /// </summary>
        private int iD;
        /// <summary>
        /// Properties that allow to modify or take the value of the ID of the activity
        /// </summary>
        public int ID
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
        }

        /// <summary>
        /// Attribute that rappresent the unique identificator of the user that make the activity
        /// </summary>
        private int userId;
        /// <summary>
        /// Properties that allow to modify or take the identificator of the user that make the activity
        /// </summary>
        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        /// <summary>
        /// Attribute that rappresent the unique identificator of the feed
        /// </summary>
        private int feedId;
        /// <summary>
        /// Properties that allow to modify or take the identificator of the feed
        /// </summary>
        public int FeedId
        {
            get
            {
                return feedId;
            }
            set
            {
                feedId = value;
            }
        }

        /// <summary>
        /// Attribute that rappresent the type of the activity
        /// </summary>
        private string type;
        /// <summary>
        /// Properties that allow to modify or take the type of the activity
        /// </summary>
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        /// <summary>
        /// Attribute that rappresent the content of the activity
        /// </summary>
        private string text;
        /// <summary>
        /// Properties that allow to modify or take the content of the activity
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        /// <summary>
        /// Attribute that rappresent the time of creation of the activity
        /// </summary>
        private DateTime timestamp;
        /// <summary>
        /// Properties that allow to modify or take the time of creation of the activity
        /// </summary>
        public DateTime Timestamp
        {
            get
            {
                return timestamp;
            }
            set
            {
                timestamp = value;
            }
        }
    }
}
