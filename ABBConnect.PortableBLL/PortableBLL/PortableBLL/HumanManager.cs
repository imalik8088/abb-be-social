﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer;
using PortableTransformationLayer.ABBConnectServiceRef;


namespace BLL
{
    public class HumanManager : IHumanManager
    {
        Accesser accesser;

        public HumanManager()
        {
            accesser = new Accesser();
        }

        public bool Login(string userName, string password)
        {
            return accesser.LogIn(userName, password);

        }

        public Human LoadHumanInformation(int humanId)
        {
            return new Human(accesser.GetHumanInformation(humanId));
        }


        public Human LoadHumanInformationByUsername(string username)
        {
            return new Human(accesser.GetHumanInformationByUserName(username));
        }
    }
}
