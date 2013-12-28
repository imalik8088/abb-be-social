using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using DAL;

namespace ABBJSONService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ABBConnectWCF : IABBConnectWCF
    {
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "LogIn?username={username}&password={password}")]
        public bool LogIn(string username, string password)
        {
            UserData userData = new UserData();
            return userData.LogIn(username, password);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHumanInfo?id={id}")]
        public DAL.GetHumanInformation_Result GetHumanInformation(string id)
        {
            UserData userData = new UserData();
            return userData.GetHumanInformation(Int32.Parse(id));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHumanInfoByUsername?username={username}")]
        public DAL.GetHumanInformationByUsername_Result GetHumanInformationByUsername(string username)
        {
            UserData userData = new UserData();
            return userData.GetHumanInformationByUsername(username);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLatestXFeeds?num={x}")]
        List<GetLatestXFeeds_Result> IABBConnectWCF.GetLatestXFeeds(string X)
        {
            List<GetLatestXFeeds_Result> feeds = new List<GetLatestXFeeds_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetLatestXFeeds";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@X", SqlDbType.Int).Value = int.Parse(X);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetLatestXFeeds_Result feed = new GetLatestXFeeds_Result();
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            feed.FilePath = (string)reader[5] != null ? (string)reader[5] : "";
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];

                            feeds.Add(feed);
                        }
                    }
                }
                sqlConn.Close();
            }
            return feeds;
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLatestXFeedsFromId?num={x}&id={id}")]
        List<GetLatestXFeedsFromId_Result> IABBConnectWCF.GetLatestXFeedsFromId(string X, string Id)
        {
            List<GetLatestXFeedsFromId_Result> feeds = new List<GetLatestXFeedsFromId_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetLatestXFeedsFromId";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@X", SqlDbType.NVarChar).Value = X;
                    cmd.Parameters.Add("@feedID", SqlDbType.NVarChar).Value = Id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetLatestXFeedsFromId_Result feed = new GetLatestXFeedsFromId_Result();
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            feed.FilePath = (string)reader[5] != null ? (string)reader[5] : "";
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];

                            feeds.Add(feed);
                        }
                    }
                }
                sqlConn.Close();
            }
            return feeds;
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PostFeed?id={id}&text={text}&path={filepath}&priority={prioID}")]
        public int PostFeed(string id, string text, string filepath, string prioId)
        {
            FeedData feedData = new FeedData();
            return feedData.PostFeed(Int32.Parse(id), text, filepath, Int32.Parse(prioId));
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PostTestFeed?id={id}&text={text}&priority={prioID}&file={filepath}")]
        public int PostTestFeed(string id, string text, string prioId, string filepath)
        {
            int result = 0;
            //string filepath = Convert.ToBase64String(imageData);

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "AddFeed";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter("@retFeedId", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@text", SqlDbType.NVarChar).Value = text;
                    cmd.Parameters.Add("@filePath", SqlDbType.NVarChar).Value = filepath;
                    cmd.Parameters.Add("@feedPriorityId", SqlDbType.Int).Value = prioId;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();

                    result = (int)cmd.Parameters["@retFeedId"].Value;
                }
                sqlConn.Close();
            }
            return result;
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCategories")]
        public List<DAL.GetPriorityCategories_Result> GetCategories()
        {
            CommonData categoriesData = new CommonData();
            return categoriesData.GetCategories();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSensors")]
        public List<DAL.GetAllSensors_Result> GetAllSensors()
        {
            UserData userData = new UserData();
            return userData.GetAllSensors();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeedComments?feedId={feedId}&guid={randomGuid}")]
        public List<DAL.GetFeedComments_Result> GetFeedComments(string feedId, string randomGuid)
        {
            FeedData feedData = new FeedData();
            return feedData.GetFeedComments(Int32.Parse(feedId));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeedTags?feedId={feedId}")]
        public List<DAL.GetFeedTags_Result> GetFeedTags(string feedId)
        {
            FeedData feedData = new FeedData();
            return feedData.GetFeedTags(Int32.Parse(feedId));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHumanFeeds")]
        List<DAL.GetAllHumanFeeds_Result> IABBConnectWCF.GetHumanFeeds()
        {
            FeedData feedData = new FeedData();
            return feedData.GetHumanFeeds();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHumanFeedsByFilter?location={location}&start={startingTime}&end={endingTime}")]
        List<DAL.GetAllHumanFeedsByFilter_Result> IABBConnectWCF.GetHumanFeedsByFilter(string location, string startingTime, string endingTime)
        {
            FeedData feedData = new FeedData();
            return feedData.GetHumanFeedsByFilter(location, Convert.ToDateTime(startingTime), Convert.ToDateTime(endingTime));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSensorFeeds")]
        List<DAL.GetAllSensorFeeds_Result> IABBConnectWCF.GetSensorFeeds()
        {
            FeedData feedData = new FeedData();
            return feedData.GetSensorFeeds();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSensorFeedsByFilter?location={location}&start={startingTime}&end={endingTime}")]
        List<DAL.GetAllSensorFeedsByFilter_Result> IABBConnectWCF.GetSensorFeedsByFilter(string location, string startingTime, string endingTime)
        {
            FeedData feedData = new FeedData();
            return feedData.GetSensorFeedsByFilter(location, Convert.ToDateTime(startingTime), Convert.ToDateTime(endingTime));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserFeeds")]
        List<DAL.GetUserFeeds_Result> IABBConnectWCF.GetUserFeeds()
        {
            FeedData feedData = new FeedData();
            return feedData.GetUserFeeds();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserFeedsByFilter?location={location}&start={startingTime}&end={endingTime}")]
        List<DAL.GetUserFeedsByFilter_Result> IABBConnectWCF.GetUserFeedsByFilter(string location, string startingTime, string endingTime)
        {
            FeedData feedData = new FeedData();
            return feedData.GetUserFeedsByFilter(location, Convert.ToDateTime(startingTime), Convert.ToDateTime(endingTime));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLocations")]
        List<string> IABBConnectWCF.GetLocations()
        {
            CommonData locationData = new CommonData();
            return locationData.GetLocations();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSensorInfo?id={id}")]
        public DAL.GetSensorInformation_Result GetSensorInformation(string id)
        {
            UserData userData = new UserData();
            return userData.GetSensorInformation(Int32.Parse(id));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetPastSensorData?id={id}&start={startingTime}&end={endingTime}")]
        List<DAL.GetHistoricalDataFromSensor_Result> IABBConnectWCF.GetHistoricalDataFromSensor(string id, string startingTime, string endingTime)
        {
            UserData sensorHistory = new UserData();
            return sensorHistory.GetHistoricalDataFromSensor(Int32.Parse(id), Convert.ToDateTime(startingTime), Convert.ToDateTime(endingTime));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLastSensorValue?id={id}")]
        public int GetLastSensorValue(string id)
        {
            UserData sensorVal = new UserData();
            return sensorVal.GetLastSensorValue(Int32.Parse(id));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "PostComment?feedId={feedId}&username={username}&comment={text}")]
        public bool PostComment(string feedId, string username, string text)
        {
            FeedData feedData = new FeedData();
            return feedData.PostComment(Int32.Parse(feedId), username, text);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "AddTag?feedId={feedId}&username={username}")]
        public bool AddTag(string feedId, string username)
        {
            FeedData feedData = new FeedData();
            return feedData.AddTag(Int32.Parse(feedId), username);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "FollowSensor?humanId={humanId}&sensorId={sensorId}")]
        public bool FollowSensor(string humanId, string sensorId)
        {
            UserData userData = new UserData();
            return userData.FollowSensor(Int32.Parse(humanId), Int32.Parse(sensorId));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FilterLatestFeeds?location={location}&start={startingTime}&end={endingTime}")]
        List<DAL.GetLatestFeedsByFilter_Result> IABBConnectWCF.GetLatestFeedsByFilter(string location, string startingTime, string endingTime)
        {
            FeedData feedData = new FeedData();
            return feedData.GetLatestFeedsByFilter(location, Convert.ToDateTime(startingTime), Convert.ToDateTime(endingTime));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FilterFeeds?name={name}&location={location}&start={startingTime}&end={endingTime}&type={feedType}")]
        List<DAL.GetFeedsByFilter_Result> IABBConnectWCF.GetFeedsByFilter(string name, string location, string startingTime, string endingTime, string feedType)
        {
            FeedData feedData = new FeedData();
            return feedData.GetFeedsByFilter(name, location, Convert.ToDateTime(startingTime), Convert.ToDateTime(endingTime), feedType);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FilterFeedsByFilter?userId={id}&location={location}&start={startingTime}&end={endingTime}&type={feedType}&feedId={startId}&numFeeds={numFeeds}&guid={randomGuid}")]
        List<DAL.GetLatestXFeeds_Result> IABBConnectWCF.GetXFeedsByFilter(string id, string location, string startingTime, string endingTime, string feedType, string startId, string numFeeds, string randomGuid)
        {
            FeedData feedData = new FeedData();

            DateTime startTime = DateTime.MinValue;

            if (!startingTime.Equals(""))
                startTime = Convert.ToDateTime(startingTime);

            DateTime endTime = DateTime.MinValue;

            if (!endingTime.Equals(""))
                endTime = Convert.ToDateTime(endingTime);

            int startingId = -1;

            if (!startId.Equals(""))
                startingId = Int32.Parse(startId);

            int feedsNumber = -1;

            if (!numFeeds.Equals(""))
                feedsNumber = Int32.Parse(numFeeds);

            int userId = -1;

            if (!id.Equals(""))
                userId = Int32.Parse(id);

            return feedData.GetXFeedsByFilter(userId, location, startTime,
                                            endTime, feedType, startingId, feedsNumber);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveFilter?userId={userId}&name={filterName}&start={startingTime}&end={endingTime}&location={location}&feedType={feedType}")]
        public int SaveFilter(string userId, string filterName, string startingTime, string endingTime, string location, string feedType)
        {
            UserData userData = new UserData();
            return userData.SaveFilter(Int32.Parse(userId), filterName, Convert.ToDateTime(startingTime), Convert.ToDateTime(endingTime), location, feedType);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "TagUserInFilter?userId={userId}&filterId={filterId}")]
        public bool AddFilterUser(string userId, string filterId)
        {
            UserData userData = new UserData();
            return userData.AddFilterUser(Int32.Parse(userId), Int32.Parse(filterId));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserFilters?userId={userId}")]
        public List<DAL.GetUserSavedFilters_Result> GetSavedFilter(string userId)
        {
            UserData userData = new UserData();
            return userData.GetSavedFilter(Int32.Parse(userId));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SearchByName?name={query}")]
        List<DAL.GetUsersByName_Result> IABBConnectWCF.SearchUsersByName(string query)
        {
            UserData userData = new UserData();
            return userData.SearchUsersByName(query);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFilterTaggedUsers?filterId={filterId}")]
        public List<DAL.GetUserSavedFiltersTagedUsers_Result> GetFilterTaggedUsers(string filterId)
        {
            UserData userData = new UserData();
            return userData.GetFilterTaggedUsers(Int32.Parse(filterId));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserActivity?userId={userId}")]
        public List<DAL.GetUserActivity_Result> GetUserActivity(string userId)
        {
            UserData userData = new UserData();
            return userData.GetUserActivity(Int32.Parse(userId));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UnfollowSensor?humanUserId={humanUserId}&sensorUserId={sensorUserId}")]
        public bool UnfollowSensor(string humanUserId, string sensorUserId)
        {
            UserData userData = new UserData();
            return userData.UnfollowSensor(Int32.Parse(humanUserId), Int32.Parse(sensorUserId));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFollowedSensors?humanUserId={humanUserId}")]
        public List<int> GetFollowedSensors(string humanUserId)
        {
            UserData userData = new UserData();
            return userData.GetFollowedSensors(Int32.Parse(humanUserId));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeedByFeedId?feedId={feedId}&guid={randomGuid}")]
        public DAL.GetLatestXFeeds_Result GetFeedByFeedId(string feedId, string randomGuid)
        {
            FeedData feedData = new FeedData();
            return feedData.GetFeedByFeedId(Int32.Parse(feedId));
        }
    }
}

