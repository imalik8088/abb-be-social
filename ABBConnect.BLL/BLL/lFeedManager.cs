using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface IFeedManager
    {
        List<Feed> LoadNewFeeds();
        List<Feed> LoadHistoryFeedsBySensor(int sensorID);
        bool PublishFeed(UserFeed feed);
        void PublishComment(int feedID, Comment comment);
        List<User> LoadFeedTags(int feedId);
        List<Comment> LoadFeedComments(int feedId);
        List<Feed> LoadLatest10Feeds();
        List<Feed> LoadLatest20Feeds();
    }
}
