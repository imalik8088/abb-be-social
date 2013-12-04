using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace PortableBLL
{
    public class SensorManager:ISensorManager
    {

        private SensorData senData;

        public SensorManager()
        {
            senData = new SensorData();
        }

        public async Task<Sensor> LoadSensorInformation(int sensorID)
        {
            GetSensorInformation_Result tempSensor = await senData.GetSensorInformation(sensorID).ConfigureAwait(false);
            Sensor responseSensor = new Sensor(tempSensor);
            return responseSensor;
        }

        public async Task<SensorHistoryData> LoadHistoryValuesBySensor(int sensorID, DateTime startingTime, DateTime endingTime)
        {
            throw new NotImplementedException();
        }

        public async Task<int> LoadCurrentValuesBySensor(int sensorID)
        {
            return await senData.GetLastSensorValue(sensorID).ConfigureAwait(false);
        }
    }
}
