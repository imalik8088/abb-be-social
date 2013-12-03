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
        Task<List<GetLatestXFeeds_Result>> GetLatestXFeeds(int X);
        Task<List<GetLatestXFeeds_Result>> GetLatestXFeedsFromId(int X, int Id);
        Task<List<GetFeedComments_Result>> GetFeedComments(int feedId);
        Task<List<GetFeedTags_Result>> GetFeedTags(int feedId);
        Task<int> PublishFeed(int usrId, string text, string filepath, int prioId);
        Task<List<GetUserFeeds_Result>> GetUserFeeds();
        Task<List<GetUserFeedsByFilter_Result>> GetUserFeedsByFilter(string location, DateTime startingTime, DateTime endingTime);
        Task<List<GetFeedsByFilter_Result>> GetFeedsByFilter(string name, string location, DateTime startingTime, DateTime endingTime, string feedType);
        Task<List<GetLatestFeedsByFilter_Result>> GetLatestFeedsByFilter(string location, DateTime startingTime, DateTime endingTime);
    }
}
