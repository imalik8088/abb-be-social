using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace BLL
{
    public class SensorFeedManager: ISensorFeedManager
    {
        SensorFeedData senFeedData;

        public SensorFeedManager()
        {
            senFeedData = new SensorFeedData();   
        }

        public async Task<List<SensorFeed>> GetAllSensorFeeds()
        {
            List<GetAllSensorFeeds_Result> list = await senFeedData.GetSensorFeeds().ConfigureAwait(false);

            List<SensorFeed> senList = new List<SensorFeed>();

            FeedManager feedMng = new FeedManager();

            foreach (GetAllSensorFeeds_Result res in list)
                    senList.Add(new SensorFeed(res, await feedMng.LoadFeedComments(res.FeedId).ConfigureAwait(false), await feedMng.LoadFeedTags(res.FeedId).ConfigureAwait(false)));

            return senList;
           
        }

        public async Task<List<SensorFeed>> GetAllSensorFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            location = (location.Equals("")) ? null : location;
            
            List<GetAllSensorFeedsByFilter_Result> list = await senFeedData.GetSensorFeedsByFilter(location, startingTime, endingTime).ConfigureAwait(false);

            List<SensorFeed> senList = new List<SensorFeed>();

            FeedManager feedMng = new FeedManager();

            foreach (GetAllSensorFeedsByFilter_Result res in list)
                senList.Add(new SensorFeed(res, await feedMng.LoadFeedComments(res.FeedId).ConfigureAwait(false), await feedMng.LoadFeedTags(res.FeedId).ConfigureAwait(false)));

            return senList;
         
        }
    }
}
