using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace PortableTransformationLayer
{
    interface ISensorData
    {
        Task<GetSensorInformation_Result> GetSensorInformation(int id);
        Task<List<GetHistoricalDataFromSensor_Result>> GetHistoricalDataFromSensor(int id, DateTime startingTime, DateTime endingTime);
        Task<int> GetLastSensorValue(int id);
    }
}
