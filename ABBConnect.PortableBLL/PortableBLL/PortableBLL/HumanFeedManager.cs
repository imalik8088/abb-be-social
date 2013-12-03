using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;


namespace BLL
{
    public class HumanFeedManager: IHumanFeedManager
    {

        private HumanFeedData humFeedData;

        public HumanFeedManager()
        {
            humFeedData = new HumanFeedData();
        }

        // METHOD: GetAllUserFeeds

        public async Task<List<HumanFeed>> LoadAllHumanFeeds()
        {
            throw new NotImplementedException();
            
        }

        // METHOD: GetAllUserFeedsByFilter

        public List<HumanFeed> LoadAllHumanFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            throw new NotImplementedException();

        }
    }
}
