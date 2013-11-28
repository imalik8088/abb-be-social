using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    interface ISensorManager
    {
        Sensor LoadSensorInformation(int sensorID);
        SensorHistoryData LoadHistoryValuesBySensor(int sensorID, DateTime startingTime, DateTime endingTime);
        int LoadCurrentValuesBySensor(int sensorID);
    }
}
