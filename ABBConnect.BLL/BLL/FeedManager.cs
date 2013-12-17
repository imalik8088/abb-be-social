using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Threading.Tasks;


namespace BLL
{
    public class FeedManager: IFeedManager
    {
            FeedData feedData;

            public FeedManager()
            {
                feedData = new FeedData();
            }

            public  List<Feed> LoadLatestXFeeds(int feedNum)
            {
                
                List<GetLatestXFeeds_Result> list =  feedData.GetXFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, "", -1, feedNum);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res,  LoadFeedComments(res.FeedId),
                                                   LoadFeedTags(res.FeedId),
                                                   userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res,  LoadFeedComments(res.FeedId), 
                                                    LoadFeedTags(res.FeedId),
                                                    userInforMng.LoadSensorInformation(res.UserId)));

                return retList;

            }

            public  List<Feed> LoadLatestXFeedsFromId(int startingId, int numberOfFeeds)
            {
                List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, "", startingId, numberOfFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res,  LoadFeedComments(res.FeedId),
                                                   LoadFeedTags(res.FeedId),
                                                   userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res,  LoadFeedComments(res.FeedId),
                                                    LoadFeedTags(res.FeedId),
                                                    userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;

            }

            public  bool PublishFeed(HumanFeed feed)
            {
                try
                {
                    
                    int feedID =  feedData.PostFeed(feed.Owner.ID, feed.Content, feed.MediaFilePath, feed.Category.Id);

                    bool success = true;
                    foreach(Human tempHuman in feed.Tags)
                    {
                        success = feedData.AddTag(feedID, feed.Owner.UserName);
                    }
                    return success;
                }
                catch (Exception)
                {

                    return false;
                }

            }

            public  bool AddTagToFeed(int feedId, string username)
            {
                return  feedData.AddTag(feedId, username);

            }

            public  List<Comment> LoadFeedComments(int feedId)
            {
                List<GetFeedComments_Result> list =  feedData.GetFeedComments(feedId);
                List<Comment> retList = new List<Comment>();

                UserManager userInforMng = new UserManager();

                foreach (GetFeedComments_Result res in list)
                    retList.Add(new Comment(res,  userInforMng.LoadHumanInformationByUsername(res.UserName)));

                return retList;

            }

            public  List<Human> LoadFeedTags(int feedId)
            {
                List<GetFeedTags_Result> list =  feedData.GetFeedTags(feedId);
                List<Human> retList = new List<Human>();

                foreach (GetFeedTags_Result res in list)
                    retList.Add(new Human(res));

                return retList;

            }

            public  bool PublishComment(int feedID, Comment comment)
            {
                return  feedData.PostComment(feedID, comment.Owner.UserName, comment.Content);
            }

            public  List<Feed> LoadFeedsByType(FeedType.FeedSource feedType, int numFeeds)
            {
                List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, feedType.ToString(), -1, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res,  LoadFeedComments(res.FeedId), 
                                                   LoadFeedTags(res.FeedId),
                                                   userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res,  LoadFeedComments(res.FeedId),
                                                    LoadFeedTags(res.FeedId),
                                                    userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public  List<Feed> LoadFeedsByType(FeedType.FeedSource feedType, int numFeeds, int startId)
            {
                List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, feedType.ToString(), startId, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res,  LoadFeedComments(res.FeedId),
                                                   LoadFeedTags(res.FeedId),
                                                   userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res,  LoadFeedComments(res.FeedId),
                                                    LoadFeedTags(res.FeedId),
                                                    userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public  List<Feed> LoadFeedsByDate(DateTime feedStartTime, DateTime feedEndTime, int numFeeds)
            {
                List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, "", feedStartTime, feedEndTime, "", -1, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res,  LoadFeedComments(res.FeedId),
                                                   LoadFeedTags(res.FeedId),
                                                   userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res,  LoadFeedComments(res.FeedId),
                                                    LoadFeedTags(res.FeedId),
                                                    userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public  List<Feed> LoadFeedsByDate(DateTime feedStartTime, DateTime feedEndTime, int numFeeds, int startId)
            {
                List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, "", feedStartTime, feedEndTime, "", startId, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res,  LoadFeedComments(res.FeedId),
                                                   LoadFeedTags(res.FeedId),
                                                   userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res,  LoadFeedComments(res.FeedId),
                                                    LoadFeedTags(res.FeedId),
                                                    userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public  List<Feed> LoadFeedsByLocation(string location, int numFeeds)
            {
                List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, location, DateTime.MinValue, DateTime.MinValue, "", -1, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res,  LoadFeedComments(res.FeedId),
                                                   LoadFeedTags(res.FeedId),
                                                   userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res,  LoadFeedComments(res.FeedId),
                                                    LoadFeedTags(res.FeedId),
                                                    userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public  List<Feed> LoadFeedsByLocation(string location, int numFeeds, int startId)
            {
                List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(-1, location, DateTime.MinValue, DateTime.MinValue, "", startId, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res,  LoadFeedComments(res.FeedId),
                                                   LoadFeedTags(res.FeedId),
                                                   userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res,  LoadFeedComments(res.FeedId),
                                                    LoadFeedTags(res.FeedId),
                                                    userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public  List<Feed> LoadFeedsByUser(int userId, int numFeeds)
            {
                List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(userId, "", DateTime.MinValue, DateTime.MinValue, "", -1, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res,  LoadFeedComments(res.FeedId),
                                                   LoadFeedTags(res.FeedId),
                                                   userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res,  LoadFeedComments(res.FeedId),
                                                    LoadFeedTags(res.FeedId),
                                                    userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public  List<Feed> LoadFeedsByUser(int userId, int numFeeds, int startId)
            {
                List<GetLatestXFeeds_Result> list = feedData.GetXFeedsByFilter(userId, "", DateTime.MinValue, DateTime.MinValue, "", startId, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res,  LoadFeedComments(res.FeedId),
                                                   LoadFeedTags(res.FeedId),
                                                   userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res,  LoadFeedComments(res.FeedId),
                                                    LoadFeedTags(res.FeedId),
                                                    userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public  List<Feed> LoadFeedsByFilter(int userId, string location, DateTime startingTime, DateTime endingTime, FeedType.FeedSource feedType, int numFeeds)
            {
                string typeOfFeed;
                if (feedType == FeedType.FeedSource.None)
                    typeOfFeed = "";
                else
                    typeOfFeed = feedType.ToString();
                List<GetLatestXFeeds_Result> list =  feedData.GetXFeedsByFilter(userId < 0 ? -1 : userId,
                                                                                     System.String.IsNullOrEmpty(location) ? "" : location,
                                                                                     startingTime,
                                                                                     endingTime,
                                                                                     typeOfFeed,
                                                                                     -1,
                                                                                     numFeeds < 0 ? -1 : numFeeds
                                                                                     );

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res,  LoadFeedComments(res.FeedId),
                                                   LoadFeedTags(res.FeedId),
                                                   userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res,  LoadFeedComments(res.FeedId),
                                                    LoadFeedTags(res.FeedId),
                                                    userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public  List<Feed> LoadFeedsByFilter(int userId, string location, DateTime startingTime, DateTime endingTime, FeedType.FeedSource feedType, int startId, int numFeeds)
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
                                                                                       startId < 0 ? -1 : startId,
                                                                                       numFeeds < 0 ? -1 : numFeeds
                                                                                       );

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res,  LoadFeedComments(res.FeedId),
                                                   LoadFeedTags(res.FeedId),
                                                   userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res,  LoadFeedComments(res.FeedId),
                                                    LoadFeedTags(res.FeedId),
                                                    userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;

            }

            public  List<Feed> LoadFeedsFromSavedFilter(Filter savedFilter, int numFeed)
            {
                List<Feed> retList = new List<Feed>();
                UserManager userInforMng = new UserManager();

                
                if (savedFilter.UsersOnFilter.Count > 0)
                    foreach (User filteredUser in savedFilter.UsersOnFilter)
                    {
                        retList.AddRange( LoadFeedsByFilter(filteredUser.ID, savedFilter.Location, savedFilter.StartDate, savedFilter.EndDate, savedFilter.TypeOfFeed, numFeed));
                    }
                else
                     LoadFeedsByFilter(-1, savedFilter.Location, savedFilter.StartDate, savedFilter.EndDate, savedFilter.TypeOfFeed, numFeed);
                
                return retList.OrderByDescending(o => o.TimeStamp).ToList().GetRange(0, numFeed-1);
            }

            public  List<Feed> LoadFeedsFromSavedFilter(Filter savedFilter, int numFeed, int startId)
            {
                List<Feed> retList = new List<Feed>();
                UserManager userInforMng = new UserManager();
                //var result = retList;
                //List<string> resultList = result.ToList();
                if (savedFilter.UsersOnFilter.Count > 0)
                    foreach (User filteredUser in savedFilter.UsersOnFilter)
                    {
                        retList.AddRange( LoadFeedsByFilter(filteredUser.ID, savedFilter.Location, savedFilter.StartDate, savedFilter.EndDate, savedFilter.TypeOfFeed, startId, numFeed));
                    }
                else
                     LoadFeedsByFilter(-1, savedFilter.Location, savedFilter.StartDate, savedFilter.EndDate, savedFilter.TypeOfFeed, startId, numFeed);

                return retList.OrderByDescending(o => o.TimeStamp).ToList().GetRange(0, numFeed-1);
            }


            public  Feed GetFeedByFeedId(int feedId)
            {
                GetLatestXFeeds_Result entityFeed =  feedData.GetFeedByFeedId(feedId);

                UserManager userInforMng = new UserManager();

                if (entityFeed.Type == "Human")
                    return new HumanFeed(entityFeed,  LoadFeedComments(entityFeed.FeedId),
                                               LoadFeedTags(entityFeed.FeedId),
                                               userInforMng.LoadHumanInformation(entityFeed.UserId));
                else
                    return new SensorFeed(entityFeed,  LoadFeedComments(entityFeed.FeedId),
                                                LoadFeedTags(entityFeed.FeedId),
                                                userInforMng.LoadSensorInformation(entityFeed.UserId));

            }
    }
}
