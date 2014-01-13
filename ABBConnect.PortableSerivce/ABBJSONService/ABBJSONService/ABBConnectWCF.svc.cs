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
        /// <summary>
        /// Method that retrieves the credentials of a human user through the url and checks them
        /// </summary>
        /// <param name="username">String that represent the username of a human user</param>
        /// <param name="password">String that represent the password of a human user</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "LogIn?username={username}&password={password}")]
        public bool LogIn(string username, string password)
        {
            UserData userData = new UserData();
            return userData.LogIn(username, password);
        }

        /// <summary>
        /// Method that returns the information of the human identified with the id of the provided url
        /// </summary>
        /// <param name="id">human user identifier</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHumanInfo?id={id}")]
        public DAL.GetHumanInformation_Result GetHumanInformation(string id)
        {
            UserData userData = new UserData();
            return userData.GetHumanInformation(Int32.Parse(id));
        }

        /// <summary>
        /// Method that returns the information of the human identified with the username provided from the url
        /// </summary>
        /// <param name="username">the username of the human</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHumanInfoByUsername?username={username}")]
        public DAL.GetHumanInformationByUsername_Result GetHumanInformationByUsername(string username)
        {
            UserData userData = new UserData();
            return userData.GetHumanInformationByUsername(username);
        }

        /// <summary>
        /// Method used to save a feed in the database
        /// </summary>
        /// <param name="id">the id of the user writing the post</param>
        /// <param name="text">the content of the post</param>
        /// <param name="filepath">the filepath of the image</param>
        /// <param name="prioId">the priority identifier</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PostFeed?id={id}&text={text}&path={filepath}&priority={prioID}")]
        public int PostFeed(string id, string text, string filepath, string prioId)
        {
            FeedData feedData = new FeedData();
            return feedData.PostFeed(Int32.Parse(id), text, filepath, Int32.Parse(prioId));
        }

        /// <summary>
        /// Method used to retrieve the feed gategories stored into the database
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCategories")]
        public List<DAL.GetPriorityCategories_Result> GetCategories()
        {
            CommonData categoriesData = new CommonData();
            return categoriesData.GetCategories();
        }

        /// <summary>
        /// Method used to retrieve all the sensors stored in the database
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSensors")]
        public List<DAL.GetAllSensors_Result> GetAllSensors()
        {
            UserData userData = new UserData();
            return userData.GetAllSensors();
        }

        /// <summary>
        /// Method used to retrieve all the comments posted for a specific feed.
        /// </summary>
        /// <param name="feedId">the feed identifier, whose comments are returned</param>
        /// <param name="randomGuid">a random value passed to trick the server and request new data and not cached</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeedComments?feedId={feedId}&guid={randomGuid}")]
        public List<DAL.GetFeedComments_Result> GetFeedComments(string feedId, string randomGuid)
        {
            FeedData feedData = new FeedData();
            return feedData.GetFeedComments(Int32.Parse(feedId));
        }

        /// <summary>
        /// Method that returns all the tags related to a specific feed.
        /// </summary>
        /// <param name="feedId">the feed identifier, whose tags are returned</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeedTags?feedId={feedId}")]
        public List<DAL.GetFeedTags_Result> GetFeedTags(string feedId)
        {
            FeedData feedData = new FeedData();
            return feedData.GetFeedTags(Int32.Parse(feedId));
        }

        /// <summary>
        /// Method used to retrieve all the work locations stored in the database
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLocations")]
        List<string> IABBConnectWCF.GetLocations()
        {
            CommonData locationData = new CommonData();
            return locationData.GetLocations();
        }

        /// <summary>
        /// Method used to retrieve the profile information of the sensor identified with the id of the provided url
        /// </summary>
        /// <param name="id">sensor user identifier</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSensorInfo?id={id}")]
        public DAL.GetSensorInformation_Result GetSensorInformation(string id)
        {
            UserData userData = new UserData();
            return userData.GetSensorInformation(Int32.Parse(id));
        }

        /// <summary>
        /// Method used to retrieve previous sensor raw data stored in the specified time range
        /// </summary>
        /// <param name="id">sensor user identifier</param>
        /// <param name="startingTime">the starting time of the time period</param>
        /// <param name="endingTime">the ending time of the time period</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetPastSensorData?id={id}&start={startingTime}&end={endingTime}")]
        List<DAL.GetHistoricalDataFromSensor_Result> IABBConnectWCF.GetHistoricalDataFromSensor(string id, string startingTime, string endingTime)
        {
            UserData sensorHistory = new UserData();
            return sensorHistory.GetHistoricalDataFromSensor(Int32.Parse(id), Convert.ToDateTime(startingTime), Convert.ToDateTime(endingTime));
        }

        /// <summary>
        /// Method that retrieves the current raw value of a sensor
        /// </summary>
        /// <param name="id">the sensor user identifier</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLastSensorValue?id={id}")]
        public int GetLastSensorValue(string id)
        {
            UserData sensorVal = new UserData();
            return sensorVal.GetLastSensorValue(Int32.Parse(id));
        }

        /// <summary>
        /// Method used to receive all the comment details through the url get parameters and store it in the database.
        /// </summary>
        /// <param name="feedId">the feed identifier, in which the comment will be stored</param>
        /// <param name="username">the username of the user that comments</param>
        /// <param name="text">the content of the comment</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "PostComment?feedId={feedId}&username={username}&comment={text}")]
        public bool PostComment(string feedId, string username, string text)
        {
            FeedData feedData = new FeedData();
            return feedData.PostComment(Int32.Parse(feedId), username, text);
        }

        /// <summary>
        /// Method used to tag a user to a feed.
        /// </summary>
        /// <param name="feedId">the feed identifier, in which the tag will be included</param>
        /// <param name="username">the username of the user that is going to be tagged</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "AddTag?feedId={feedId}&username={username}")]
        public bool AddTag(string feedId, string username)
        {
            FeedData feedData = new FeedData();
            return feedData.AddTag(Int32.Parse(feedId), username);
        }

        /// <summary>
        /// Method used to receive the identifiers through the url and connect a human user with a sensor user. This way the human can get live update of a sensor's values
        /// </summary>
        /// <param name="humanId">the human identifier of the human user that will follow the specific sensor</param>
        /// <param name="sensorId">the sensor identifier of the sensor that the specific human will follow</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "FollowSensor?humanId={humanId}&sensorId={sensorId}")]
        public bool FollowSensor(string humanId, string sensorId)
        {
            UserData userData = new UserData();
            return userData.FollowSensor(Int32.Parse(humanId), Int32.Parse(sensorId));
        }

        /// <summary>
        /// Method that search and retrieve the feeds by all their attributes.
        /// If an attribute of the feed is not needed in the search it can be leaved empty as empty string.
        /// </summary>
        /// <param name="id">represents the ID of the user, whose feeds will be returned</param>
        /// <param name="location">represents the work location, from where the feed was posted</param>
        /// <param name="startingTime">defines the starting point of a time period</param>
        /// <param name="endingTime">defines the ending point of a time period</param>
        /// <param name="feedType">defines the feed type that should be returned (Human, Sensor or both)</param>
        /// <param name="categoryName">defines the category of the feeds to be returned</param>
        /// <param name="startId"></param>
        /// <param name="numFeeds">the number of feeds to be returned</param>
        /// <param name="randomGuid">a random value passed to trick the server and request new data and not cached</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FilterFeedsByFilter?userId={id}&location={location}&start={startingTime}&end={endingTime}&type={feedType}&category={categoryName}&feedId={startId}&numFeeds={numFeeds}&guid={randomGuid}")]
        List<DAL.GetLatestXFeeds_Result> IABBConnectWCF.GetXFeedsByFilter(string id, string location, string startingTime, string endingTime, string feedType, string categoryName, string startId, string numFeeds, string randomGuid)
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
                                            endTime, feedType, categoryName, startingId, feedsNumber);
        }

        /// <summary>
        /// Method that saves a filter option, by saving whicever data is received through the url. This means that not all the data are required.
        /// </summary>
        /// <param name="userId">String that represent the ID of the user that set the filter option</param>
        /// <param name="filterName">String that represent the name of the filter</param>
        /// <param name="startingTime">DateTime that represent the starting date where the filter option start</param>
        /// <param name="endingTime">DateTime that represent the ending date where the filter option end</param>
        /// <param name="location">String that represent the location where the filtering option is applied</param>
        /// <param name="feedType">String that represent the type of the feed that could be human or sensor</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveFilter?userId={userId}&name={filterName}&start={startingTime}&end={endingTime}&location={location}&feedType={feedType}")]
        public int SaveFilter(string userId, string filterName, string startingTime, string endingTime, string location, string feedType)
        {
            UserData userData = new UserData();
            return userData.SaveFilter(Int32.Parse(userId), filterName, Convert.ToDateTime(startingTime), Convert.ToDateTime(endingTime), location, feedType);
        }

        /// <summary>
        /// Method that save the reference to a user in a specific filter
        /// </summary>
        /// <param name="userId">String that rappresent the ID of the user that will be referenced in the filter option</param>
        /// <param name="filterId">String that rappresent the ID of the filter option</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "TagUserInFilter?userId={userId}&filterId={filterId}")]
        public bool AddFilterUser(string userId, string filterId)
        {
            UserData userData = new UserData();
            return userData.AddFilterUser(Int32.Parse(userId), Int32.Parse(filterId));
        }

        /// <summary>
        /// Method used to retrieve all the filters that a user has saved
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserFilters?userId={userId}")]
        public List<DAL.GetUserSavedFilters_Result> GetSavedFilter(string userId)
        {
            UserData userData = new UserData();
            return userData.GetSavedFilter(Int32.Parse(userId));
        }

        /// <summary>
        /// Method used to search all the users of the database by the string provided as a query from the url
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SearchByName?name={query}")]
        List<DAL.GetUsersByName_Result> IABBConnectWCF.SearchUsersByName(string query)
        {
            UserData userData = new UserData();
            return userData.SearchUsersByName(query);
        }

        /// <summary>
        /// Method used to receive the users who are considered as tagged in a filter
        /// </summary>
        /// <param name="filterId">the identifier of the filter</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFilterTaggedUsers?filterId={filterId}")]
        public List<DAL.GetUserSavedFiltersTagedUsers_Result> GetFilterTaggedUsers(string filterId)
        {
            UserData userData = new UserData();
            return userData.GetFilterTaggedUsers(Int32.Parse(filterId));
        }

        /// <summary>
        /// Method used to retrieve all the user activity from the user identified by the provided in the url id
        /// </summary>
        /// <param name="userId">the identifier of the user, whose activity will be returned</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserActivity?userId={userId}")]
        public List<DAL.GetUserActivity_Result> GetUserActivity(string userId)
        {
            UserData userData = new UserData();
            return userData.GetUserActivity(Int32.Parse(userId));
        }

        /// <summary>
        /// Method used to unrelate a human user from a sensor user
        /// </summary>
        /// <param name="humanUserId">the human identifier of the human user that will unfollow the specific sensor</param>
        /// <param name="sensorUserId">the sensor identifier of the sensor that the specific human will unfollow</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UnfollowSensor?humanUserId={humanUserId}&sensorUserId={sensorUserId}")]
        public bool UnfollowSensor(string humanUserId, string sensorUserId)
        {
            UserData userData = new UserData();
            return userData.UnfollowSensor(Int32.Parse(humanUserId), Int32.Parse(sensorUserId));
        }

        /// <summary>
        /// Method that returns all the sensors that a user is currently following
        /// </summary>
        /// <param name="humanUserId">the human user identifier</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFollowedSensors?humanUserId={humanUserId}")]
        public List<int> GetFollowedSensors(string humanUserId)
        {
            UserData userData = new UserData();
            return userData.GetFollowedSensors(Int32.Parse(humanUserId));
        }

        /// <summary>
        /// Method used to retrieve all the information of just a specific feed from the database.
        /// </summary>
        /// <param name="feedId">the feed identifier of the requested feed</param>
        /// <param name="randomGuid">a random value passed to trick the server and request new data and not cached</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeedByFeedId?feedId={feedId}&guid={randomGuid}")]
        public DAL.GetLatestXFeeds_Result GetFeedByFeedId(string feedId, string randomGuid)
        {
            FeedData feedData = new FeedData();
            return feedData.GetFeedByFeedId(Int32.Parse(feedId));
        }

        /// <summary>
        /// Method used to receive a specific number of feeds, which were posted during the previous shift (previous 8 hours of some predefined shift scedules)
        /// </summary>
        /// <param name="numFeeds">the number of the feeds to be returned</param>
        /// <param name="randomGuid">a random value passed to trick the server and request new data and not cached</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeedsFromLastShift?numFeeds={numFeeds}&guid={randomGuid}")]
        public List<DAL.GetLatestXFeeds_Result> GetFeedsFromLastShift(string numFeeds, string randomGuid)
        {
            FeedData feedData = new FeedData();

            int feedsNumber = -1;

            if (!numFeeds.Equals(""))
                feedsNumber = Int32.Parse(numFeeds);

            return feedData.GetFeedsFromLastShift(feedsNumber);
        }

        /// <summary>
        /// Method used to get the a specific number of activities related to a user. The returned activities begin from a specified id.
        /// </summary>
        /// <param name="userId">the user identifier of the user whose activity is requested</param>
        /// <param name="numberOfActivities">the number of the returned activities</param>
        /// <param name="startId">the identifier acting as a starting point in the requested activities</param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserActivityFromId?userId={userId}&numberOfActivities={numberOfActivities}&startId={startId}")]
        public List<DAL.GetUserActivity_Result> GetUserActivityFromId(string userId, string numberOfActivities, string startId)
        {
            UserData userData = new UserData();

            int activitiesNumber = -1;

            if (!numberOfActivities.Equals(""))
                activitiesNumber = Int32.Parse(numberOfActivities);

            int startingId = -1;

            if (!startId.Equals(""))
                startingId = Int32.Parse(startId);

            return userData.GetUserActivityFromId(Int32.Parse(userId), activitiesNumber, startingId);
        }
    }
}

