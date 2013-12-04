using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableBLL
{
    interface IFeedManager
    {
        Task<bool> PublishFeed(HumanFeed feed);
        Task<bool> PublishComment(int feedID, Comment comment);
        Task<bool> AddTagToFeed(int feedId, string username);
        Task<List<Human>> LoadFeedTags(int feedId);
        Task<List<Comment>> LoadFeedComments(int feedId);
        Task<List<Feed>> LoadLatestXFeeds(int numberOfFeeds);
        Task<List<Feed>> LoadLatestXFeedsFromId(int startingId, int numberOfFeeds);
        Task<List<Feed>> LoadFeedsByFilter(string username, string location, DateTime startingTime, DateTime endingTime, string feedType);
        Task<List<Feed>> LoadFeedsByType(FeedType feedType, int numFeeds);
        Task<List<Feed>> LoadFeedsByType(FeedType feedType, int numFeeds, int startId);
        Task<List<Feed>> LoadFeedsByDate(DateTime feedStartTime, DateTime feedEndTime, int numFeeds);
        Task<List<Feed>> LoadFeedsByDate(DateTime feedStartTime, DateTime feedEndTime, int numFeeds, int startId);
        Task<List<Feed>> LoadFeedsByLocation(string location, int numFeeds);
        Task<List<Feed>> LoadFeedsByLocation(string location, int numFeeds, int startId);
        Task<List<Feed>> LoadFeedsByUser(int userId, int numFeeds);
        Task<List<Feed>> LoadFeedsByUser(int userId, int numFeeds, int startId);

    }
}
