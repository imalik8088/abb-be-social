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
    class FeedData: IFeedData
    {
        private Connection urlServer;

        public FeedData()
        {
            urlServer = new Connection();
        }

        public async Task<List<ABBConnectServiceRef.GetLatestXFeeds_Result>> GetLatestXFeeds(int X)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetLatestXFeeds/" + X.ToString()).ConfigureAwait(false);

            //   var result =  response.Content.ReadAsStringAsync().Result;
            List<GetLatestXFeeds_Result> ret = JsonConvert.DeserializeObject<List<GetLatestXFeeds_Result>>(response);

            // Task<List<GetLatestXFeeds_Result>> namn = ret;
            return ret;
        }

        public async Task<List<ABBConnectServiceRef.GetLatestXFeeds_Result>> GetLatestXFeedsFromId(int X, int Id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetLatestXFeeds/" + X.ToString() + "&" + Id.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetLatestXFeeds_Result>>(response); 
        }

        public async Task<List<ABBConnectServiceRef.GetFeedComments_Result>> GetFeedComments(int feedId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetFeedComments/" + feedId).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetFeedComments_Result>>(response);
        }

        public async Task<List<ABBConnectServiceRef.GetFeedTags_Result>> GetFeedTags(int feedId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetFeedTags/" + feedId).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetFeedTags_Result>>(response);
        }

        public async Task<int> PublishFeed(int usrId, string text, string filepath, int prioId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("PostFeed/" + usrId.ToString() + "&" + text + "&" + filepath + "&" + prioId.ToString()).ConfigureAwait(false);
            var obj = JsonConvert.DeserializeObject<string>(response);
            return int.Parse(obj);
        }

        public async Task<List<GetUserFeedsByFilter_Result>> GetUserFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetUserFeedsByFilter/" + location 
                + "/" + startingTime.ToString() + "/" + endingTime.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetUserFeedsByFilter_Result>>(response);
        }


        public async Task<List<GetUserFeeds_Result>> GetUserFeeds()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetUserFeeds").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetUserFeeds_Result>>(response);
        }


        public async Task<List<GetFeedsByFilter_Result>> GetFeedsByFilter(string name, string location, DateTime startingTime, DateTime endingTime, string feedType)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("FilterFeeds/" + name + "/" + location
                + "/" + startingTime.ToString() + "/" + endingTime.ToString() + "/" + feedType).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetFeedsByFilter_Result>>(response);
        }

        public async Task<List<GetLatestFeedsByFilter_Result>> GetLatestFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("FilterLatestFeeds/" + location
                + "/" + startingTime.ToString() + "/" + endingTime.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetLatestFeedsByFilter_Result>>(response);
        }
    }
}
