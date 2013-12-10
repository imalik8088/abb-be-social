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
    }
}
