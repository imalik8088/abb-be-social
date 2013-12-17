using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;


namespace PortableBLL
{
    public class FeedManager: IFeedManager
    {
            FeedData feedData;

            public FeedManager()
            {
                feedData = new FeedData();
            }

            public async Task<List<Feed>> LoadLatestXFeeds(int feedNum)
            {
                List<GetLatestXFeeds_Result> list = await feedData.GetFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, "", -1, feedNum).ConfigureAwait(false);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                  await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                  await userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false), 
                                                   await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                   await userInforMng.LoadSensorInformation(res.UserId)));

                return retList;

            }

            public async Task<List<Feed>> LoadLatestXFeedsFromId(int startingId, int numberOfFeeds)
            {
                List<GetLatestXFeeds_Result> list = await feedData.GetFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, "", startingId, numberOfFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                  await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                  await userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                   await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                   await userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;

            }

            public async Task<bool> PublishFeed(HumanFeed feed)
            {
                try
                {
                    int feedID = await feedData.PublishFeed(feed.Owner.ID, feed.Content, feed.MediaFilePath, feed.Category.Id).ConfigureAwait(false);

                    bool success = true;
                    foreach(Human tempHuman in feed.Tags)
                    {
                        success = await feedData.AddFeedTag(feedID, tempHuman.UserName);
                    }
                    return success;
                }
                catch (Exception)
                {

                    return false;
                }

            }

            public async Task<bool> AddTagToFeed(int feedId, string username)
            {
                return await feedData.AddFeedTag(feedId, username);

            }

            public async Task<List<Comment>> LoadFeedComments(int feedId)
            {
                List<GetFeedComments_Result> list = await feedData.GetFeedComments(feedId).ConfigureAwait(false);
                List<Comment> retList = new List<Comment>();

                UserManager userInforMng = new UserManager();

                foreach (GetFeedComments_Result res in list)
                    retList.Add(new Comment(res, await userInforMng.LoadHumanInformationByUsername(res.UserName).ConfigureAwait(false)));

                return retList;

            }

            public async Task<List<Human>> LoadFeedTags(int feedId)
            {
                List<GetFeedTags_Result> list = await feedData.GetFeedTags(feedId).ConfigureAwait(false);
                List<Human> retList = new List<Human>();

                foreach (GetFeedTags_Result res in list)
                    retList.Add(new Human(res));

                return retList;

            }

            public async Task<bool> PublishComment(int feedID, Comment comment)
            {
                return await feedData.PublishComment(feedID, comment.Owner.UserName, comment.Content);
            }

            public async Task<List<Feed>> LoadFeedsByType(FeedType.FeedSource feedType, int numFeeds)
            {
                List<GetLatestXFeeds_Result> list = await feedData.GetFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, feedType.ToString(), -1, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false), 
                                                  await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                  await userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                   await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                   await userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public async Task<List<Feed>> LoadFeedsByType(FeedType.FeedSource feedType, int numFeeds, int startId)
            {
                List<GetLatestXFeeds_Result> list = await feedData.GetFeedsByFilter(-1, "", DateTime.MinValue, DateTime.MinValue, feedType.ToString(), startId, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                  await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                  await userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                   await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                   await userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public async Task<List<Feed>> LoadFeedsByDate(DateTime feedStartTime, DateTime feedEndTime, int numFeeds)
            {
                List<GetLatestXFeeds_Result> list = await feedData.GetFeedsByFilter(-1, "", feedStartTime, feedEndTime, "", -1, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                  await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                  await userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                   await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                   await userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public async Task<List<Feed>> LoadFeedsByDate(DateTime feedStartTime, DateTime feedEndTime, int numFeeds, int startId)
            {
                List<GetLatestXFeeds_Result> list = await feedData.GetFeedsByFilter(-1, "", feedStartTime, feedEndTime, "", startId, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                  await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                  await userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                   await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                   await userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public async Task<List<Feed>> LoadFeedsByLocation(string location, int numFeeds)
            {
                List<GetLatestXFeeds_Result> list = await feedData.GetFeedsByFilter(-1, location, DateTime.MinValue, DateTime.MinValue, "", -1, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                  await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                  await userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                   await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                   await userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public async Task<List<Feed>> LoadFeedsByLocation(string location, int numFeeds, int startId)
            {
                List<GetLatestXFeeds_Result> list = await feedData.GetFeedsByFilter(-1, location, DateTime.MinValue, DateTime.MinValue, "", startId, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                  await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                  await userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                   await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                   await userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public async Task<List<Feed>> LoadFeedsByUser(int userId, int numFeeds)
            {
                List<GetLatestXFeeds_Result> list = await feedData.GetFeedsByFilter(userId, "", DateTime.MinValue, DateTime.MinValue, "", -1, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                  await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                  await userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                   await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                   await userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public async Task<List<Feed>> LoadFeedsByUser(int userId, int numFeeds, int startId)
            {
                List<GetLatestXFeeds_Result> list = await feedData.GetFeedsByFilter(userId, "", DateTime.MinValue, DateTime.MinValue, "", startId, numFeeds);

                List<Feed> retList = new List<Feed>();

                UserManager userInforMng = new UserManager();

                foreach (GetLatestXFeeds_Result res in list)
                {
                    if (res.Type == "Human")
                        retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                  await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                  await userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                   await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                   await userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public async Task<List<Feed>> LoadFeedsByFilter(int userId, string location, DateTime startingTime, DateTime endingTime, FeedType.FeedSource feedType, int numFeeds)
            {
                string typeOfFeed;
                if (feedType == FeedType.FeedSource.None)
                    typeOfFeed = "";
                else
                    typeOfFeed = feedType.ToString();
                List<GetLatestXFeeds_Result> list = await feedData.GetFeedsByFilter(userId < 0 ? -1 : userId,
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
                        retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                  await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                  await userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                   await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                   await userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;
            }

            public async Task<List<Feed>> LoadFeedsByFilter(int userId, string location, DateTime startingTime, DateTime endingTime, FeedType.FeedSource feedType, int startId, int numFeeds)
            {
                string typeOfFeed;
                if (feedType == FeedType.FeedSource.None)
                    typeOfFeed = "";
                else
                    typeOfFeed = feedType.ToString();
                List<GetLatestXFeeds_Result> list = await feedData.GetFeedsByFilter(userId < 0 ? -1 : userId,
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
                        retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                  await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                  await userInforMng.LoadHumanInformation(res.UserId)));
                    else
                        retList.Add(new SensorFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false),
                                                   await LoadFeedTags(res.FeedId).ConfigureAwait(false),
                                                   await userInforMng.LoadSensorInformation(res.UserId)));
                }

                return retList;

            }

            public async Task<List<Feed>> LoadFeedsFromSavedFilter(Filter savedFilter, int numFeed)
            {
                List<Feed> retList = new List<Feed>();
                UserManager userInforMng = new UserManager();

                
                if (savedFilter.UsersOnFilter.Count > 0)
                    foreach (User filteredUser in savedFilter.UsersOnFilter)
                    {
                        retList.AddRange(await LoadFeedsByFilter(filteredUser.ID, savedFilter.Location, savedFilter.StartDate, savedFilter.EndDate, savedFilter.TypeOfFeed, numFeed).ConfigureAwait(false));
                    }
                else
                    await LoadFeedsByFilter(-1, savedFilter.Location, savedFilter.StartDate, savedFilter.EndDate, savedFilter.TypeOfFeed, numFeed).ConfigureAwait(false);
                
                return retList.OrderByDescending(o => o.TimeStamp).ToList().GetRange(0, numFeed-1);
            }

            public async Task<List<Feed>> LoadFeedsFromSavedFilter(Filter savedFilter, int numFeed, int startId)
            {
                List<Feed> retList = new List<Feed>();
                UserManager userInforMng = new UserManager();
                //var result = retList;
                //List<string> resultList = result.ToList();
                if (savedFilter.UsersOnFilter.Count > 0)
                    foreach (User filteredUser in savedFilter.UsersOnFilter)
                    {
                        retList.AddRange(await LoadFeedsByFilter(filteredUser.ID, savedFilter.Location, savedFilter.StartDate, savedFilter.EndDate, savedFilter.TypeOfFeed, startId, numFeed).ConfigureAwait(false));
                    }
                else
                    await LoadFeedsByFilter(-1, savedFilter.Location, savedFilter.StartDate, savedFilter.EndDate, savedFilter.TypeOfFeed, startId, numFeed).ConfigureAwait(false);

                return retList.OrderByDescending(o => o.TimeStamp).ToList().GetRange(0, numFeed-1);
            }


            public async Task<Feed> GetFeedByFeedId(int feedId)
            {
                GetLatestXFeeds_Result entityFeed = await feedData.GetFeedByFeedId(feedId).ConfigureAwait(false);

                UserManager userInforMng = new UserManager();

                if (entityFeed.Type == "Human")
                    return new HumanFeed(entityFeed, await LoadFeedComments(entityFeed.FeedId).ConfigureAwait(false),
                                              await LoadFeedTags(entityFeed.FeedId).ConfigureAwait(false),
                                              await userInforMng.LoadHumanInformation(entityFeed.UserId));
                else
                    return new SensorFeed(entityFeed, await LoadFeedComments(entityFeed.FeedId).ConfigureAwait(false),
                                               await LoadFeedTags(entityFeed.FeedId).ConfigureAwait(false),
                                               await userInforMng.LoadSensorInformation(entityFeed.UserId));

            }
    }
}
