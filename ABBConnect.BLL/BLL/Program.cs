using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    class Program
    {
        static void Main(string[] args)
        {
            //UserManager um = new UserManager();
            //User u = um.LoadUserInformation("simpm");
            //Console.WriteLine(u.FirstName);
            //Console.WriteLine(um.Login("simpm","password"));
            UserFeedManager ufm = new UserFeedManager();
            //SensorManager sm = new SensorManager();
            //Console.WriteLine(sm.LoadSensorInformation(1001).LowerBoundary);

            DateTime myDate = Convert.ToDateTime("2013-11-11 14:01");

            //SensorHistoryData hisData = new SensorHistoryData();
            //hisData = sm.LoadHistoryValuesBySensor(1001, myDate, DateTime.Now);


            //CommonDataManager cm = new CommonDataManager();
            //List<string> loc = cm.GetLocations();
            //List<string> loc = cm.GetFeedCategories();

            //foreach (string str in loc)
            //{
            //    Console.WriteLine(str);
            //}

            //List<Feed> feedList = new List<Feed>();
            //feedList = fm.LoadUserFeeds("simpm");
            //foreach (Feed f in feedList)
            //{
            //    Console.WriteLine(f.Location);
            //}

            SensorFeedManager sfm = new SensorFeedManager();
            List<SensorFeed> lsf = new List<SensorFeed>();
            //lsf = sfm.GetAllSensorFeeds();
            //lsf = sfm.GetSensorFeeds(1002);
            //lsf = sfm.GetAllSensorFeedsByFilter(null, myDate, DateTime.Now);
            //lsf = sfm.GetAllSensorFeedsByFilter("Machine 1", DateTime.MinValue, DateTime.MinValue);
            //lsf = sfm.GetSensorFeedsByFilter(1001, null, DateTime.MinValue, DateTime.MinValue);

            //foreach (SensorFeed sf in lsf)
            //{
            //    Console.WriteLine(sf.Content);
            //    Console.WriteLine(sf.Owner.Name);
            //}

            //FeedManager fm = new FeedManager();
            //List<Feed> lsfeed = fm.LoadLatest20Feeds();

            List<UserFeed> lsfeed = ufm.LoadUserFeedsByFilter("rgn09003" , "ControlRoom 1B", DateTime.MinValue, DateTime.MinValue);

            foreach (Feed f in lsfeed)
            {
                Console.WriteLine(f.Content);

                if(f.Tags.Count>0)
                    foreach (User u in f.Tags)
                    {
                        Console.WriteLine(u.UserName);
                    }

                foreach (Comment u in f.Comments)
                {
                    Console.WriteLine(u.Content);
                }
            }

            Console.ReadKey();
        }
    }
}
