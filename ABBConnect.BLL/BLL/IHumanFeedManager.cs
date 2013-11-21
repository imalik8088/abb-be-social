using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface IHumanFeedManager
    {
        List<HumanFeed> LoadAllHumanFeeds();
        List<HumanFeed> LoadAllHumanFeedsByFilter(string location, DateTime startingTime, DateTime endingTime);
    }
}
