using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    interface IHumanManager
    {
        bool Login(string userName, string password);
        Human LoadHumanInformation(int humandId);
        Human LoadHumanInformationByUsername(string username);
    }
}
