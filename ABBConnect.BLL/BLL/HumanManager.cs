using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class HumanManager : IHumanManager
    {
       
        public HumanManager()
        {
            
        }

        public bool Login(string userName, string password)
        {
            throw new NotImplementedException();

        }

        public Human LoadHumanInformation(int humanId)
        {
            throw new NotImplementedException();
           
        }


        public Human LoadHumanInformationByUsername(string username)
        {
            throw new NotImplementedException();


        }
    }
}
