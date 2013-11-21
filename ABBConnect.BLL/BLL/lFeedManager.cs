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
        bool PublishFeed(HumanFeed feed);
        bool PublishComment(int feedID, Comment comment);
        bool AddTagToFeed(int feedId, Human user);
        List<Human> LoadFeedTags(int feedId);
        List<Comment> LoadFeedComments(int feedId);
        List<Feed> LoadLatestXFeeds(int numberOfFeeds);
        List<Feed> LoadFeedsByFilter(string username, string location, DateTime startingTime, DateTime endingTime, string feedType);
        List<Feed> LoadNewFeedsByFilter(string location, DateTime startingTime, DateTime endingTime);

    }
}
