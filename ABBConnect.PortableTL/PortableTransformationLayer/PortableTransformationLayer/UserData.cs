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
    public class UserData: IUserData
    {
        private Connection urlServer;

        public UserData()
        {
            urlServer = new Connection();
        }

        public async Task<ABBConnectServiceRef.GetSensorInformation_Result> GetSensorInformation(int id)
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

        public async Task<int> GetLastSensorValue(int id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetLastSensorValue?id=" + id.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<int>(response);
        }

        public async Task<bool> LogIn(string usrName, string pw)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("LogIn?username="
                + usrName + "&password=" + pw).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<bool>(response);
        }

        public async Task<ABBConnectServiceRef.GetHumanInformation_Result> GetHumanInformation(int Id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetHumanInfo?id=" + Id.ToString()).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GetHumanInformation_Result>(response);
        }

        public async Task<ABBConnectServiceRef.GetHumanInformationByUsername_Result> GetHumanInformationByUserName(string username)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetHumanInfoByUsername?username=" + username).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GetHumanInformationByUsername_Result>(response);
        }


        public async Task<List<GetUsersByName_Result>> SearchUsersByName(string query)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("SearchByName?name=" + query).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetUsersByName_Result>>(response);
        }
    }
}
