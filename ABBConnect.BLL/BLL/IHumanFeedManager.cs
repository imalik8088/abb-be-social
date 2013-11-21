using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface IHumanFeedManager
    {
        List<Feed> LoadAllHumanFeeds();
        List<Feed> LoadAllHumanFeedsByFilter(string location, DateTime startingTime, DateTime endingTime);
    }
}
