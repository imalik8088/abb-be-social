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
        HumanData humanData;

        public HumanManager()
        {
            humanData = new HumanData();
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
