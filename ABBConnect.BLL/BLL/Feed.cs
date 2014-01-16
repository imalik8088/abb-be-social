using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    /// <summary>
    /// Class that rappresent a general feed including all the informations about it
    /// </summary>
    public class Feed
    {
        /// <summary>
        /// Constructor that instatiate automatically the attributes of the class with empty values
        /// </summary>
        public Feed()
        {
            this.comments = new List<Comment>();
            this.tags = new List<Human>();
            this.timeStamp = DateTime.MinValue;
            this.location = "";
            this.content = "";
            this.category = new Category();
            this.category.CategoryName = "";
        }

        /// <summary>
        /// Constructor of the class that instantiate the attributes of the class with the given data
        /// </summary>
        /// <param name="res">Class that contain the creation date, the location, the contet, the category, and the priority of feed</param>
        /// <param name="listCom">List of the comments related with the feed</param>
        /// <param name="listTag">List of users tagged on the feed</param>
        public Feed(GetLatestXFeeds_Result res, List<Comment> listCom, List<Human> listTag)
        {
            category = new Category();
            this.comments = new List<Comment>();
            this.tags = new List<Human>();

            comments = listCom;
            tags = listTag;
            timeStamp = res.CreationTimeStamp;
            location = res.Location;
            content = res.Text;
            category.CategoryName = res.PrioCategory;
            category.Priority = res.PrioValue;
        }

        /// <summary>
        /// Attribute that rappresent the unique identificator of the post
        /// </summary>
        private int iD;
        /// <summary>
        /// properties that allow to modify or take the value of the ID of a feed
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
        /// attribute that rappresent the date of creation of the feed
        /// </summary>
        private DateTime timeStamp;
        /// <summary>
        /// properties that allow to modify or take the value of the time of creation of the feed
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
        /// attribute that rappresent the list of users tagged in the feed
        /// </summary>
        private List<Human> tags;
        /// <summary>
        /// properties that allow to modify or take the list of users tagged in the feed
        /// </summary>
        public List<Human> Tags
        {
            get
            {
                return tags;
            }
            set
            {
                tags = value;
            }
        }

        /// <summary>
        /// Attribute that rappresent the location of the feed
        /// </summary>
        private string location;
        /// <summary>
        /// properties that allow to modify or take the location of the current feed
        /// </summary>
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        /// <summary>
        /// Attribute that rappresent the content of the feed
        /// </summary>
        private string content;
        /// <summary>
        /// properties that allow to modify or take the content of the current feed
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

        /// <summary>
        /// Attribute that rappresent the category of the feed
        /// </summary>
        private Category category;
        /// <summary>
        /// properties that allow to modify or take the category of the current feed
        /// </summary>
        public Category Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        /// <summary>
        /// Attribute that rappresent the List of comments of the current feed
        /// </summary>
        private List<Comment> comments;
        // <summary>
        /// properties that allow to modify or take the list of comments related with the current feed
        /// </summary>
        public List<Comment> Comments
        {
            get
            {
                return comments;
            }
            set
            {
                comments = value;
            }
        }

        /// <summary>
        /// Attribute that communicate if the author of the feed is a human user or a sensor
        /// </summary>
        private string feedType;
        /// <summary>
        /// properties that allow to modify or take the type of the author, that could be human or sensor
        /// </summary>
        public string FeedType
        {
            get { return feedType; }
            set { feedType = value; }
        }
    }
}
