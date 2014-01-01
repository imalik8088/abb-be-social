using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Threading.Tasks;

namespace BLL
{
    public class UserManager : IUserManager
    {
        private UserData usrData;

        public UserManager()
        {
            usrData = new UserData();
        }

        public  Sensor LoadSensorInformation(int sensorID)
        {
            GetSensorInformation_Result tempSensor =  usrData.GetSensorInformation(sensorID);
            Sensor responseSensor = new Sensor(tempSensor);
            return responseSensor;
        }

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

        public  int LoadCurrentValuesBySensor(int sensorID)
        {
            
            return  usrData.GetLastSensorValue(sensorID);
        }

        public  bool Login(string userName, string password)
        {
            return  usrData.LogIn(userName, password); ;

        }

        public  Human LoadHumanInformation(int humanId)
        {
            return new Human( usrData.GetHumanInformation(humanId), humanId);
        }

        public  Human LoadHumanInformationByUsername(string username)
        {
            return new Human( usrData.GetHumanInformationByUsername(username));
        }

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


        public  User LoadUserInformation(int userId)
        {
            Sensor tempSensor =  LoadSensorInformation(userId);
            if (tempSensor.ID == 0 )
                return  LoadHumanInformation(userId);
            else
                return tempSensor;
        }


        public  List<Filter> GetUserSavedFilters(int userId)
        {
            List<GetUserSavedFilters_Result> list =  usrData.GetSavedFilter(userId);

            List<Filter> filterList = new List<Filter>();

            foreach (GetUserSavedFilters_Result entityFilter in list)
                filterList.Add(new Filter(entityFilter,  GetFilterTaggedUsers(entityFilter.ID)));

            return filterList;
        }

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


        public int AddFilter(int userId, Filter newFilter)
        {
            if (userId < 0 || newFilter.Equals(null))
            {
                return -1;
            }
            return usrData.SaveFilter(userId, newFilter.Name, newFilter.StartDate, newFilter.EndDate, newFilter.Location, newFilter.TypeOfFeed.ToString());
        }

        public bool AddUserToFilter(int userId, int filterId)
        {
            if (userId < 0 || filterId < 0)
            {
                return false;
            }

            return usrData.AddFilterUser(userId, filterId);
        }

        public bool FollowSensor(int humanUserId, int sensorUserId)
        {
            if (humanUserId < 0 || sensorUserId < 0)
            {
                return false;
            }

            return usrData.FollowSensor(humanUserId, sensorUserId);
        }

        public bool UnfollowSensor(int humanUserId, int sensorUserId)
        {
            if (humanUserId < 0 || sensorUserId < 0)
            {
                return false;
            }

            return usrData.UnfollowSensor(humanUserId, sensorUserId);
        }

        public List<int> GetFollowedSensors(int humanUserId)
        {
            if (humanUserId < 0)
            {
                return null;
            }

            return usrData.GetFollowedSensors(humanUserId);
        }

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
    }
}
