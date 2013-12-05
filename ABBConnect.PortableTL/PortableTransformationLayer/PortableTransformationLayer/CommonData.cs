﻿using System;
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
    public class CommonData: ICommonData
    {
        private Connection urlServer;

        public CommonData()
        {
            urlServer = new Connection();
        }

        public async Task<List<ABBConnectServiceRef.GetPriorityCategories_Result>> GetCategories()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetCategories").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<GetPriorityCategories_Result>>(response);
        }

        public async Task<List<string>> GetLocations()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlServer.Url);
            var response = await client.GetStringAsync("GetLocations").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<string>>(response);
        }
    }
}
