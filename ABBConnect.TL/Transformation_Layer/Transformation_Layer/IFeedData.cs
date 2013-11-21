using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Transformation_Layer
{
    interface IFeedData
    {
        DataSet GetLatestXFeeds(int numberOfFeeds);
        DataSet GetFeedTags(int feedId);
        DataSet GetFeedComments(int feedId);
        DataSet GetFeedsByFilter(string name, string location, DateTime startingTime, DateTime endingTime, string feedType);
        bool PublishComment(int feedID, string username, string comment);
        bool IncludeTagFeed(int feedId, string username);
        bool PublishFeed(int userId, List<string> tags, string location, string content, string category, string filePath, int priorityID);
        DataSet GetLatestFeedByFilter(string location, DateTime startingTime, DateTime endingTime);
        DataSet GetLatestFeeds();
    }
}
