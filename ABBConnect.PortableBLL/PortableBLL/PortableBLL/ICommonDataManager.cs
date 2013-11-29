using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface ICommonDataManager
    {
        List<string> GetLocations();
        Task<List<Category>> GetFeedCategories();
    }
}
