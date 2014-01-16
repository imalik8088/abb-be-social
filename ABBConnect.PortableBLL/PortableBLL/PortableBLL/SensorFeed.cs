using System.Collections.Generic;
using PortableTransformationLayer.ABBConnectServiceRef;

namespace PortableBLL
{
    /// <summary>
    /// Class that rappresent the feed made by a sensor
    /// </summary>
    public class SensorFeed: Feed
    {
        public SensorFeed()
        {
            owner = new Sensor();
        }

        /// <summary>
        /// Constructor that instantiate the attributes of the class with the values given in input
        /// </summary>
        /// <param name="res">Object containing all the information about the feed</param>
        /// <param name="listCom">List of comments made to the feed</param>
        /// <param name="listTag">List of human users that are referenced into the feed</param>
        /// <param name="owner">Class that rappresent the sensor that made the feed</param>
        public SensorFeed(GetLatestXFeeds_Result res, List<Comment> listCom, List<Human> listTag, Sensor owner)
        {
            base.Category = new Category();
            base.Comments = new List<Comment>();
            base.Tags = new List<Human>();
            this.owner = new Sensor();

            base.ID = res.FeedId;
            base.Comments = listCom;
            base.Tags = listTag;
            base.TimeStamp = res.CreationTimeStamp;
            base.Location = res.Location;
            base.Content = res.Text;
            base.Category.CategoryName = res.PrioCategory;
            base.Category.Priority = res.PrioValue;

            this.owner = owner;

        }


        /// <summary>
        /// Attribute that rappresent the sensor that made the comment
        /// </summary>
        private Sensor owner;
        /// <summary>
        /// Properties that allow to change or retrieve the class that represent the sensor that made the comment
        /// </summary>
        public Sensor Owner
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
    }
}
