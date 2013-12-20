using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableBLL
{
    /// <summary>
    /// Class that rappresent the comment to a post, reporting its content and all the attached informations
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Constructor of the class that instantiate the attributes with standard values
        /// </summary>
        public Comment()
        {
            this.owner = new Human();
            this.content = "";
            this.timeStamp = DateTime.MinValue;
        }

        /// <summary>
        /// Constructor of the class that instantiate the attributes with the given values
        /// </summary>
        /// <param name="res">Class that rapresent the comment of a feed</param>
        public Comment(PortableTransformationLayer.ABBConnectServiceRef.GetFeedComments_Result res, Human owner)
        {
            this.owner = new Human();
            timeStamp = res.CreationTimeStamp;
            Content = res.CommentText;
            this.owner = owner;
        }

        /// <summary>
        /// Attribute that rappresent the  identifier of a comment
        /// </summary>
        private int iD;

        /// <summary>
        /// Properties that allow to modify or take the value of the ID of a comment
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
        /// Attribute rappresenting the human user that made the current comment
        /// </summary>
        private Human owner;

        /// <summary>
        /// properties that allow to modify or take the human user that made the comment
        /// </summary>
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

        /// <summary>
        /// Attribute that rappresent the time when the comment was made
        /// </summary>
        private DateTime timeStamp;

        /// <summary>
        /// Properties that allow to modify or take the time when the comment was made
        /// </summary>
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

        /// <summary>
        /// Attribute that rappresent the text contained in a comment
        /// </summary>
        private string content;

        /// <summary>
        /// Properties that allow to modify or take the textual content of a comment
        /// </summary>
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
