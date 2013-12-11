using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace PortableBLL
{
    public class UserManager : IUserManager
    {
        private UserData usrData;

        public UserManager()
        {
            usrData = new UserData();
        }

        public async Task<Sensor> LoadSensorInformation(int sensorID)
        {
            GetSensorInformation_Result tempSensor = await usrData.GetSensorInformation(sensorID).ConfigureAwait(false);
            Sensor responseSensor = new Sensor(tempSensor);
            return responseSensor;
        }

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

        public async Task<int> LoadCurrentValuesBySensor(int sensorID)
        {
            return await usrData.GetLastSensorValue(sensorID).ConfigureAwait(false);
        }

        public async Task<bool> Login(string userName, string password)
        {
            return await usrData.LogIn(userName, password).ConfigureAwait(false); ;

        }

        public async Task<Human> LoadHumanInformation(int humanId)
        {
            return new Human(await usrData.GetHumanInformation(humanId).ConfigureAwait(false));
        }

        public async Task<Human> LoadHumanInformationByUsername(string username)
        {
            return new Human(await usrData.GetHumanInformationByUserName(username).ConfigureAwait(false));
        }

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
                    retList.Add(await LoadSensorInformation(res.Id));
                }

            return retList;
        }


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
    }
}
