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
        bool AddTagToFeed(int feedId, string username);
        List<Human> LoadFeedTags(int feedId);
        List<Comment> LoadFeedComments(int feedId);
        Task<List<Feed>> LoadLatestXFeeds(int numberOfFeeds);
        Task<List<Feed>> LoadLatestXFeedsFromId(int startingId, int numberOfFeeds);
        List<Feed> LoadFeedsByFilter(string username, string location, DateTime startingTime, DateTime endingTime, string feedType);
        List<Feed> LoadNewFeedsByFilter(string location, DateTime startingTime, DateTime endingTime);
        List<Feed> GetUserFeedByFilter(int userId, string location, DateTime startingTime, DateTime endingTime);
        List<Feed> GetUserFeeds(int userId);
    }
}
