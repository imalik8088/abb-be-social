using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableBLL
{
    interface IUserManager
    {
        Task<bool> Login(string userName, string password);
        Task<Human> LoadHumanInformation(int humandId);
        Task<Human> LoadHumanInformationByUsername(string username);
        Task<Sensor> LoadSensorInformation(int sensorID);
        Task<User> LoadUserInformation(int userId);
        Task<SensorHistoryData> LoadHistoryValuesBySensor(int sensorID, DateTime startingTime, DateTime endingTime);
        Task<int> LoadCurrentValuesBySensor(int sensorID);
        Task<List<User>> SearchUserByName(string query);
        Task<List<Human>> GetAllHumanUsers();
        Task<List<Sensor>> GetAllSensors();
        Task<List<Filter>> GetUserSavedFilters(int userId);
        Task<List<User>> GetFilterTaggedUsers(int filterId);

    }
}
