using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface ISensorFeedManager
    {
        List<SensorFeed> GetSensorFeedsByFilter(int sensorId, string location, DateTime startingTime, DateTime endingTime);
        List<SensorFeed> GetSensorFeeds(int sensorId);
        List<SensorFeed> GetAllSensorFeedsByFilter(string location, DateTime startingTime, DateTime endingTime);
        List<SensorFeed> GetAllSensorFeeds();
    }
}
