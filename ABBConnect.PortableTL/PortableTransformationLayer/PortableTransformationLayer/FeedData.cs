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
    public class FeedData: IFeedData
    {
        private Connection urlServer;

        public FeedData()
        {
            urlServer = new Connection();
        }

        public async Task<List<ABBConnectServiceRef.GetFeedComments_Result>> GetFeedComments(int feedId)
        {
            string dateTimePattern = "yy-MM-dd H:mm:ss";
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetFeedComments?feedId=" + feedId + "&date=" + DateTime.Now.ToString(dateTimePattern)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetFeedComments_Result>>(response);
        }

        public async Task<List<ABBConnectServiceRef.GetFeedTags_Result>> GetFeedTags(int feedId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetFeedTags?feedId=" + feedId).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetFeedTags_Result>>(response);
        }

        public async Task<int> PublishFeed(int usrId, string text, string filepath, int prioId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("PostFeed?id=" + usrId.ToString() 
                + "&text=" + text + "&path=" + filepath + "&priority=" + prioId.ToString()).ConfigureAwait(false);
            var obj = JsonConvert.DeserializeObject<string>(response);
            return int.Parse(obj);
        }

        public async Task<List<GetLatestXFeeds_Result>> GetFeedsByFilter(int userId, string location, DateTime startingTime, DateTime endingTime, string feedType, int startId, int numFeeds)
        {
            string dateTimePattern = "yy-MM-dd H:mm:ss";
            string url = String.Format("FilterFeedsByFilter?userId={0}&location={1}&start={2}&end={3}&type={4}&feedId={5}&numFeeds={6}",
                                        userId == -1 ? "" : userId.ToString(),
                                        location,
                                        startingTime == DateTime.MinValue ? "" : startingTime.ToString(dateTimePattern),
                                        endingTime == DateTime.MinValue ? "" : endingTime.ToString(dateTimePattern),
                                        feedType,
                                        startId == -1 ? "" : startId.ToString(),
                                        numFeeds == -1 ? "" : numFeeds.ToString());

            string backUp = "FilterFeedsByFilter?userId=" + userId
                + "&location=" + location + "&start=" + startingTime.ToString() + "&end=" +
                endingTime.ToString() + "&type=" + feedType + "&feedId=" + startId +
                "&numFeeds=" + numFeeds;

            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetLatestXFeeds_Result>>(response);
        }

        public async Task<bool> PublishComment(int feedId, string username, string comment)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("PostComment?feedId=" + feedId +
                           "&username=" + username + "&comment=" + comment).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<bool>(response); ;
        }


        public async Task<bool> AddFeedTag(int feedId, string username)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("AddTag?feedId=" + feedId +
                           "&username=" + username).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<bool>(response); ;
        }


        public async Task<GetLatestXFeeds_Result> GetFeedByFeedId(int feedId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetFeedByFeedId?feedId=" + feedId).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GetLatestXFeeds_Result>(response); ;
        }
    }
}
