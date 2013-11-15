using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface IUserFeedManager
    {
        List<Feed> LoadUserFeeds(string userName);
        //List<Feed> LoadUserFeedsByLocation(string location);
        //List<Feed> LoadUserFeedsByTime(DateTime startTime, DateTime endTime);
    }
}
