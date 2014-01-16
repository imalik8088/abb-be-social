using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace PortableBLL
{
    /// <summary>
    /// Class that allow to retrieve informations about users, distinguishing between human users and sensors
    /// </summary>
    public class UserManager : IUserManager
    {
        /// <summary>
        /// attribute that allow to retrieve informations about human users and sensors
        /// </summary>
        private UserData usrData;

        /// <summary>
        /// Constructor that instantiate the attribute of the class
        /// </summary>
        public UserManager()
        {
            usrData = new UserData();
        }

        /// <summary>
        /// This method take the information of a specific sensor
        /// </summary>
        /// <param name="sensorID">integer that identify the sensor</param>
        /// <returns>all the informations about a sensor</returns>
        public async Task<Sensor> LoadSensorInformation(int sensorID)
        {
            GetSensorInformation_Result tempSensor = await usrData.GetSensorInformation(sensorID).ConfigureAwait(false);
            Sensor responseSensor = new Sensor(tempSensor);
            return responseSensor;
        }

        /// <summary>
        /// This method take the historical information of a specific sensor in a specific period
        /// </summary>
        /// <param name="sensorID">integer that identify the sensor</param>
        /// <param name="startingTime">starting time of the requested history</param>
        /// <param name="endingTime">ending time of the requested history</param>
        /// <returns></returns>
        public async Task<SensorHistoryData> LoadHistoryValuesBySensor(int sensorID, DateTime startingTime, DateTime endingTime)
        {
            List<GetHistoricalDataFromSensor_Result> listHistData = await usrData.GetHistoricalDataFromSensor(sensorID, startingTime, endingTime);

            UserManager senInfoMng = new UserManager();
 
            SensorHistoryData senHistData = new SensorHistoryData(await senInfoMng.LoadSensorInformation(sensorID));
            senHistData.StartingTime = startingTime;
            senHistData.EndingTime = endingTime;

            foreach (GetHistoricalDataFromSensor_Result res in listHistData)
            {
                SensorVTData sensorData = new SensorVTData();

                int rawInt;
	            bool convertRes = int.TryParse(res.RawValue, out rawInt);
	            if (convertRes == true)
	            {
                    sensorData.RawData = rawInt;
	            }

                sensorData.CreationTime = res.CreationTimeStamp ?? DateTime.Now; 
                senHistData.Owner.SensorValues.Add(sensorData);
            }

            return senHistData;
        }

        /// <summary>
        /// This method get the last value of a specific sensor
        /// </summary>
        /// <param name="sensorID">integer that identify the sensor</param>
        /// <returns>integer that rappresent the last sensor value</returns>
        public async Task<int> LoadCurrentValuesBySensor(int sensorID)
        {
            return await usrData.GetLastSensorValue(sensorID).ConfigureAwait(false);
        }

        /// <summary>
        /// This method allow to log into the system, if the credential of a registered user are right
        /// </summary>
        /// <param name="userName">string that rappresent the username of the user</param>
        /// <param name="password">string that rappresent the password of the user</param>
        /// <returns>A boolean value that comunicate if the login succeded or fail</returns>
        public async Task<bool> Login(string userName, string password)
        {
            return await usrData.LogIn(userName, password).ConfigureAwait(false); ;

        }

        /// <summary>
        /// This method take the information of a specific user 
        /// </summary>
        /// <param name="humandId">integer that identify the user</param>
        /// <returns>all the informations about a user</returns>
        public async Task<Human> LoadHumanInformation(int humanId)
        {
            return new Human(await usrData.GetHumanInformation(humanId).ConfigureAwait(false), humanId);
        }

        /// <summary>
        /// This method take the information of a specific user 
        /// </summary>
        /// <param name="username">string that rappresent the username used by the user</param>
        /// <returns>all the informations about a user</returns>
        public async Task<Human> LoadHumanInformationByUsername(string username)
        {
            return new Human(await usrData.GetHumanInformationByUserName(username).ConfigureAwait(false));
        }

        /// <summary>
        /// This method search all the users with the name given in input
        /// </summary>
        /// <param name="query">name of the user that should be searched</param>
        /// <returns>list of users with that name</returns>
        public async Task<List<User>> SearchUserByName(string query)
        {
            List<GetUsersByName_Result> list = await usrData.SearchUsersByName(query).ConfigureAwait(false);

            List<User> retList = new List<User>();

            foreach (GetUsersByName_Result res in list)
                if (res.isHuman)
                {
                    retList.Add(new Human(res));
                }
                else
                {
                    retList.Add(new Sensor(res));
                }

            return retList;
        }


        /// <summary>
        /// This method rietreve all the human users 
        /// </summary>
        /// <returns>a list with all the human users</returns>
        public async Task<List<Human>> GetAllHumanUsers()
        {
            List<GetUsersByName_Result> list = await usrData.SearchUsersByName("").ConfigureAwait(false);

            List<Human> retList = new List<Human>();

            foreach (GetUsersByName_Result res in list)
                if (res.isHuman)
                {
                    retList.Add(new Human(res));
                }

            return retList;
        }

        /// <summary>
        /// This method rietreve all the sensors
        /// </summary>
        /// <returns>a list with all the sensors</returns>
        public async Task<List<Sensor>> GetAllSensors()
        {
            List<GetUsersByName_Result> list = await usrData.SearchUsersByName("").ConfigureAwait(false);

            List<Sensor> retList = new List<Sensor>();

            foreach (GetUsersByName_Result res in list)
                if (!res.isHuman)
                {
                    retList.Add(await LoadSensorInformation(res.Id));
                }

            return retList;
        }

        /// <summary>
        /// Method that rietreve the informations of a specific user
        /// </summary>
        /// <param name="userId">Integer that represent the ID of the user</param>
        /// <returns>Asynchronous operation that contain the informations about the user</returns>
        public async Task<User> LoadUserInformation(int userId)
        {
            Sensor tempSensor = await LoadSensorInformation(userId).ConfigureAwait(false);
            if (tempSensor.ID == 0 )
                return await LoadHumanInformation(userId);
            else
                return tempSensor;
        }

        /// <summary>
        /// Method that retrieve all the saved filters option on the feeds of a specific user
        /// </summary>
        /// <param name="userId">String that rappresent the ID of the user</param>
        /// <returns>Asynchronous operation that contain the List with all the saved filters of a specific user</returns>
        public async Task<List<Filter>> GetUserSavedFilters(int userId)
        {
            List<GetUserSavedFilters_Result> list = await usrData.GetUserSavedFilters(userId).ConfigureAwait(false);

            List<Filter> filterList = new List<Filter>();

            foreach (GetUserSavedFilters_Result entityFilter in list)
                filterList.Add(new Filter(entityFilter, await GetFilterTaggedUsers(entityFilter.ID)));

            return filterList;
        }

        /// <summary>
        /// Method that retrieve all the users referenced in a specific filter
        /// </summary>
        /// <param name="filterId">String that rappresent the ID of the filter</param>
        /// <returns>Asynchronous operation that contain the List referenced in the filter</returns>
        public async Task<List<User>> GetFilterTaggedUsers(int filterId)
        {
            List<GetUserSavedFiltersTagedUsers_Result> list = await usrData.GetFilterTaggedUsers(filterId).ConfigureAwait(false);

            List<User> userList = new List<User>();

            foreach(GetUserSavedFiltersTagedUsers_Result entityUser in list)
            {
                userList.Add(new User(entityUser));
            }

            return userList;
        }

        /// <summary>
        /// Method that retrieve the activities of a specific user.
        /// The activity could be like make a comment, or a feed, or a reference to another user
        /// </summary>
        /// <param name="userId">String that rappresent the ID of the human user</param>
        /// <returns>Asynchronous operation containing a List of activities</returns>
        public async Task<List<Activity>> GetUserActivity(int userId)
        {
            List<GetUserActivity_Result> list = await usrData.GetUserActivity(userId).ConfigureAwait(false);

            List<Activity> activityList = new List<Activity>();

            foreach (GetUserActivity_Result entityActivity in list)
            {
                activityList.Add(new Activity(entityActivity));
            }

            return activityList;
        }

        /// <summary>
        /// Method that retrieve the activities of a specific user.
        /// The activity could be like make a comment, or a feed, or a reference to another user
        /// </summary>
        /// <param name="userId">String that rappresent the ID of the human user</param>
        /// <param name="activitiesNumber">The number of the Activities to return</param>
        /// <param name="startId">The starting identifier of an activity in the database</param>
        /// <returns>Asynchronous operation containing a List of activities</returns>
        public async Task<List<Activity>> GetUserActivity(int userId, int activitiesNumber, int startId)
        {
            List<GetUserActivity_Result> list = await usrData.GetUserActivityFromId(userId, activitiesNumber, startId).ConfigureAwait(false);

            List<Activity> activityList = new List<Activity>();

            foreach (GetUserActivity_Result entityActivity in list)
            {
                activityList.Add(new Activity(entityActivity));
            }

            return activityList;
        }
    }
}
