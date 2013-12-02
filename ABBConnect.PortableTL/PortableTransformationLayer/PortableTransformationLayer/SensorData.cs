using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;

namespace PortableTransformationLayer
{
    class SensorData: ISensorData
    {
        public async Task<ABBConnectServiceRef.GetSensorInformation_Result> GetSensorInformation(int id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync("GetSensorInfo/" + id.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GetSensorInformation_Result>(response);  
        }

        public async Task<List<ABBConnectServiceRef.GetHistoricalDataFromSensor_Result>> GetHistoricalDataFromSensor(int id, DateTime startingTime, DateTime endingTime)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync("GetPastSensorData/" + id.ToString() 
                + "/" + startingTime.ToString() + "/" + endingTime.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetHistoricalDataFromSensor_Result>>(response); 
        }

        public async Task<int> GetLastSensorValue(int id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync("GetLastSensorValue/" + id.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<int>(response);  
        }
    }
}
