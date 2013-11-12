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
            UserManager um = new UserManager();
            User u = um.LoadUserInformation("simpm");
            Console.WriteLine(u.LastName);
            UserFeedManager fm = new UserFeedManager();
            List<Feed> feedList = new List<Feed>();
            feedList = fm.LoadUserFeeds("simpm");
            foreach (Feed f in feedList)
            {
                Console.WriteLine(f.Location);
            }
            Console.ReadKey();
        }
    }
}
