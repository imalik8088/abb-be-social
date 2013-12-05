using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transformation_Layer;

namespace BLL
{
    class Program
    {
        static void Main(string[] args)
        {
            HumanManager um = new HumanManager();
            Human u = um.LoadHumanInformationByUsername("dks12001");
            Console.WriteLine(u.FirstName);
            //Console.WriteLine(um.Login("rgn09003","password"));
            HumanFeedManager ufm = new HumanFeedManager();
            //SensorManager sm = new SensorManager();
            //Console.WriteLine(sm.LoadSensorInformation(1001).LowerBoundary);

            DateTime myDate = Convert.ToDateTime("2013-11-11 14:01");

            //SensorHistoryData hisData = new SensorHistoryData();
            //hisData = sm.LoadHistoryValuesBySensor(1001, myDate, DateTime.Now);

            CommonDataManager cdm = new CommonDataManager();

           

            //CommonDataManager cm = new CommonDataManager();
            //List<string> loc = cdm.GetLocations();
            //List<Category> loc = cdm.GetFeedCategories();

            //foreach (Category str in loc)
            //{
            //    Console.WriteLine(str.CategoryName);
            //}

            //List<Feed> feedList = new List<Feed>();
            //feedList = fm.LoadUserFeeds("simpm");
            //foreach (Feed f in feedList)
            //{
            //    Console.WriteLine(f.Location);
            //}

            FeedManager sfm = new FeedManager();
            HumanFeed hf = new HumanFeed();
            //hf.Owner.ID = 3;
            //hf.Location = "ControlRoom 1B";
            //hf.Content = "BLL speaking";
            //hf.Category = "WorkPost";
            //hf.MediaFilePath = "";
            //hf.Priority = 3;
            //Human h = new Human();
            //h.UserName = "dpa12001";
            //hf.Tags.Add(h);
            //sfm.PublishFeed(hf);
            List<Feed> lsf = new List<Feed>();
            lsf = sfm.LoadLatestXFeedsFromId(28,5);
            //lsf = sfm.GetSensorFeeds(1002);
            //lsf = sfm.GetAllSensorFeedsByFilter(null, myDate, DateTime.Now);
            //lsf = sfm.GetAllSensorFeedsByFilter("Machine 1", DateTime.MinValue, DateTime.MinValue);
            //lsf = sfm.GetSensorFeedsByFilter(1001, null, DateTime.MinValue, DateTime.MinValue);

            foreach (Feed sf in lsf)
            {
                Console.WriteLine(sf.Content);
                Console.WriteLine(sf.ID);
                Console.WriteLine(sf.TimeStamp);
            }

            //FeedManager fm = new FeedManager();
            //List<Feed> lsfeed = fm.LoadLatest20Feeds();

            //List<HumanFeed> lsfeed = ufm.LoadAllHumanFeeds();

            //foreach (Feed f in lsfeed)
            //{
            //    Console.WriteLine(f.Content);

            //    if (f.Tags.Count > 0)
            //        foreach (Human h in f.Tags)
            //        {
            //            Console.WriteLine(h.UserName);
            //        }

            //    foreach (Comment c in f.Comments)
            //    {
            //        Console.WriteLine(c.Content);
            //    }
            //}

            Console.ReadKey();
        }
    }
}
