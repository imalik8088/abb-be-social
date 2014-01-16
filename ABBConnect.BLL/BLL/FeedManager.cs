using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Threading.Tasks;


namespace BLL
{
    /// <summary>
    /// Class that allow to make operation on the feeds like retrieve old feeds, or publish new feeds and comments
    /// </summary>
    public class FeedManager : IFeedManager
    {
        /// <summary>
        /// Class that allow to make operation  on the feeds
        /// </summary>
        FeedData feedData;

        /// <summary>
        /// Constructor that authomaticaly instanciate the attributes of the class
        /// </summary>
        public FeedManager()
        {
            feedData = new FeedData();
        }

        /// <summary>
        /// Method that load the specified number of latest feed
        /// </summary>
        /// <param name="feedNum">number of feeds that want to be retrieved</param>
        /// <returns>List of feeds retrieved</returns>
        public List<Feed> LoadLatestXFeeds(int feedNum)
        {

            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, "", "", -1, feedNum);

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));

            return retList;

        }

        /// <summary>
        /// Method that load the feed with a specified ID, and a specified number of feeds with a bigger ID number
        /// </summary>
        /// <param name="startingId">ID of the feed, where the retrieve start</param>
        /// <param name="numberOfFeeds">Number of feeds to retrieve</param>
        /// <returns></returns>
        public List<Feed> LoadLatestXFeedsFromId(int startingId, int numberOfFeeds)
        {
            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, "", "", startingId, numberOfFeeds);

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;

        }

        /// <summary>
        /// Method that store a feed from a human user
        /// </summary>
        /// <param name="feed">Class that rappresent the feed from the human user that has to be stored</param>
        /// <param name="image">parameter storing the byte array of the image that will be included in the feed</param>
        /// <returns>boolean that inform if the storage of the feed succeed</returns>
        public bool PublishFeed(HumanFeed feed)
        {
            try
            {

                int feedID = feedData.PostFeed(feed.Owner.ID, feed.Content, feed.MediaFilePath, feed.Category.Id);

                bool success = true;
                foreach (Human tempHuman in feed.Tags)
                {
                    success = feedData.AddTag(feedID, tempHuman.UserName);
                }
                return success;
            }
            catch (Exception)
            {

                return false;
            }

        }

        /// <summary>
        /// Method that the reference to a user into a feed
        /// </summary>
        /// <param name="feedId">ID of the feed where the reference should be added</param>
        /// <param name="username">Username of the the user that should be reference in the feed</param>
        /// <returns>boolean that inform if the reference of the feed has succeeded</returns>
        public bool AddTagToFeed(int feedId, string username)
        {
            return feedData.AddTag(feedId, username);

        }

        /// <summary>
        /// Method that retrieve all the comments made to a feed
        /// </summary>
        /// <param name="feedId">ID of the feed that own the comments</param>
        /// <returns>List of comments made to a feed</returns>
        public List<Comment> LoadFeedComments(int feedId)
        {
            List<GetFeedComments_Result> list = feedData.GetFeedComments(feedId);
            List<Comment> retList = new List<Comment>();

            UserManager userInforMng = new UserManager();

            foreach (GetFeedComments_Result res in list)
                retList.Add(new Comment(res, userInforMng.LoadHumanInformationByUsername(res.UserName)));

            return retList;

        }

        /// <summary>
        /// Method that retrieve all human users referenced into a feed
        /// </summary>
        /// <param name="feedId">ID of the feed, where the human users are referenced </param>
        /// <returns>List of human users referenced into the feed</returns>
        public List<Human> LoadFeedTags(int feedId)
        {
            List<GetFeedTags_Result> list = feedData.GetFeedTags(feedId);
            List<Human> retList = new List<Human>();

            foreach (GetFeedTags_Result res in list)
                retList.Add(new Human(res));

            return retList;

        }

        /// <summary>
        /// Method that store a comment made to a feed from a human user
        /// </summary>
        /// <param name="feedID"> ID of the feed that is commented</param>
        /// <param name="comment"> Object that reppresent the comment</param>
        /// <returns>boolean that inform if the storage of the comment has succeeded</returns>
        public bool PublishComment(int feedID, Comment comment)
        {
            return feedData.PostComment(feedID, comment.Owner.UserName, comment.Content);
        }

        /// <summary>
        /// Method that retrieve a specified number of feeds that are of a spcified type.
        /// The type of the feed could be human or sensor
        /// </summary>
        /// <param name="feedType">Class with an enumerative attribute that reppresent the type of the feed: human or sensor</param>
        /// <param name="numFeeds">Number of feeds that have to be retrieved</param>
        /// <returns>List of feeds retrieved</returns>
        public List<Feed> LoadFeedsByType(FeedType.FeedSource feedType, int numFeeds)
        {
            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, feedType.ToString(), "", -1, numFeeds);

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;
        }

        /// <summary>
        /// Method that retrieve a specified number of feeds that are of a spcified type.
        /// The feeds retrieved have an ID equal or bigger to the ID given in input.
        /// The type of the feed could be human or sensor.
        /// </summary>
        /// <param name="feedType">Class with an enumerative attribute that reppresent the type of the feed: human or sensor</param>
        /// <param name="numFeeds">Number of feeds that have to be retrieved</param>
        /// <param name="startId">ID of the feed, where the retrieve start </param>
        /// <returns>List of retrieved feeds</returns>
        public List<Feed> LoadFeedsByType(FeedType.FeedSource feedType, int numFeeds, int startId)
        {
            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, feedType.ToString(), "", startId, numFeeds);

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;
        }

        /// <summary>
        /// Method that retrieve a specified number of feeds included between two dates.
        /// </summary>
        /// <param name="feedStartTime">Class that represent the date where the search begin, it must be older than the date where the search have to stop</param>
        /// <param name="feedEndTime">Class that represent the date where the search end, it must be younger than the date where the search have to start</param>
        /// <param name="numFeeds">numeber of feeds that must be retrieved</param>
        /// <returns>List of feeds retrieved</returns>
        public List<Feed> LoadFeedsByDate(DateTime feedStartTime, DateTime feedEndTime, int numFeeds)
        {
            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, "", feedStartTime, feedEndTime, "", "", -1, numFeeds);

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;
        }

        /// <summary>
        /// Method that retrieve a specified number of feeds included between two dates.
        /// The feeds retrieved have an ID equal or bigger to the ID given in input.
        /// </summary>
        /// <param name="feedStartTime">Class that represent the date where the search begin, it must be older than the date where the search should stop</param>
        /// <param name="feedEndTime">Class that represent the date where the search end, it must be younger than the date where the search should start</param>
        /// <param name="numFeeds">numeber of feeds that must be retrieved</param>
        /// <param name="startId">ID of the feed, where the retrieve start</param>
        /// <returns>List of feeds retrieved</returns>
        public List<Feed> LoadFeedsByDate(DateTime feedStartTime, DateTime feedEndTime, int numFeeds, int startId)
        {
            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, "", feedStartTime, feedEndTime, "", "", startId, numFeeds);

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;
        }

        /// <summary>
        /// Method that retrieve a specified number of feeds by thier location.
        /// </summary>
        /// <param name="location">Location of the feed that must be retrieved</param>
        /// <param name="numFeeds">numeber of feeds that must be retrieved</param>
        /// <returns>List of feeds retrieved</returns>
        public List<Feed> LoadFeedsByLocation(string location, int numFeeds)
        {
            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, location, DateTime.MinValue, DateTime.MinValue, "", "", -1, numFeeds);

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;
        }

        /// <summary>
        /// Method that retrieve a specified number of feeds by thier location.
        /// The feeds retrieved have an ID equal or bigger to the ID given in input.
        /// </summary>
        /// <param name="location">Location of the feed that must be retrieved</param>
        /// <param name="numFeeds">numeber of feeds that must be retrieved</param>
        /// <param name="startId">ID of the feed, where the retrieve start</param>
        /// <returns>List of feeds retrieved</returns>
        public List<Feed> LoadFeedsByLocation(string location, int numFeeds, int startId)
        {
            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, location, DateTime.MinValue, DateTime.MinValue, "", "", startId, numFeeds);

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;
        }

        /// <summary>
        /// Method that retrieve a specified number of feeds by the user that have made tem. 
        /// </summary>
        /// <param name="userId">ID of the user that have made the comment</param>
        /// <param name="numFeeds">numeber of feeds that must be retrieved</param>
        /// <returns>List of feeds retrieved</returns>
        public List<Feed> LoadFeedsByUser(int userId, int numFeeds)
        {
            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(userId, "", DateTime.MinValue, DateTime.MinValue, "", "", -1, numFeeds);

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;
        }

        /// <summary>
        /// Method that retrieve a specified number of feeds by the user that have made tem. 
        /// The feeds retrieved have an ID equal or bigger to the ID given in input.
        /// </summary>
        /// <param name="userId">ID of the user that have made the comment</param>
        /// <param name="numFeeds">Numeber of feeds that must be retrieved</param>
        /// <param name="startId">ID of the feed, where the retrieve start</param>
        /// <returns>List of feeds retrieved</returns>
        public List<Feed> LoadFeedsByUser(int userId, int numFeeds, int startId)
        {
            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(userId, "", DateTime.MinValue, DateTime.MinValue, "", "", startId, numFeeds);

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;
        }

        /// <summary>
        /// Method that search and retrieve the feeds by all their attributes.
        /// If an attribute of the feed is not needed in the search it can be leaved null for string and object, or minor than zero for integers.
        /// </summary>
        /// <param name="userId">ID of the user that have made the comment</param>
        /// <param name="location">Location of the feed that must be retrieved</param>
        /// <param name="feedStartTime">Class that represent the date where the search begin, it must be older than the date where the search should stop</param>
        /// <param name="feedEndTime">Class that represent the date where the search end, it must be younger than the date where the search should start</param>
        /// <param name="feedType">Class with an enumerative attribute that reppresent the type of the feed: human or sensor</param>
        /// <param name="numFeeds">Number of feeds that must be retrieved</param>
        /// <returns>List of feeds retrieved</returns>
        public List<Feed> LoadFeedsByFilter(int userId, string location, DateTime startingTime, DateTime endingTime, FeedType.FeedSource feedType, string categoryName, int numFeeds)
        {
            string typeOfFeed;
            if (feedType == FeedType.FeedSource.None)
                typeOfFeed = "";
            else
                typeOfFeed = feedType.ToString();
            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(userId < 0 ? -1 : userId,
                                                                                 System.String.IsNullOrEmpty(location) ? "" : location,
                                                                                 startingTime,
                                                                                 endingTime,
                                                                                 typeOfFeed,
                                                                                 System.String.IsNullOrEmpty(categoryName) ? "" : categoryName,
                                                                                 -1,
                                                                                 numFeeds < 0 ? -1 : numFeeds
                                                                                 );

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;
        }

        /// <summary>
        /// Method that retrieve the feeds by all their attributes.
        /// If an attribute of the feed is not needed in the search, it can be lived null for string and object, or minor than zero for integers.
        /// </summary>
        /// <param name="userId">ID of the user that have made the comment</param>
        /// <param name="location">Location of the feed that must be retrieved</param>
        /// <param name="feedStartTime">Class that represent the date where the search begin, it must be older than the date where the search should stop</param>
        /// <param name="feedEndTime">Class that represent the date where the search end, it must be younger than the date where the search should start</param>
        /// <param name="feedType">Class with an enumerative attribute that reppresent the type of the feed: human or sensor</param>
        /// <param name="startId">ID of the feed, where the retrieve start</param>
        /// <returns>List of feeds retrieved</returns>
        public List<Feed> LoadFeedsByFilter(int userId, string location, DateTime startingTime, DateTime endingTime, FeedType.FeedSource feedType, string categoryName, int startId, int numFeeds)
        {
            string typeOfFeed;
            if (feedType == FeedType.FeedSource.None)
                typeOfFeed = "";
            else
                typeOfFeed = feedType.ToString();
            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(userId < 0 ? -1 : userId,
                                                                                   String.IsNullOrEmpty(location) ? "" : location,
                                                                                   startingTime,
                                                                                   endingTime,
                                                                                   typeOfFeed,
                                                                                   String.IsNullOrEmpty(categoryName) ? "" : categoryName,
                                                                                   startId < 0 ? -1 : startId,
                                                                                   numFeeds < 0 ? -1 : numFeeds
                                                                                   );

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;

        }

        /// <summary>
        /// Method that retrieves a specific number of feeds, filtered by some attributes specified in the Filter object
        /// </summary>
        /// <param name="savedFilter">The Filter object containing filter information</param>
        /// <param name="numFeed">Number of feeds that must be retrieved</param>
        /// <returns></returns>
        public List<Feed> LoadFeedsFromSavedFilter(Filter savedFilter, int numFeed)
        {
            List<Feed> retList = new List<Feed>();
            UserManager userInforMng = new UserManager();


            if (savedFilter.UsersOnFilter.Count > 0)
                foreach (User filteredUser in savedFilter.UsersOnFilter)
                {
                    retList.AddRange(LoadFeedsByFilter(filteredUser.ID, savedFilter.Location, savedFilter.StartDate, savedFilter.EndDate, savedFilter.TypeOfFeed, savedFilter.CategoryName, numFeed));
                }
            else
                LoadFeedsByFilter(-1, savedFilter.Location, savedFilter.StartDate, savedFilter.EndDate, savedFilter.TypeOfFeed, savedFilter.CategoryName, numFeed);

            return retList.OrderByDescending(o => o.TimeStamp).ToList().GetRange(0, numFeed - 1);
        }

        /// <summary>
        /// Method that retrieves a specific number of feeds starting from a specific feed id. The feeds are 
        /// filtered by some attributes specified in the Filter object
        /// </summary>
        /// <param name="savedFilter">The Filter object containing filter information</param>
        /// <param name="numFeed">Number of feeds that must be retrieved</param>
        /// <param name="startId">ID of the feed, where the retrieve start</param>
        /// <returns></returns>
        public List<Feed> LoadFeedsFromSavedFilter(Filter savedFilter, int numFeed, int startId)
        {
            List<Feed> retList = new List<Feed>();
            UserManager userInforMng = new UserManager();
            //var result = retList;
            //List<string> resultList = result.ToList();
            if (savedFilter.UsersOnFilter.Count > 0)
                foreach (User filteredUser in savedFilter.UsersOnFilter)
                {
                    retList.AddRange(LoadFeedsByFilter(filteredUser.ID, savedFilter.Location, savedFilter.StartDate, savedFilter.EndDate, savedFilter.TypeOfFeed, savedFilter.CategoryName, startId, numFeed));
                }
            else
                LoadFeedsByFilter(-1, savedFilter.Location, savedFilter.StartDate, savedFilter.EndDate, savedFilter.TypeOfFeed, savedFilter.CategoryName, startId, numFeed);

            return retList.OrderByDescending(o => o.TimeStamp).ToList().GetRange(0, numFeed - 1);
        }

        /// <summary>
        /// MEthod that returns one feed identified by its id
        /// </summary>
        /// <param name="feedId">The id of a feed</param>
        /// <returns></returns>
        public Feed GetFeedByFeedId(int feedId)
        {
            GetLatestXFeeds_Result entityFeed = feedData.GetFeedByFeedId(feedId);

            UserManager userInforMng = new UserManager();

            if (entityFeed.Type == "Human")
                return new HumanFeed(entityFeed, LoadFeedComments(entityFeed.FeedId),
                                           LoadFeedTags(entityFeed.FeedId),
                                           userInforMng.LoadHumanInformation(entityFeed.UserId));
            else
                return new SensorFeed(entityFeed, LoadFeedComments(entityFeed.FeedId),
                                            LoadFeedTags(entityFeed.FeedId),
                                            userInforMng.LoadSensorInformation(entityFeed.UserId));

        }

        /// <summary>
        /// Method that returns all the feeds that were posted during the last shift
        /// </summary>
        /// <param name="numFeed">Number of feeds that must be retrieved</param>
        /// <returns></returns>
        public List<Feed> LoadLastShiftFeeds(int numberOfFeeds)
        {
            List<GetLatestXFeeds_Result> list = feedData.GetFeedsFromLastShift(numberOfFeeds);

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;
        }

        /// <summary>
        /// Method that retrieves a specified number of feeds, filtered by category name
        /// </summary>
        /// <param name="categoryName">The category name</param>
        /// <param name="numFeeds">Number of feeds that must be retrieved</param>
        /// <returns></returns>
        public List<Feed> LoadFeedsByCategoryName(string categoryName, int numFeeds)
        {
            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, "", categoryName, -1, numFeeds);

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;
        }

        /// <summary>
        /// Method that retrieves a specified number of feeds starting on a specific id.
        /// The feeds are filtered by category name.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="numFeeds">Number of feeds that must be retrieved</param>
        /// <param name="startId">ID of the feed, where the retrieve start</param>
        /// <returns></returns>
        public List<Feed> LoadFeedsByCategoryName(string categoryName, int numFeeds, int startId)
        {
            List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, "", categoryName, startId, numFeeds);

            List<Feed> retList = new List<Feed>();

            UserManager userInforMng = new UserManager();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId),
                                               LoadFeedTags(res.FeedId),
                                               userInforMng.LoadHumanInformation(res.UserId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId),
                                                LoadFeedTags(res.FeedId),
                                                userInforMng.LoadSensorInformation(res.UserId)));
            }

            return retList;
        }
    }
}
