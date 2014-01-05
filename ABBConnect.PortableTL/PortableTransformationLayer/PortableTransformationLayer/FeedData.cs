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
using System.Collections.Specialized;

namespace PortableTransformationLayer
{
    /// <summary>
    /// Class that allow to make operation on the feeds like retrieve old feeds, or publish new feeds and comments
    /// </summary>
    public class FeedData: IFeedData
    {
        /// <summary>
        /// Attribute that provide the connection to the server
        /// </summary>
        private Connection urlServer;

        /// <summary>
        /// Constructor that automatically instantiate the attribute of the class
        /// </summary>
        public FeedData()
        {
            urlServer = new Connection();
        }

        /// <summary>
        /// Method that retrieve all the comments of a feed
        /// </summary>
        /// <param name="feedId">Integer representing the ID of the feed</param>
        /// <returns>Asynchronous operation that contain the List of comments</returns>
        public async Task<List<ABBConnectServiceRef.GetFeedComments_Result>> GetFeedComments(int feedId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetFeedComments?feedId=" + feedId + "&guid=" + Guid.NewGuid()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetFeedComments_Result>>(response);
        }

        /// <summary>
        /// Method that retrieve all human users referenced into a feed
        /// </summary>
        /// <param name="feedId">Integer representing the ID of the feed</param>
        /// <returns>Asynchronous operation that contain the List of tags</returns>
        public async Task<List<ABBConnectServiceRef.GetFeedTags_Result>> GetFeedTags(int feedId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetFeedTags?feedId=" + feedId).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetFeedTags_Result>>(response);
        }

        /// <summary>
        /// Method that store a feed from a human user
        /// </summary>
        /// <param name="usrId">Integer that represent the ID of a human user</param>
        /// <param name="text">String that represent the content of the feed</param>
        /// <param name="filepath">String that represent the the path </param>
        /// <param name="prioId">Integer that represent the priority level of the feed</param>
        /// <returns>Asynchronous operation that contain the ID of the feed</returns>
        public async Task<int> PublishFeed(int usrId, string text, string filepath, int prioId, byte[] image)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("PostFeed?id=" + usrId.ToString()
                + "&text=" + text + "&path=" + filepath + "&priority=" + prioId.ToString()).ConfigureAwait(false);
            var obj = JsonConvert.DeserializeObject<string>(response);

            if (image.Length != 0 || image != null)
            {
                await AddImageToFeed(image, obj);
            }
            //HttpWebResponse resp = await (HttpWebResponse)req.GetResponseAsync().ConfigureAwait(false);

            return int.Parse(obj);
        }

        private async Task AddImageToFeed(byte[] image, string obj)
        {
            string feedId = (int.Parse(obj)).ToString() + ";";
            byte[] filebytes = new byte[image.Length + feedId.Length * sizeof(char)];
            System.Buffer.BlockCopy(feedId.ToCharArray(), 0, filebytes, 0, feedId.Length * sizeof(char));
            System.Buffer.BlockCopy(image, 0, filebytes, feedId.Length * sizeof(char), image.Length);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(urlServer.UrlStream + "/SaveImage");
            req.Method = "POST";
            req.ContentType = "text/plain";
            Stream reqStream = await req.GetRequestStreamAsync().ConfigureAwait(false);
            reqStream.Write(filebytes, 0, filebytes.Length);
            reqStream.Dispose();
            await req.GetResponseAsync();
        }

        public async Task<int> PublishTestFeed2(int usrId, string text, string filepath, int prioId)
        {
            //using (WebClient client = new WebClient())
            //{
              //  byte[] response = client.UploadValues("http://dork.com/service", new NameValueCollection()
                //{
                  //  { "home", "Cosby" },
                    //{ "favorite+flavor", "flies" }
                //});
            //}
            //var client = new HttpClient();
            //client.BaseAddress = new Uri(urlServer.Url);
            //var response = await client.GetStringAsync("PostFeed?id=" + usrId.ToString()
             //   + "&text=" + text + "&path=" + filepath + "&priority=" + prioId.ToString()).ConfigureAwait(false);
            //var obj = JsonConvert.DeserializeObject<string>(response);
            //return int.Parse(obj);
            return -1;
        }

        public async Task<int> PublishTestFeed(int usrId, string text, byte[] fileArray, int prioId)
        {


            //HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(urlServer.Url + "PostFeed?id=" + usrId.ToString()
            //    + "&text=" + text + "&priority=" + prioId.ToString());
            //req.Method = "POST";
            //req.ContentType = "text/plain";
            //Stream reqStream = await req.GetRequestStreamAsync().ConfigureAwait(false);
            
            //byte[] fileToSend = new byte[5];
            //for (int i = 0; i < fileToSend.Length; i++)
            //{
            //    fileToSend[i] = (byte)('a' + (i % 26));
            //}
            //reqStream.Write(fileToSend, 0, fileToSend.Length);
            //reqStream.Dispose();

          

            //HttpWebResponse response = await (HttpWebResponse)req.GetResponseAsync();
            //var obj = JsonConvert.DeserializeObject<string>(response);
            //return int.Parse(obj);

            string filepath = Convert.ToBase64String(fileArray);
            //filepath = filepath.Remove(filepath.Length-1,1);
            //var client = new HttpClient();
            //client.BaseAddress = new Uri(urlServer.Url);


            WebRequest request = WebRequest.Create(urlServer.Url + "PostIt"); //create a request object
            request.Method = "POST"; //we will use POST (as opposed to GET)
            //request.ContentLength = postData.Length; //set the length of the request
            request.ContentType = "text/json"; //we will use Json data formatting
            using (Stream reqStream = await request.GetRequestStreamAsync()) {

                reqStream.Write(fileArray, 0, fileArray.Length); //send the input data to the web service

            }
            
            try
            {
                //var response = await client.PostAsync(urlServer.Url,sc).ConfigureAwait(false);
                //var obj = JsonConvert.DeserializeObject<HttpResponseMessage>(response);
                //return int.Parse(obj);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }
            
            
            return 1;
        }

        /// <summary>
        /// Method that search and retrieve the feeds by all their attributes.
        /// If an attribute of the feed is not needed in the search it can be leaved empty for strings, MinValue for DateTime objects, or -1 for integers.
        /// </summary>
        /// <param name="userId">Integer that represent the ID of the user, if not needed in the search put it -1</param>
        /// <param name="location">String that represent the location of the feed, if not needed in the search leave it empty</param>
        /// <param name="startingTime">Class that represent the date where the search begin, it must be older than the date where the search should stop;
        /// if  not needed in the search put it like MinValue</param>
        /// <param name="endingTime">Class that represent the date where the search end, it must be younger than the date where the search should start;
        /// if  not needed in the search put it like MinValue</param>
        /// <param name="feedType">String that represent the type of the feed: human or sensor;
        /// if not needed in the search put it like empty string</param>
        /// <param name="startId"></param>
        /// <param name="numFeeds">Integer that represent the number of feeds that must be retrieved, if not needed in the search put it -1</param>
        /// <returns>Asynchronous operation that contain the List of feeds required</returns>
        public async Task<List<GetLatestXFeeds_Result>> GetFeedsByFilter(int userId, string location, DateTime startingTime, DateTime endingTime, string feedType, int startId, int numFeeds)
        {
            string dateTimePattern = "yy-MM-dd H:mm:ss";
            string url = String.Format("FilterFeedsByFilter?userId={0}&location={1}&start={2}&end={3}&type={4}&feedId={5}&numFeeds={6}&guid={7}",
                                        userId == -1 ? "" : userId.ToString(),
                                        location,
                                        startingTime == DateTime.MinValue ? "" : startingTime.ToString(dateTimePattern),
                                        endingTime == DateTime.MinValue ? "" : endingTime.ToString(dateTimePattern),
                                        feedType,
                                        startId == -1 ? "" : startId.ToString(),
                                        numFeeds == -1 ? "" : numFeeds.ToString(),
                                        Guid.NewGuid());

            string backUp = "FilterFeedsByFilter?userId=" + userId
                + "&location=" + location + "&start=" + startingTime.ToString() + "&end=" +
                endingTime.ToString() + "&type=" + feedType + "&feedId=" + startId +
                "&numFeeds=" + numFeeds;

            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetLatestXFeeds_Result>>(response);
        }

        /// <summary>
        /// Method that store a comment made to a feed from a human user
        /// </summary>
        /// <param name="feedId">Integer that represent the ID of a feed</param>
        /// <param name="username">String that represent the username of the human user</param>
        /// <param name="comment">String that rapresent the content of the comment</param>
        /// <returns>Asynchronous operation that contain a boolean that indicate if the operation succeed</returns>
        public async Task<bool> PublishComment(int feedId, string username, string comment)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("PostComment?feedId=" + feedId +
                           "&username=" + username + "&comment=" + comment).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<bool>(response); ;
        }

        /// <summary>
        /// Method that the reference to a user into a feed
        /// </summary>
        /// <param name="feedId">ID of the feed where the reference should be added</param>
        /// <param name="username">Username of the the user that should be reference in the feed</param>
        /// <returns>Asynchronous operation that contain a boolean that indicate if the operation succeed</returns>
        public async Task<bool> AddFeedTag(int feedId, string username)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("AddTag?feedId=" + feedId +
                           "&username=" + username).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<bool>(response); ;
        }

        /// <summary>
        /// Method that retrieve the feed with a specified ID
        /// </summary>
        /// <param name="feedId">ID of the feed where the reference should be added</param>
        /// <returns>Asynchronous operation that contain the feed rewuired</returns>
        public async Task<GetLatestXFeeds_Result> GetFeedByFeedId(int feedId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetFeedByFeedId?feedId=" + feedId + "&guid=" + Guid.NewGuid()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GetLatestXFeeds_Result>(response); ;
        }


        public async Task<List<GetLatestXFeeds_Result>> GetFeedsFromLastShift(int numFeeds)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetFeedsFromLastShift?numFeeds=" + numFeeds + "&guid=" + Guid.NewGuid()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetLatestXFeeds_Result>>(response);
        }
    }
}
