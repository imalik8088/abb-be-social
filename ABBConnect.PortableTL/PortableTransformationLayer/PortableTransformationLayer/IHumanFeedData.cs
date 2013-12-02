using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace PortableTransformationLayer
{
    interface IHumanFeedData
    {
        Task<List<GetAllHumanFeeds_Result>> GetHumanFeeds();
        Task<List<GetAllHumanFeedsByFilter_Result>> GetHumanFeedsByFilter(string location, string startingTime, string endingTime);
    }
}
