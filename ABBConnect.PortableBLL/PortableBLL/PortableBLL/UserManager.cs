using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace PortableBLL
{
    class UserManager: IUserManager
    {
        public async Task<Sensor> LoadSensorInformation(int sensorID)
        {
            GetSensorInformation_Result tempSensor = await senData.GetSensorInformation(sensorID).ConfigureAwait(false);
            Sensor responseSensor = new Sensor(tempSensor);
            return responseSensor;
        }

        public async Task<SensorHistoryData> LoadHistoryValuesBySensor(int sensorID, DateTime startingTime, DateTime endingTime)
        {
            throw new NotImplementedException();
        }

        public async Task<int> LoadCurrentValuesBySensor(int sensorID)
        {
            return await senData.GetLastSensorValue(sensorID).ConfigureAwait(false);
        }

        public async Task<bool> Login(string userName, string password)
        {
            return await humanData.LogIn(userName, password).ConfigureAwait(false); ;

        }

        public async Task<Human> LoadHumanInformation(int humanId)
        {
            return new Human(await humanData.GetHumanInformation(humanId).ConfigureAwait(false));
        }

        public async Task<Human> LoadHumanInformationByUsername(string username)
        {
            return new Human(await humanData.GetHumanInformationByUserName(username).ConfigureAwait(false));
        }
    }
}
