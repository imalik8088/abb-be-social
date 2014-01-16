
using System.Collections.Generic;
using DAL;

namespace BLL
{
    /// <summary>
    /// Class that rapprent the feed made by a human user
    /// </summary>
    public class HumanFeed: Feed
    {
        /// <summary>
        /// Constructor of the class that automatically instantiate the attribute of the class
        /// </summary>
        public HumanFeed()
        {
            owner = new Human();
            mediaFilePath = "";
        }

        /// <summary>
        /// Costructor of the class that instantiate the attribute of the class with the parameter in input
        /// </summary>
        /// <param name="res">Object containing all the information about the feed</param>
        /// <param name="listCom">List of comments made to the feed</param>
        /// <param name="listTag">List of human users that are referenced into the feed</param>
        /// <param name="owner">Class that rappresent the human user that made the feed</param>
        public HumanFeed(GetLatestXFeeds_Result res, List<Comment> listCom, List<Human> listTag, Human owner)
        {
            base.Category = new Category();
            base.Comments = new List<Comment>();
            base.Tags = new List<Human>();
            this.owner = new Human();

            base.ID = res.FeedId;
            base.Comments = listCom;
            base.Tags = listTag;
            base.TimeStamp = res.CreationTimeStamp;
            base.Location = res.Location;
            base.Content = res.Text;
            base.Category.CategoryName = res.PrioCategory;
            base.Category.Priority = res.PrioValue;

            this.owner = owner;
            this.owner.ID = res.UserId;
            
            mediaFilePath = res.FilePath;

        }

        /// <summary>
        /// Attribute that rappresent the human user that made the comment
        /// </summary>
        private Human owner;
        /// <summary>
        /// Properties that allow to change or retrieve the class that represent the human user that made the comment
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
        /// Attribute that the path where is stored the media file attached to the feed
        /// </summary>
        private string mediaFilePath;
        /// <summary>
        /// Properties that allow to change or retrieve the path where is stored the media file attached to the feed
        /// </summary>
        public string MediaFilePath
        {
            get
            {
                return mediaFilePath;
            }
            set
            {
                mediaFilePath = value;
            }
        }
    }
}
