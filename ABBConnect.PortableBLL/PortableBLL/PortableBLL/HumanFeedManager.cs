using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BLL
{
    public class HumanFeedManager: IHumanFeedManager
    {


        public HumanFeedManager()
        {
         
        }

        // METHOD: GetAllUserFeeds

        public List<HumanFeed> LoadAllHumanFeeds()
        {
            throw new NotImplementedException();
            
        }

        // METHOD: GetAllUserFeedsByFilter

        public List<HumanFeed> LoadAllHumanFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            throw new NotImplementedException();

        }

        private static bool CastStringToInt(string str, ref int returnValue)
        {
            throw new NotImplementedException();

        }

        private static string AvoidStringNulls(string str)
        {
            string resStr = "";

            if (str != null)
                resStr = str;

            return resStr;
        }
    }
}
