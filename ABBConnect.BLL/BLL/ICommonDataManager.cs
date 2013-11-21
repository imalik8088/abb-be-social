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
        List<Category> GetFeedCategories();
        List<Feed> GetUserFeedByFilter(int userId, string location, DateTime startingTime, DateTime endingTime);
        List<Feed> GetUserFeeds(int userId);
    }
}
