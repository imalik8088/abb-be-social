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
    public class SensorFeedData: ISensorFeedData
    {
        private Connection urlServer;

        public SensorFeedData()
        {
            urlServer = new Connection();
        }

        public async Task<List<GetAllSensorFeeds_Result>> GetSensorFeeds()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetSensorFeeds").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetAllSensorFeeds_Result>>(response); 
        }

        public async Task<List<GetAllSensorFeedsByFilter_Result>> GetSensorFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetSensorFeedsByFilter?location=" 
                + location + "&start=" + startingTime.ToString() + "&end=" + endingTime.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetAllSensorFeedsByFilter_Result>>(response);
        }
    }
}
