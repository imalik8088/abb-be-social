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
    /// <summary>
    /// Class that allow to retrieve additional information shared by the feeds
    /// </summary>
    public class CommonData: ICommonData
    {
        /// <summary>
        /// Attribute that provide the connection to the server
        /// </summary>
        private Connection urlServer;

        /// <summary>
        /// Constructor that automatically instantiate the attribute of the class
        /// </summary>
        public CommonData()
        {
            urlServer = new Connection();
        }

        /// <summary>
        /// This method  get the categories of the feeds rappresented by the priority
        /// </summary>
        /// <returns>Asynchronous operation that contain the List of catetegories</returns>
        public async Task<List<ABBConnectServiceRef.GetPriorityCategories_Result>> GetCategories()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetCategories").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetPriorityCategories_Result>>(response);
        }

        /// <summary>
        /// This method get all locations that compose a specific workspace
        /// </summary>
        /// <returns>Asynchronous operation that contain the List of Locations</returns>
        public async Task<List<string>> GetLocations()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetLocations").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<string>>(response);
        }
    }
}
