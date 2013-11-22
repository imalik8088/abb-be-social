using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Transformation_Layer
{
    interface IHumanData
    {
        bool CheckCredentials(string userName, string password);
        DataSet GetHumanInformation(int humanId);
        DataSet GetHumanInformationByUsername(int humanId);
    }
}
