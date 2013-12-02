using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace PortableTransformationLayer
{
    interface ISensorFeedData
    {
        Task<List<GetAllSensorFeeds_Result>> GetSensorFeeds();
        Task<List<GetAllSensorFeedsByFilter_Result>> GetSensorFeedsByFilter(string location, string startingTime, string endingTime);
    }
}
