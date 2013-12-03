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
    public class HumanFeedData:IHumanFeedData
    {
        private Connection urlServer;

        public HumanFeedData()
        {
            urlServer = new Connection();
        }

        public async Task<List<GetAllHumanFeeds_Result>> GetHumanFeeds()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetHumanFeeds").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetAllHumanFeeds_Result>>(response); 
        }

        public async Task<List<GetAllHumanFeedsByFilter_Result>> GetHumanFeedsByFilter(string location, string startingTime, string endingTime)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetHumanFeedsByFilter/" + location + "/" + startingTime + "/" + endingTime).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetAllHumanFeedsByFilter_Result>>(response); 
        }
    }
}
