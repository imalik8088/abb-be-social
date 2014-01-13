using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface IUserManager
    {
        bool Login(string userName, string password);
        Human LoadHumanInformation(int humandId);
        Human LoadHumanInformationByUsername(string username);
        Sensor LoadSensorInformation(int sensorID);
        User LoadUserInformation(int userId);
        SensorHistoryData LoadHistoryValuesBySensor(int sensorID, DateTime startingTime, DateTime endingTime);
        int LoadCurrentValuesBySensor(int sensorID);
        List<User> SearchUserByName(string query);
        List<Human> GetAllHumanUsers();
        List<Sensor> GetAllSensors();
        List<Filter> GetUserSavedFilters(int userId);
        List<User> GetFilterTaggedUsers(int filterId);
        int AddFilter(int userId, Filter newFilter);
        bool AddUserToFilter(int userId, int filterId);
        bool FollowSensor(int humanUserId, int sensorUserId);
        bool UnfollowSensor(int humanUserId, int sensorUserId);
        List<Sensor> GetFollowedSensors(int humanUserId);
        List<Activity> GetUserActivity(int userId);
        List<Activity> GetUserActivity(int userId, int activitiesNumber, int startId);
        bool AddUserAvatar(int userId, string image);
    }
}
