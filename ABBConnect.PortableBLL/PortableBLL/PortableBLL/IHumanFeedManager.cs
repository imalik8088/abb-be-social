using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    interface IHumanFeedManager
    {
        List<HumanFeed> LoadAllHumanFeeds();
        List<HumanFeed> LoadAllHumanFeedsByFilter(string location, DateTime startingTime, DateTime endingTime);
    }
}
