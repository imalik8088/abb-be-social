using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace PortableTransformationLayer
{
    interface IUserData
    {
        Task<GetSensorInformation_Result> GetSensorInformation(int id);
        Task<List<GetHistoricalDataFromSensor_Result>> GetHistoricalDataFromSensor(int id, DateTime startingTime, DateTime endingTime);
        Task<int> GetLastSensorValue(int id);
        Task<bool> LogIn(string usrName, string pw);
        Task<GetHumanInformation_Result> GetHumanInformation(int Id);
        Task<GetHumanInformationByUsername_Result> GetHumanInformationByUserName(string username);
        Task<List<GetUsersByName_Result>> SearchUsersByName(string query);
        Task<List<GetUserSavedFilters_Result>> GetUserSavedFilters(int userId);
        Task<List<GetUserSavedFiltersTagedUsers_Result>> GetFilterTaggedUsers(int filterId);
        Task<int> AddFilter(int userId, string filterName, DateTime startingTime, DateTime endingTime, string location, string feedType);
        Task<bool> AddUserToFilter(int userId, int filterId);
        Task<bool> FollowSensor(int humanUserId, int sensorUserId);
        Task<bool> UnfollowSensor(int humanUserId, int sensorUserId);
        Task<List<int>> GetFollowedSensors(int humanUserId);
        Task<List<GetUserActivity_Result>> GetUserActivity(int userId);

    }
}
