using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace PortableTransformationLayer
{
    interface IFeedData
    {
        Task<List<GetFeedComments_Result>> GetFeedComments(int feedId);
        Task<List<GetFeedTags_Result>> GetFeedTags(int feedId);
        Task<bool> PublishComment(int feedId, string username, string comment);
        Task<bool> AddFeedTag(int feedId, string username);
        Task<int> PublishFeed(int usrId, string text, string filepath, int prioId);
        Task<int> PublishTestFeed(int usrId, string text, byte[] fileArray, int prioId);
        Task<List<GetLatestXFeeds_Result>> GetFeedsByFilter(int userId, string location, DateTime startingTime, DateTime endingTime, string feedType, int startId, int numFeeds);
        Task<GetLatestXFeeds_Result> GetFeedByFeedId(int feedId);
    }
}
