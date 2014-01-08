using System;
using NUnit;
using NUnit.Framework;
using BLL;
using DAL;
using System.Linq;
using System.Collections.Generic;

namespace TestBLL
{
    [TestFixture]
    public class TestBLL
    {
        Human loggedInUser;

        [SetUp]
        public void InitLogin()
        {
            UserManager userManager = new UserManager();

            loggedInUser = userManager.LoadHumanInformationByUsername("mario");
        }


        /*[TestFixture]
           public class TestCategory
           {
               [Test]
               public void categoryTest()
               {
                   Category c1 = new Category();
                   GetPriorityCategories_Result loadedCategory = c1.Category("results");
                   string categoryName = result.Name;
                   int id = 1;
                   string actual = c1.CategoryName;
                   string expected = "SensorAlarm";
                   Assert.AreEqual(actual, expected);
               }

           }*/

        /* [TestFixture]
         public class TestComment
         {
             [Test]
             public void commentTest()
             {
                 Comment comment1 = new Comment();
                 comment1.Content = "New comment";
                 Assert.AreEqual(comment1.ID, 9);
             }

         }

         [TestFixture]
         public class TestCommonDataManager
         {
             [Test]
             public void commentDbDataTest()
             {
                 Comment comment1 = new Comment();
                 comment1.Content = "New comment";
                 Assert.AreEqual(comment1.ID, 1);
             }

         }
         */

        [Test]
        public void feedTest()
        {
            Feed f1 = new Feed();
            f1.Content = "asfasf";
            Assert.AreEqual(f1.ID, 154);
        }


        /*  [TestFixture]
          public class TestFeedManager
          {
              [Test]
              public void feedTest()
              {
                  FeedManager fm = new FeedManager();
                  fm.LoadNewFeeds().Owner.UserName = "rgn09003";
                  Assert.AreEqual(fm.ID, 154);
              }

          }

          [TestFixture]
          public class TestHumanFeed
          {
              [Test]
              public void humanFeedTest()
              {
                  HumanFeed hf = new HumanFeed();
                  hf.Owner="dpa12001";

                  Assert.AreEqual(hf.MediaFilePath, "hej");
              }

          } 
        [TestFixture]
        public class TestFeed
        {
            [Test]
            public void feedTest()
            {
                FeedManager h = new FeedManager();
                GetLatestXFeeds_Result feed = new GetLatestXFeeds_Result();
               // List<Feed> loadedFeeds = h.LoadLatestXFeeds(2);
                feed.FeedId = 1;
                string actual = feed.Username;
                string expected = "Rob";
                Assert.AreEqual(actual, expected);
            }

        }*/

        [Test]
        public void feedTest2()
        {
            FeedManager h = new FeedManager();
            GetLatestXFeeds_Result feed = new GetLatestXFeeds_Result();
            // List<Feed> loadedFeeds = h.LoadLatestXFeeds(2);
            feed.FeedId = 1;
            string actual = feed.Username;
            string expected = "Rob";
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void sensorInformationTest()
        {
            UserManager h = new UserManager();
            Sensor loadedSensor = h.LoadSensorInformation(7);
            string actual = loadedSensor.Location;
            string expected = "3";
            Assert.AreEqual(actual, expected);
        }

        //Test case for Method "LoadHistoryValuesBySensor" in UserManager class
        //It tests if the right username is returned
        [Test]
        public void loadHistoryValuesBySensorTest()
        {
            UserManager h = new UserManager();
            SensorHistoryData loadedSensor = h.LoadHistoryValuesBySensor(9, DateTime.MinValue, DateTime.MinValue);
            string actual = loadedSensor.Owner.UserName;
            string expected = "Sensor4";
            Assert.AreEqual(actual, expected);
        }

        //Test case for Method "LoadHistoryValuesBySensor" in UserManager class
        //It tests if the right Upper Boundary value is returned
        [Test]
        public void loadHistoryValuesBySensorTest2()
        {
            UserManager h = new UserManager();
            SensorHistoryData loadedSensor = h.LoadHistoryValuesBySensor(9, DateTime.MinValue, DateTime.MinValue);
            decimal actual = loadedSensor.Owner.UpperBoundary;
            decimal expected = 10;
            Assert.AreEqual(actual, expected);
        }

        //Test case for Method "LoadCurrentValuesBySensor" in UserManager class
        //It tests if the right current value is returned from a sensor
        [Test]
        public void loadCurrentValuesBySensorTest()
        {
            UserManager h = new UserManager();
            int actual = h.LoadCurrentValuesBySensor(8);
            int expected = -666;
            Assert.AreEqual(actual, expected);
        }

        //Test case for boolean Method "Login" in UserManager class
        //It tests if the right credentials are entered
        [Test]
        public void loginTest()
        {
            UserManager h = new UserManager();
            bool loadedHuman = h.Login("rgn09003", "password");
            Assert.IsTrue(loadedHuman);
        }

        //Test case for Method "LoadHumanInformation" in UserManager class
        //It tests if the right username is returned when the user ID is given
        [Test]
        public void loadHumanInformationTest()
        {
            UserManager h = new UserManager();
            Human loadedHuman = h.LoadHumanInformation(2);
            string actual = loadedHuman.UserName;
            string expected = "dpa12001";
            Assert.AreEqual(actual, expected);
        }

        //Test case for Method "LoadHumanInformation" in UserManager class
        //It tests if the right email is returned when the user ID is given
        [Test]
        public void loadHumanInformationTest2()
        {
            UserManager h = new UserManager();
            Human loadedHuman = h.LoadHumanInformation(1);
            string actual = loadedHuman.Email;
            string expected = "rgn09003@student.mdh.se";
            Assert.AreEqual(actual, expected);
        }

        //Test case for Method "LoadHumanInformationByUsername" in UserManager class
        //It tests if the right firstname is returned when the username is given
        [Test]
        public void loadHumanInformationByUsernameTest()
        {
            UserManager h = new UserManager();
            Human loadedHuman = h.LoadHumanInformationByUsername("rgn09003");
            string actual = loadedHuman.FirstName;
            string expected = "Rob";
            Assert.AreEqual(actual, expected);
        }

        //Test case for Method "GetFeedByFeedId" in FeedManager class
        //It tests if the right ... is returned when the FeedId is given
        //not working...
        [Test]
        public void getFeedByFeedIdTest()
        {
            FeedManager h = new FeedManager();
            Feed loadedFeed = h.GetFeedByFeedId(85);
            string actual = loadedFeed.FeedType;
            string expected = "Sen";
            Assert.AreEqual(actual, expected);
        }

        //Test case for Method "LoadLatestXFeedsTest" in FeedManager class
        //It tests if the right string is contained in the latest feeds
        //not working...??
        [Test]
        public void loadLatestXFeedsTest()
        {
            FeedManager h = new FeedManager();
            List<Feed> loadedFeed = h.LoadLatestXFeeds(1);
            string expected = "lorem";
            Assert.Contains(expected, loadedFeed);
        }

        //Test case for Method "LoadLatestXFeedsTest" in FeedManager class
        //It tests if the the latest loaded feeds are not empty 
        [Test]
        public void loadLatestXFeedsTest2()
        {
            FeedManager h = new FeedManager();
            List<Feed> loadedFeed = h.LoadLatestXFeeds(1);
            Assert.IsNotEmpty(loadedFeed);
        }

        //Test case for Method "LoadLatestXFeedsTestFromId" in FeedManager class
        //It tests if the right string is contained in the latest feeds
        //not working...??
        [Test]
        public void loadLatestXFeedsFromIdTest()
        {
            FeedManager h = new FeedManager();
            List<Feed> loadedFeed = h.LoadLatestXFeedsFromId(230, 3);

            string expected = "Work";
            Assert.Contains(expected, loadedFeed);
        }

        //Test case for Method "LoadLatestXFeedsTestFromId" in FeedManager class
        //It tests if the right string is contained in the latest feeds
        //not working...??
        [Test]
        public void loadLatestXFeedsFromIdTest2()
        {
            FeedManager h = new FeedManager();
            List<Feed> loadedFeed = h.LoadLatestXFeedsFromId(230, 3);
            CollectionAssert.AllItemsAreNotNull(loadedFeed);
        }


        //Test case for Method "LoadLatestXFeedsTestFromId" in FeedManager class
        //It tests if the the loaded feed comment is not empty
        //The test passes only for feeds with comments in the DB e.g., feedID = 85 etc.
        [Test]
        public void loadLatestXFeedsFromIdTest3()
        {
            FeedManager h = new FeedManager();
            List<Comment> loadedFeed = h.LoadFeedComments(85);
            CollectionAssert.IsNotEmpty(loadedFeed);
        }

        //Test case for Method "LoadLatestXFeedsTestFromId" in FeedManager class
        //It tests if the the loaded feed comments contain a certain string
        //PROBLEM: Test passes with wrong values
        [Test]
        public void loadLatestXFeedsFromIdTest4()
        {
            FeedManager h = new FeedManager();
            List<Comment> loadedFeed = h.LoadFeedComments(2);
            string expected = "let us see";
            Assert.Contains(expected, loadedFeed);
        }

        //Test case for Method "LoadLatestXFeedsTestFromId" in FeedManager class
        //It tests if the the loaded feed tag is not empty
        //The test passes only for feeds with tags in the DB e.g., feedID = 1 etc.
        [Test]
        public void loadLatestXFeedsFromIdTest5()
        {
            FeedManager h = new FeedManager();
            List<Human> loadedFeed = h.LoadFeedTags(1);
            CollectionAssert.IsNotEmpty(loadedFeed);
        }

        //Test case for Method "LoadLatestXFeedsTestFromId" in FeedManager class
        //It tests if the the loaded feed tag contains an object e.g., 1 (userId)
        //not working
        [Test]
        public void loadLatestXFeedsFromIdTest6()
        {
            FeedManager h = new FeedManager();
            List<Human> loadedFeed = h.LoadFeedTags(4);
            int expected = 1;
            CollectionAssert.Contains(loadedFeed, expected);
        }

        //Test case for Method "LoadLatestXFeedsTestFromId" in FeedManager class
        //It tests if the the loaded feed tag contains a list of user IDs
        //not working
        [Test]
        public void loadLatestXFeedsFromIdTest7()
        {
            FeedManager h = new FeedManager();
            List<Human> loadedFeed = h.LoadFeedTags(2);
            var list = new List<int>() { 1, 3, 4 };
            CollectionAssert.IsSubsetOf(list, loadedFeed);
        }

        //Test case for Method "LoadLatestXFeedsTestFromId" in FeedManager class
        //It tests if the the loaded feed comment contains a list of user IDs
        //not working

        /* [TestFixture]
         public class TestLoadLatestXFeedsFromId8
         {
             [Test]
             public void loadLatestXFeedsFromIdTest8()
             {
             int hej = 0;
             foreach HumanFeed hf in myHumanFeedList
                 {
                 if(hf.Content.Equals(myString)
                      hej = 1;
                      AssertTrue();
                                     }

                     if(hej != 1)
                     AssertFalse();
             list<string> contents = new list<string> ();

             foreach HumanFeed hf in myHumanFeedList
              { contents.Add(hf.Content);
             AssertEquals(MyString, contents);  }*/

        [Test]
        public void loadLatestXFeedsFromIdTest8()
        {
            FeedManager h = new FeedManager();
            List<Comment> loadedFeed = h.LoadFeedComments(2);
            var list = new List<String>() { "let us see" };
            CollectionAssert.IsSubsetOf(list, loadedFeed);
        }

        //Test case for boolean Method "Publish" in FeedManager class
        //It tests if the tests fails (passes for Assert.IsFalse) when no feed is entered
        [Test]
        public void publishFeedTest()
        {
            FeedManager h = new FeedManager();
            bool loadedHuman = h.PublishFeed(null);
            Assert.IsFalse(loadedHuman);
        }

        //Test case for boolean Method "AddTagToFeed" in FeedManager class
        //It tests if the tests fails when no tag is entered
        [Test]
        public void addTagToFeedTest()
        {
            //needs some correcting, because it needs to addtagto existing feed?

            FeedManager h = new FeedManager();
            HumanFeed newHumanFeed = new HumanFeed();
            CommonDataManager commonDataManager = new CommonDataManager();

            newHumanFeed.FeedType = "Human";
            newHumanFeed.Content = "addTagToFeedTest()";
            newHumanFeed.Location = "Madagascar";
            newHumanFeed.TimeStamp = DateTime.Now;
            newHumanFeed.Category = commonDataManager.GetFeedCategories().Where(c => c.CategoryName == "WorkPost").FirstOrDefault();
            newHumanFeed.Tags = new List<Human>() { loggedInUser };
            newHumanFeed.Owner = loggedInUser;

            Assert.IsTrue(h.PublishFeed(newHumanFeed));
        }

        //Test case for Method "LoadFeedsByType" in FeedManager class
        //It tests if a string is contained in the collection feed list that is loaded by the method
        //Currently not working: problems with loading the content of the feeds
        [TestFixture]
        public class TestLoadFeedsByType
        {
            [Test]
            public void loadFeedsByTypeTest()
            {
                FeedManager h = new FeedManager();
                List<Feed> loadedHuman = h.LoadFeedsByType(FeedType.FeedSource.Human, 1, 1);
                var list = new List<String>() { "hej" };
                CollectionAssert.Contains(loadedHuman, list);
            }

        }

        //Test case for Method "LoadFeedsByDate" in FeedManager class
        //It tests if a string is contained in the collection feed list that is loaded by the method
        //Currently not working:
        [Test]
        public void loadFeedsByDateTest()
        {
            FeedManager h = new FeedManager();
            DateTime d1 = new DateTime(2013, 11, 20, 11, 11, 11);
            DateTime d2 = new DateTime(2013, 11, 20, 11, 11, 13);
            List<Feed> loadedHuman = h.LoadFeedsByDate(DateTime.MinValue, DateTime.MinValue, 1, 4);

            List<String> list = new List<String>() { "hej" };
            CollectionAssert.AreEqual(loadedHuman, list);
        }

        //Test case for Method "LoadFeedsByLocation" in FeedManager class
        //It tests if a string is contained in the collection feed list that is loaded by the method
        //Same problem as two previous methods
        [Test]
        public void loadFeedsByLocationTest()
        {
            FeedManager h = new FeedManager();
            List<Feed> loadedHuman = h.LoadFeedsByLocation("ControlRoom 1A", 1, 9);
            List<String> list = new List<String>() { "hej" };
            CollectionAssert.Contains(loadedHuman, list);
        }

        //Test case for Method "LoadFeedsByUser" in FeedManager class
        //It tests if a string is contained in the collection feed list that is loaded by the method
        //Same problem as previous methods
        [Test]
        public void loadFeedsByUserTest()
        {
            FeedManager h = new FeedManager();
            List<Feed> loadedHuman = h.LoadFeedsByUser(2, 1, 9);
            List<String> list = new List<String>() { "hej" };
            CollectionAssert.Contains(loadedHuman, list);
        }

    }
}
