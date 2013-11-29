using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;


namespace BLL
{
    public class FeedManager: IFeedManager
    {
        Accesser acceser;

        public FeedManager()
        {
            acceser = new Accesser();
        }

        public async Task<List<Feed>> LoadLatestXFeeds(int feedNum)
        {
            List<GetLatestXFeeds_Result> list = await acceser.GetLatestXFeeds(feedNum).ConfigureAwait(false);

            List<Feed> retList = new List<Feed>();

            foreach (GetLatestXFeeds_Result res in list)
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false), await LoadFeedTags(res.FeedId).ConfigureAwait(false)));
                else
                    retList.Add(new SensorFeed(res,  await LoadFeedComments(res.FeedId).ConfigureAwait(false), await LoadFeedTags(res.FeedId).ConfigureAwait(false)));       

            return retList;
           
        }

        public async Task<List<Feed>> LoadLatestXFeedsFromId(int startingId, int numberOfFeeds)
        {
            List<GetLatestXFeeds_Result> list = await acceser.GetLatestXFeedsFromId(numberOfFeeds, startingId);

            List<Feed> retList = new List<Feed>();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false), await LoadFeedTags(res.FeedId).ConfigureAwait(false)));
                else
                    retList.Add(new SensorFeed(res, await LoadFeedComments(res.FeedId).ConfigureAwait(false), await LoadFeedTags(res.FeedId).ConfigureAwait(false)));
            }

            return retList;

        }

        public  async Task<List<Feed>> LoadNewFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            throw new NotImplementedException();
           
        }

        public  async Task<bool> PublishFeed(HumanFeed feed)
        {
            try
            {
                int feedID = await acceser.PublishFeed(feed.Owner.ID, feed.Content, feed.MediaFilePath, feed.Category.Id).ConfigureAwait(false);
                //NEEDS TO BE CHNAGED WHEN YOU CAN ACCESS ADDCOMMENT AND ADDTAGS
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public  async Task<bool> AddTagToFeed(int feedId, string username)
        {
            throw new NotImplementedException();

        }

        public  async Task<List<Comment>> LoadFeedComments(int feedId)
        {
            List<GetFeedComments_Result> list = await acceser.GetFeedComments(feedId).ConfigureAwait(false);
            List<Comment> retList = new List<Comment>();

            foreach (GetFeedComments_Result res in list)
                retList.Add(new Comment(res));

            return retList;

        }

        public  async Task<List<Human>> LoadFeedTags(int feedId)
        {
            List<GetFeedTags_Result> list = await acceser.GetFeedTags(feedId).ConfigureAwait(false);
            List<Human> retList = new List<Human>();

            foreach (GetFeedTags_Result res in list)
                retList.Add(new Human(res));

            return retList;

        }

        public  async Task<List<Feed>> LoadFeedsByFilter(string username, string location, DateTime startingTime, DateTime endingTime, string feedType)
        {
            throw new NotImplementedException();
        }

        public  async Task<List<Feed>> GetUserFeedByFilter(int userId, string location, DateTime startingTime, DateTime endingTime)
        {
            throw new NotImplementedException();
        }

        public  async Task<List<Feed>> GetUserFeeds(int userId)
        {
            throw new NotImplementedException();
        }

        public  async Task<bool> PublishComment(int feedID, Comment comment)
        {
            throw new NotImplementedException();
        }

        public  async Task<List<Feed>> LoadNewFeeds()
        {
            throw new NotImplementedException();
        }
    }
}
