using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface ISensorManager
    {
        Sensor LoadSensorInformation(int sensorID);
        SensorHistoryData LoadHistoryValuesBySensor(int sensorID);
        int LoadCurrentValuesBySensor(int sensorID);
    }
}
