using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;


namespace BLL
{
    public class HumanManager : IHumanManager
    {
        Accesser accesser;

        public HumanManager()
        {
            accesser = new Accesser();
        }

        public async Task<bool> Login(string userName, string password)
        {
            return await accesser.LogIn(userName, password).ConfigureAwait(false); ;

        }

        public async Task<Human> LoadHumanInformation(int humanId)
        {
            return new Human(await accesser.GetHumanInformation(humanId).ConfigureAwait(false));
        }


        public async Task<Human> LoadHumanInformationByUsername(string username)
        {
            return new Human(await accesser.GetHumanInformationByUserName(username).ConfigureAwait(false));
        }
    }
}
