﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface IHumanManager
    {
        bool Login(string userName, string password);
        Human LoadHumanInformation(int humandId);
    }
}