using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace PortableTransformationLayer
{
    interface IHumanData
    {
        Task<bool> LogIn(string usrName, string pw);
        Task<GetHumanInformation_Result> GetHumanInformation(int Id);
        Task<GetHumanInformationByUsername_Result> GetHumanInformationByUserName(string username);
    }
}
