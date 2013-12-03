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
    public class HumanData: IHumanData
    {
        private Connection urlServer;

        public HumanData()
        {
            urlServer = new Connection();
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
    }
}
