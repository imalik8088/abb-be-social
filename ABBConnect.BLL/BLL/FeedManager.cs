using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd;
using System.Data;

namespace BLL
{
    public class FeedManager: IFeedManager
    {
        private FeedData postDbData;
        private HumanFeedData humanPostDbData;

        public FeedManager()
        {
            this.postDbData = new FeedData();
            this.humanPostDbData = new HumanFeedData();
        }

        public List<Feed> LoadNewFeeds()
        {
            throw new NotImplementedException();
        }

        public List<Feed> LoadHistoryFeedsBySensor(int sensorID)
        {
            throw new NotImplementedException();
        }

        public bool PublishFeed(UserFeed feed)
        {
            return this.postDbData.PublishFeed(feed.Owner.UserName, null, feed.Location, 
                feed.Content, feed.Category, feed.MediaFilePath, feed.ID);
        }

        public void PublishComment(int feedID, Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
