using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableBLL
{
    public class Comment
    {
        public Comment()
        {
            this.owner = new Human();
            this.content = "";
            this.timeStamp = DateTime.MinValue;
        }

        public Comment(PortableTransformationLayer.ABBConnectServiceRef.GetFeedComments_Result res)
        {
            this.owner = new Human();
            timeStamp = res.CreationTimeStamp;
            Content = res.CommentText;
            owner.FirstName = res.FirstName;
            owner.LastName = res.LastName;
            owner.UserName = res.UserName;
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

        private Human owner;
        public Human Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        private DateTime timeStamp;
        public DateTime TimeStamp
        {
            get
            {
                return timeStamp;
            }
            set
            {
                timeStamp = value;
            }
        }

        private string content;
        private PortableTransformationLayer.ABBConnectServiceRef.GetFeedComments_Result res;
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }
    }
}
