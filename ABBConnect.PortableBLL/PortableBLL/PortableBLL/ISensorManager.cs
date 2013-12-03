using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface ISensorManager
    {
        Task<Sensor> LoadSensorInformation(int sensorID);
        Task<SensorHistoryData> LoadHistoryValuesBySensor(int sensorID, DateTime startingTime, DateTime endingTime);
        Task<int> LoadCurrentValuesBySensor(int sensorID);
    }
}
