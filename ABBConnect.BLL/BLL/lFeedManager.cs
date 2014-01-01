using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface IFeedManager
    {
        bool PublishFeed(HumanFeed feed);
        bool PublishComment(int feedID, Comment comment);
        bool AddTagToFeed(int feedId, string username);
        List<Human> LoadFeedTags(int feedId);
        List<Comment> LoadFeedComments(int feedId);
        List<Feed> LoadLastShiftFeeds(int numberOfFeeds);
        List<Feed> LoadLatestXFeeds(int numberOfFeeds);
        List<Feed> LoadLatestXFeedsFromId(int startingId, int numberOfFeeds);
        List<Feed> LoadFeedsByType(FeedType.FeedSource feedType, int numFeeds);
        List<Feed> LoadFeedsByType(FeedType.FeedSource feedType, int numFeeds, int startId);
        List<Feed> LoadFeedsByDate(DateTime feedStartTime, DateTime feedEndTime, int numFeeds);
        List<Feed> LoadFeedsByDate(DateTime feedStartTime, DateTime feedEndTime, int numFeeds, int startId);
        List<Feed> LoadFeedsByLocation(string location, int numFeeds);
        List<Feed> LoadFeedsByLocation(string location, int numFeeds, int startId);
        List<Feed> LoadFeedsByUser(int userId, int numFeeds);
        List<Feed> LoadFeedsByUser(int userId, int numFeeds, int startId);
        List<Feed> LoadFeedsFromSavedFilter(Filter savedFilter, int numFeed);
        List<Feed> LoadFeedsFromSavedFilter(Filter savedFilter, int numFeed, int startId);
        Feed GetFeedByFeedId(int feedId);
    }
}
