using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface IHumanManager
    {
        Task<bool> Login(string userName, string password);
        Task<Human> LoadHumanInformation(int humandId);
        Task<Human> LoadHumanInformationByUsername(string username);
    }
}
