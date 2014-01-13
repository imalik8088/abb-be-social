using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Threading.Tasks;

namespace BLL
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
        public  Sensor LoadSensorInformation(int sensorID)
        {
            GetSensorInformation_Result tempSensor =  usrData.GetSensorInformation(sensorID);
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
        public  SensorHistoryData LoadHistoryValuesBySensor(int sensorID, DateTime startingTime, DateTime endingTime)
        {
            
            List<GetHistoricalDataFromSensor_Result> listHistData =  usrData.GetHistoricalDataFromSensor(sensorID, startingTime, endingTime);

            UserManager senInfoMng = new UserManager();
 
            SensorHistoryData senHistData = new SensorHistoryData( senInfoMng.LoadSensorInformation(sensorID));
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
        public  int LoadCurrentValuesBySensor(int sensorID)
        {
            
            return  usrData.GetLastSensorValue(sensorID);
        }

        /// <summary>
        /// This method allow to log into the system, if the credential of a registered user are right
        /// </summary>
        /// <param name="userName">string that rappresent the username of the user</param>
        /// <param name="password">string that rappresent the password of the user</param>
        /// <returns>A boolean value that comunicate if the login succeded or fail</returns>
        public  bool Login(string userName, string password)
        {
            return  usrData.LogIn(userName, password); ;

        }

        /// <summary>
        /// This method take the information of a specific user 
        /// </summary>
        /// <param name="humandId">integer that identify the user</param>
        /// <returns>all the informations about a user</returns>
        public  Human LoadHumanInformation(int humanId)
        {
            return new Human( usrData.GetHumanInformation(humanId), humanId);
        }

        /// <summary>
        /// This method take the information of a specific user 
        /// </summary>
        /// <param name="username">string that rappresent the username used by the user</param>
        /// <returns>all the informations about a user</returns>
        public  Human LoadHumanInformationByUsername(string username)
        {
            return new Human( usrData.GetHumanInformationByUsername(username));
        }

        /// <summary>
        /// This method search all the users with the name given in input
        /// </summary>
        /// <param name="query">name of the user that should be searched</param>
        /// <returns>list of users with that name</returns>
        public  List<User> SearchUserByName(string query)
        {
            List<GetUsersByName_Result> list =  usrData.SearchUsersByName(query);

            List<User> retList = new List<User>();

            foreach (GetUsersByName_Result res in list)
                if (res.isHuman)
                {
                    retList.Add(new Human(res));
                }
                else
                {
                    retList.Add( LoadSensorInformation(res.Id));
                }

            return retList;
        }

        /// <summary>
        /// This method rietreve all the human users 
        /// </summary>
        /// <returns>a list with all the human users</returns>
        public  List<Human> GetAllHumanUsers()
        {
            List<GetUsersByName_Result> list =  usrData.SearchUsersByName("");

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
        public  List<Sensor> GetAllSensors()
        {
            List<GetUsersByName_Result> list =  usrData.SearchUsersByName("");

            List<Sensor> retList = new List<Sensor>();

            foreach (GetUsersByName_Result res in list)
                if (!res.isHuman)
                {
                    retList.Add( LoadSensorInformation(res.Id));
                }

            return retList;
        }

        /// <summary>
        /// Method that rietreve the informations of a specific user
        /// </summary>
        /// <param name="userId">Integer that represent the ID of the user</param>
        /// <returns>operation that contain the informations about the user</returns>
        public  User LoadUserInformation(int userId)
        {
            Sensor tempSensor =  LoadSensorInformation(userId);
            if (tempSensor.ID == 0 )
                return  LoadHumanInformation(userId);
            else
                return tempSensor;
        }

        /// <summary>
        /// Method that retrieve all the saved filters option on the feeds of a specific user
        /// </summary>
        /// <param name="userId">String that rappresent the ID of the user</param>
        /// <returns>operation that contain the List with all the saved filters of a specific user</returns>
        public  List<Filter> GetUserSavedFilters(int userId)
        {
            List<GetUserSavedFilters_Result> list =  usrData.GetSavedFilter(userId);

            List<Filter> filterList = new List<Filter>();

            foreach (GetUserSavedFilters_Result entityFilter in list)
                filterList.Add(new Filter(entityFilter,  GetFilterTaggedUsers(entityFilter.ID)));

            return filterList;
        }

        /// <summary>
        /// Method that retrieve all the users referenced in a specific filter
        /// </summary>
        /// <param name="filterId">String that rappresent the ID of the filter</param>
        /// <returns>operation that contain the List referenced in the filter</returns>
        public  List<User> GetFilterTaggedUsers(int filterId)
        {
            List<GetUserSavedFiltersTagedUsers_Result> list =  usrData.GetFilterTaggedUsers(filterId);

            List<User> userList = new List<User>();

            foreach(GetUserSavedFiltersTagedUsers_Result entityUser in list)
            {
                userList.Add(new User(entityUser));
            }

            return userList;
        }

        /// <summary>
        /// Method that store a filter made by an human user
        /// </summary>
        /// <param name="userId">Integer that rappresent the ID of the human user</param>
        /// <param name="newFilter">Class that represent the filter informations</param>
        /// <returns>operation containing the ID of the stored filter</returns>
        public int AddFilter(int userId, Filter newFilter)
        {
            if (userId < 0 || newFilter.Equals(null))
            {
                return -1;
            }
            return usrData.SaveFilter(userId, newFilter.Name, newFilter.StartDate, newFilter.EndDate, newFilter.Location, newFilter.TypeOfFeed.ToString());
        }

        /// <summary>
        /// Method that save the reference to a user in a specific filter
        /// </summary>
        /// <param name="userId">String that rappresent the ID of the user that will be referenced in the filter option</param>
        /// <param name="filterId">String that rappresent the ID of the filter option</param>
        /// <returns>operation containing the outcome of the operation</returns>
        public bool AddUserToFilter(int userId, int filterId)
        {
            if (userId < 0 || filterId < 0)
            {
                return false;
            }

            return usrData.AddFilterUser(userId, filterId);
        }

        /// <summary>
        /// Method that save the reference between a sensor and a human user
        /// </summary>
        /// <param name="humanUserId">String that rappresent the ID of the human user</param>
        /// <param name="sensorUserId">String that rappresent the ID of the sensor</param>
        /// <returns>operation containing a boolean that represent the outcome of the operation</returns>
        public bool FollowSensor(int humanUserId, int sensorUserId)
        {
            if (humanUserId < 0 || sensorUserId < 0)
            {
                return false;
            }

            return usrData.FollowSensor(humanUserId, sensorUserId);
        }

        /// <summary>
        /// Method that delete the reference between a sensor and a human user
        /// </summary>
        /// <param name="humanUserId">String that rappresent the ID of the human user</param>
        /// <param name="sensorUserId">String that rappresent the ID of the sensor user</param>
        /// <returns>operation containing a boolean that represent the outcome of the operation</returns>
        public bool UnfollowSensor(int humanUserId, int sensorUserId)
        {
            if (humanUserId < 0 || sensorUserId < 0)
            {
                return false;
            }

            return usrData.UnfollowSensor(humanUserId, sensorUserId);
        }

        /// <summary>
        /// Method that retrieve the ID of the sensors referenced by a human user
        /// </summary>
        /// <param name="humanUserId">String that rappresent the ID of the human user</param>
        /// <returns>operation containing a List of all the sensors followed by the human user</returns>
        public List<Sensor> GetFollowedSensors(int humanUserId)
        {
            if (humanUserId < 0)
            {
                return null;
            }

            List<Sensor> sensorList = new List<Sensor>();

            List<int> sensorIds = usrData.GetFollowedSensors(humanUserId);

            foreach (int i in sensorIds)
            {
                Sensor tempSensor = this.LoadSensorInformation(i);
                sensorList.Add(tempSensor);
            }

            return sensorList;
        }

        /// <summary>
        /// Method that retrieve the activities of a specific user.
        /// The activity could be like make a comment, or a feed, or a reference to another user
        /// </summary>
        /// <param name="userId">String that rappresent the ID of the human user</param>
        /// <returns>operation containing a List of activities</returns>
        public  List<Activity> GetUserActivity(int userId)
        {
            List<GetUserActivity_Result> list =  usrData.GetUserActivity(userId);

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
        /// <param name="userId">The identifier of the user, whose activity is requested</param>
        /// <param name="activitiesNumber">The amount of the requested activities</param>
        /// <param name="startId">The starting identifier of the activity objects</param>
        /// <returns>operation containing a List of activities</returns>
        public List<Activity> GetUserActivity(int userId, int activitiesNumber, int startId)
        {
            List<GetUserActivity_Result> list = usrData.GetUserActivityFromId(userId, activitiesNumber, startId);

            List<Activity> activityList = new List<Activity>();

            foreach (GetUserActivity_Result entityActivity in list)
            {
                activityList.Add(new Activity(entityActivity));
            }

            return activityList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public bool AddUserAvatar(int userId, string image)
        {
            if (userId < 0)
            {
                return false;
            }

            return usrData.AddUserAvatar(userId, image);
        }
    }
}
