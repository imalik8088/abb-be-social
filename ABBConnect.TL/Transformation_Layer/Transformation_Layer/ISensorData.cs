using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Transformation_Layer
{
    interface ISensorData
    {
        int RetrieveCurrentSensorData(int sensorID);
        DataSet GetSensorInformation(int sensorID);
        DataSet RetrieveHistoricalSensorData(int sensorID, DateTime startingTime, DateTime endingTime);
    }
}
