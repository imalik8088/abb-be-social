using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;


/*
 * Connects to the JSON service hosted at remote server
 * Get the results in a async way.
 * IS used by Windows phoneetc.
 * 
 * Written by: Robert G
 * 2013-11-29
 * 
 * STUFF THAT NEEDS TO BE ADDED:
 * Fault codes when reciveing data
 * More methods
 * 
 */

namespace PortableTransformationLayer
{
    public class Accesser
    {
        private readonly string url = "http://83.255.84.243:85/ServiceJSON/ABBConnectWCF.svc/";
        List<GetLatestXFeeds_Result> ret;

        public Accesser(string url) 
        { 
            //this.url = url; 
        }
        public Accesser ()
	    {

        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetPriorityCategories_Result>> GetCategories()
        {
            
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync("GetCategories").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetPriorityCategories_Result>>(response);
        }

        /// <summary>
        /// Get the lastest feeds, X is the number of feeds
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        public async Task<List<GetLatestXFeeds_Result>> GetLatestXFeeds(int X)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync("GetLatestXFeeds/" + X.ToString()).ConfigureAwait(false);

            //   var result =  response.Content.ReadAsStringAsync().Result;
            List<GetLatestXFeeds_Result> ret = JsonConvert.DeserializeObject<List<GetLatestXFeeds_Result>>(response);
           
           // Task<List<GetLatestXFeeds_Result>> namn = ret;
           return ret;
        }


        /// <summary>
        /// Get the lastest feeds from specific Id, X is number of feeds
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<List<GetLatestXFeeds_Result>> GetLatestXFeedsFromId(int X, int Id)
        {
            
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync("GetLatestXFeeds/" + X.ToString() + "&" + Id.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetLatestXFeeds_Result>>(response);       
        }

        /// <summary>
        /// Log a user in to the system
        /// </summary>
        /// <param name="usrName"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
 
        public async Task<bool> LogIn(string usrName, string pw)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync("LogIn/" + usrName + "&" + pw).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<bool>(response);
        
        }

        /// <summary>
        /// The information about a user from his Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<GetHumanInformation_Result> GetHumanInformation(int Id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync("GetHumanInfo/" + Id.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GetHumanInformation_Result>(response);
        }

        /// <summary>
        ///  The information about a user from his username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<GetHumanInformationByUsername_Result> GetHumanInformationByUserName(string username)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync("GetHumanInfoByUsername/" + username).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GetHumanInformationByUsername_Result>(response);
        }

        /// <summary>
        /// Get comments that are attached to a feed
        /// </summary>
        /// <param name="feedId"></param>
        /// <returns></returns>
        public async  Task<List<GetFeedComments_Result>> GetFeedComments(int feedId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync("GetFeedComments/" + feedId).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetFeedComments_Result>>(response);
        }

        /// <summary>
        /// Get tags that are attached to a feed
        /// </summary>
        /// <param name="feedId"></param>
        /// <returns></returns>
        public async Task<List<GetFeedTags_Result>> GetFeedTags(int feedId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync("GetFeedTags/" + feedId).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetFeedTags_Result>>(response);
        }

        /// <summary>
        /// Publish a feed to the system
        /// </summary>
        /// <param name="usrId"></param>
        /// <param name="text"></param>
        /// <param name="filepath"></param>
        /// <param name="prioId"></param>
        /// <returns></returns>
        public async Task<int> PublishFeed(int usrId, string text, string filepath, int prioId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync("PostFeed/" + usrId.ToString() + "&" + text + "&" + filepath + "&" + prioId.ToString()).ConfigureAwait(false);
            var obj =  JsonConvert.DeserializeObject<string>(response);
            return int.Parse(obj);
        }

    }
}
