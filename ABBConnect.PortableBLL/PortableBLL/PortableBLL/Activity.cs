using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;

namespace PortableBLL
{
    public class Activity
    {

        public Activity()
        {
            this.iD = -1;
            this.userId = -1;
            this.feedId = -1;
            this.text = "";
            this.type = "";
            this.timestamp = DateTime.MinValue;
        }

        public Activity(GetUserActivity_Result entityActivity)
        {
            this.iD = entityActivity.Id;
            this.userId = entityActivity.UserId;
            this.feedId = entityActivity.FeedId;
            this.text = entityActivity.Text;
            this.type = entityActivity.Type;
            this.timestamp = entityActivity.Timestamp;
        }

        private int iD;
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

        private int userId;
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

        private int feedId;
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

        private string type;
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

        private string text;
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

        private DateTime timestamp;
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
