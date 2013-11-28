using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    interface ICommonDataManager
    {
        List<string> GetLocations();
        List<Category> GetFeedCategories();
    }
}
