using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Transformation_Layer
{
    interface ISensorFeedData
    {
        DataSet GetAllSensorFeeds();
        DataSet GetAllSensorFeedsByFilter(string location, DateTime startingTime, DateTime endingTime);
    }
}
