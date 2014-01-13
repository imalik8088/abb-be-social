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
    /// <summary>
    /// Class that allow to retrieve and store informations about users, distinguishing between human users and sensors
    /// </summary>
    public class UserData: IUserData
    {
        /// <summary>
        /// Attribute that provide the connection to the server
        /// </summary>
        private Connection urlServer;

        /// <summary>
        /// Constructor that automatically instantiate the attribute of the class
        /// </summary>
        public UserData()
        {
            urlServer = new Connection();
        }

        /// <summary>
        /// Method that retrieve the information of a sensor with a specified ID
        /// </summary>
        /// <param name="id">ID of the sensor that have to be retrieved</param>
        /// <returns>Asynchronous operation that contain the information about the sensor</returns>
        public async Task<GetSensorInformation_Result> GetSensorInformation(int id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetSensorInfo?id=" + id.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GetSensorInformation_Result>(response);
        }

        public async Task<List<ABBConnectServiceRef.GetHistoricalDataFromSensor_Result>> GetHistoricalDataFromSensor(int id, DateTime startingTime, DateTime endingTime)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetPastSensorData?id=" + id.ToString()
                + "&start=" + startingTime.ToString() + "&end=" + endingTime.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetHistoricalDataFromSensor_Result>>(response);
        }

        /// <summary>
        /// Method that retrieve the last value register from a sensor
        /// </summary>
        /// <param name="id">ID of the sensor that have to be retrieved</param>
        /// <returns>Asynchronous operation that contain the last value of the sensor</returns>
        public async Task<int> GetLastSensorValue(int id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetLastSensorValue?id=" + id.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<int>(response);
        }

        /// <summary>
        /// Method that check the credentials of a human user
        /// </summary>
        /// <param name="usrName">String that rappresent the username of a human user</param>
        /// <param name="pw">String that rappresent the password of a human user</param>
        /// <returns>Asynchronous operation that contain a boolean value representing the outcome of the operation</returns>
        public async Task<bool> LogIn(string usrName, string pw)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("LogIn?username="
                + usrName + "&password=" + pw).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<bool>(response);
        }

        /// <summary>
        /// Method that retrieve all the information of a human user
        /// </summary>
        /// <param name="Id">ID of the human user that have to be retrieved</param>
        /// <returns>Asynchronous operation that contain the human user information</returns>
        public async Task<ABBConnectServiceRef.GetHumanInformation_Result> GetHumanInformation(int Id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetHumanInfo?id=" + Id.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GetHumanInformation_Result>(response);
        }

        /// <summary>
        /// Method that retrieve all the information of a human user
        /// </summary>
        /// <param name="username">String that rappresent the username of the human user</param>
        /// <returns>Asynchronous operation that contain the human user informations</returns>
        public async Task<ABBConnectServiceRef.GetHumanInformationByUsername_Result> GetHumanInformationByUserName(string username)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetHumanInfoByUsername?username=" + username).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GetHumanInformationByUsername_Result>(response);
        }

        /// <summary>
        /// Method that retrieve all the information of a user, human or sensor
        /// </summary>
        /// <param name="query">String that rappresent the name of the user</param>
        /// <returns>Asynchronous operation that contain a List with the user informations</returns>
        public async Task<List<GetUsersByName_Result>> SearchUsersByName(string query)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("SearchByName?name=" + query).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetUsersByName_Result>>(response);
        }

        /// <summary>
        /// Method that retrieve all the saved filters option on the feeds of a specific user
        /// </summary>
        /// <param name="userId">String that rappresent the ID of the user</param>
        /// <returns>Asynchronous operation that contain the List with all the saved filters of a specific user</returns>
        public async Task<List<GetUserSavedFilters_Result>> GetUserSavedFilters(int userId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetUserFilters?userId=" + userId.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetUserSavedFilters_Result>>(response);
        }

        /// <summary>
        /// Method that retrieve all the users referenced in a specific filter
        /// </summary>
        /// <param name="filterId">String that rappresent the ID of the filter</param>
        /// <returns>Asynchronous operation that contain the List referenced in the filter</returns>
        public async Task<List<GetUserSavedFiltersTagedUsers_Result>> GetFilterTaggedUsers(int filterId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetFilterTaggedUsers?filterId=" + filterId.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetUserSavedFiltersTagedUsers_Result>>(response);
        }

        /// <summary>
        /// Method that save a filter option
        /// </summary>
        /// <param name="userId">String that rappresent the ID of the user that set the filter option</param>
        /// <param name="filterName">String that rappresent the name of the filter</param>
        /// <param name="startingTime">Class DateTime that rappresent the starting date where the filter option start</param>
        /// <param name="endingTime">Class DateTime that rappresent the ending date where the filter option end</param>
        /// <param name="location">String that rappresent the location where the filtering option is applied</param>
        /// <param name="feedType">String that rappresent the type of the feed that could be human or sensor</param>
        /// <returns>Asynchronous operation that contain the ID of the filter</returns>
        public async Task<int> AddFilter(int userId, string filterName, DateTime startingTime, DateTime endingTime, string location, string feedType)
        {
            string dateTimePattern = "yy-MM-dd H:mm:ss";
            string url = String.Format("SaveFilter?userId={0}&name={1}&start={2}&end={3}&location={4}&feedType={5}",
                                        userId.ToString(),
                                        location,
                                        startingTime == DateTime.MinValue ? "" : startingTime.ToString(dateTimePattern),
                                        endingTime == DateTime.MinValue ? "" : endingTime.ToString(dateTimePattern),
                                        feedType);

            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<int>(response);
        }

        /// <summary>
        /// Method that save the reference to a user in a specific filter
        /// </summary>
        /// <param name="userId">String that rappresent the ID of the user that will be referenced in the filter option</param>
        /// <param name="filterId">String that rappresent the ID of the filter option</param>
        /// <returns>Asynchronous operation containing the outcome of the operation</returns>
        public async Task<bool> AddUserToFilter(int userId, int filterId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("TagUserInFilter?userId=" + userId + "&filterId=" + filterId).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<bool>(response);
        }

        /// <summary>
        /// Method that save the reference between a sensor and a human user
        /// </summary>
        /// <param name="humanUserId">String that rappresent the ID of the human user</param>
        /// <param name="sensorUserId">String that rappresent the ID of the sensor</param>
        /// <returns>Asynchronous operation containing a boolean that represent the outcome of the operation</returns>
        public async Task<bool> FollowSensor(int humanUserId, int sensorUserId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("FollowSensor?humanId=" + humanUserId.ToString()
                                                        + "&sensorId=" + sensorUserId.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<bool>(response);
        }

        /// <summary>
        /// Method that retrieve the ID of the sensors referenced by a human user
        /// </summary>
        /// <param name="humanUserId">String that rappresent the ID of the human user</param>
        /// <returns>Asynchronous operation containing a List of all the sensors followed by the human user</returns>
        public async Task<List<int>> GetFollowedSensors(int humanUserId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetFollowedSensors?humanUserId=" + humanUserId.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<int>>(response);
        }

        /// <summary>
        /// Method that retrieve the activities of a specific user.
        /// The activity could be like make a comment, or a feed, or a reference to another user
        /// </summary>
        /// <param name="userId">String that rappresent the ID of the human user</param>
        /// <returns>Asynchronous operation containing a List of activities</returns>
        public async Task<List<GetUserActivity_Result>> GetUserActivity(int userId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetUserActivity?userId=" + userId.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetUserActivity_Result>>(response);
        }

        /// <summary>
        /// Method that delete the reference between a sensor and a human user
        /// </summary>
        /// <param name="humanUserId">String that rappresent the ID of the human user</param>
        /// <param name="sensorUserId">String that rappresent the ID of the sensor user</param>
        /// <returns>Asynchronous operation containing a boolean that represent the outcome of the operation</returns>
        public async Task<bool> UnfollowSensor(int humanUserId, int sensorUserId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("UnfollowSensor?humanUserId=" + humanUserId.ToString() 
                                                        + "&sensorUserId=" + sensorUserId.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<bool>(response);
        }


        public async Task<List<GetUserActivity_Result>> GetUserActivityFromId(int userId, int numActivities, int startId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url); 
            var response = await client.GetStringAsync("GetUserActivityFromId?userId=" + userId.ToString()
                                                        + "&numberOfActivities=" + numActivities.ToString()
                                                        + "&startId=" + startId.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetUserActivity_Result>>(response);
        }
    }
}
