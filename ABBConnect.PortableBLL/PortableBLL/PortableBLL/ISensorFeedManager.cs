using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    interface ISensorFeedManager
    {
        List<SensorFeed> GetAllSensorFeedsByFilter(string location, DateTime startingTime, DateTime endingTime);
        List<SensorFeed> GetAllSensorFeeds();
    }
}
