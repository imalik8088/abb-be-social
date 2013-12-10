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
                    //NEEDS TO BE CHNAGED WHEN YOU CAN ACCESS ADDCOMMENT AND ADDTAGS
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }

            }

            public async Task<bool> AddTagToFeed(int feedId, string username)
            {
                throw new NotImplementedException();

            }

            public async Task<List<Comment>> LoadFeedComments(int feedId)
            {
                List<GetFeedComments_Result> list = await feedData.GetFeedComments(feedId).ConfigureAwait(false);
                List<Comment> retList = new List<Comment>();

                UserManager userInforMng = new UserManager();

                foreach (GetFeedComments_Result res in list)
                    retList.Add(new Comment(res, await userInforMng.LoadHumanInformationByUsername(res.UserName)));

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
                throw new NotImplementedException();
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


            public Task<List<Feed>> LoadFeedsByFilter(string username, string location, DateTime startingTime, DateTime endingTime, string feedType)
            {
                throw new NotImplementedException();
            }
    }
}
