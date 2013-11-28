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

        public List<GetPriorityCategories_Result> GetCategories()
        {
            
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = client.GetAsync("GetCategories").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<GetPriorityCategories_Result>>(result);
        }

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


 
        public async Task<List<GetLatestXFeeds_Result>> GetLatestXFeedsFromId(int X, int Id)
        {
            
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync("GetLatestXFeeds/" + X.ToString() + "&" + Id.ToString());
 
         //   var result =  response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<GetLatestXFeeds_Result>>(response);
            
        }


            
        public bool LogIn(string usrName, string pw)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = client.GetAsync("LogIn/" + usrName + "&" + pw).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<bool>(result);
        
        }

        public GetHumanInformation_Result GetHumanInformation(int Id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = client.GetAsync("GetHumanInfo/" + Id.ToString()).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<GetHumanInformation_Result>(result);
        }

        public GetHumanInformationByUsername_Result GetHumanInformationByUserName(string username)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = client.GetAsync("GetHumanInfoByUsername/" + username).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<GetHumanInformationByUsername_Result>(result);
        }
        public List<GetFeedComments_Result> GetFeedComments(int feedId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = client.GetAsync("GetFeedComments/" + feedId).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<GetFeedComments_Result>>(result);
        }
        public List<GetFeedTags_Result> GetFeedTags(int feedId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = client.GetAsync("GetFeedTags/" + feedId).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<GetFeedTags_Result>>(result);
        }

        public int PublishFeed(int usrId, string text, string filepath, int prioId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = client.GetAsync("PostFeed/" + usrId.ToString() + "&" + text + "&" + filepath + "&" + prioId.ToString()).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var obj =  JsonConvert.DeserializeObject<string>(result);
            return int.Parse(obj);
        }

    }
}
