using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface ISensorFeedManager
    {
        List<SensorFeed> GetAllSensorFeedsByFilter(string location, DateTime startingTime, DateTime endingTime);
        List<SensorFeed> GetAllSensorFeeds();
    }
}
