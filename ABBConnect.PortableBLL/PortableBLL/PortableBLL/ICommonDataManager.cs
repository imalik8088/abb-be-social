using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableBLL
{
    interface ICommonDataManager
    {
        Task<List<string>> GetLocations();
        Task<List<Category>> GetFeedCategories();
    }
}
