using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Transformation_Layer
{
    interface ICommonData
    {
        DataSet GetAllLocations();
        DataSet GetPostGategories();
    }
}
