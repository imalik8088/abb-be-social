using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableBLL
{
    interface IFeedManager
    {
        Task<List<Feed>> LoadNewFeeds();
        Task<bool> PublishFeed(HumanFeed feed);
        Task<bool> PublishComment(int feedID, Comment comment);
        Task<bool> AddTagToFeed(int feedId, string username);
        Task<List<Human>> LoadFeedTags(int feedId);
        Task<List<Comment>> LoadFeedComments(int feedId);
        Task<List<Feed>> LoadLatestXFeeds(int numberOfFeeds);
        Task<List<Feed>> LoadLatestXFeedsFromId(int startingId, int numberOfFeeds);
        Task<List<Feed>> LoadFeedsByFilter(string username, string location, DateTime startingTime, DateTime endingTime, string feedType);
        Task<List<Feed>> LoadNewFeedsByFilter(string location, DateTime startingTime, DateTime endingTime);
        Task<List<Feed>> GetUserFeedByFilter(int userId, string location, DateTime startingTime, DateTime endingTime);
        Task<List<Feed>> GetUserFeeds(int userId);
    }
}
