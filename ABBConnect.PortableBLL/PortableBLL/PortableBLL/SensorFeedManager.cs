using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class SensorFeedManager: ISensorFeedManager
    {

        public SensorFeedManager()
        {
           
        }

        public List<SensorFeed> GetAllSensorFeeds()
        {
            throw new NotImplementedException();
           
        }

        public List<SensorFeed> GetAllSensorFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            throw new NotImplementedException();
         
        }

        private static bool CastStringToInt(string str, ref int returnValue)
        {
            throw new NotImplementedException();
           
        }
    }
}
