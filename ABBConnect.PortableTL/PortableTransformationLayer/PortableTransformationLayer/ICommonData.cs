using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace PortableTransformationLayer
{
    interface ICommonData
    {
        Task<List<GetPriorityCategories_Result>> GetCategories();
        Task<List<string>> GetLocations();
    }
}
