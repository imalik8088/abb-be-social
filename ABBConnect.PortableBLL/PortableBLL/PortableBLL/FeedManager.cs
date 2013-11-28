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
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId), LoadFeedTags(res.FeedId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId), LoadFeedTags(res.FeedId)));       

            return retList;
           
        }

        public async Task<List<Feed>> LoadLatestXFeedsFromId(int startingId, int numberOfFeeds)
        {
            List<GetLatestXFeeds_Result> list = await acceser.GetLatestXFeedsFromId(numberOfFeeds, startingId);

            List<Feed> retList = new List<Feed>();

            foreach (GetLatestXFeeds_Result res in list)
            {
                if (res.Type == "Human")
                    retList.Add(new HumanFeed(res, LoadFeedComments(res.FeedId), LoadFeedTags(res.FeedId)));
                else
                    retList.Add(new SensorFeed(res, LoadFeedComments(res.FeedId), LoadFeedTags(res.FeedId)));
            }

            return retList;

        }

        public List<Feed> LoadNewFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            throw new NotImplementedException();
           
        }

        public bool PublishFeed(HumanFeed feed)
        {
            try
            {
                int feedID = acceser.PublishFeed(feed.Owner.ID, feed.Content, feed.MediaFilePath, feed.Category.Id);
                //NEEDS TO BE CHNAGED WHEN YOU CAN ACCESS ADDCOMMENT AND ADDTAGS
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool AddTagToFeed(int feedId, string username)
        {
            throw new NotImplementedException();

        }

        public List<Comment> LoadFeedComments(int feedId)
        {
            List<GetFeedComments_Result> list = acceser.GetFeedComments(feedId);
            List<Comment> retList = new List<Comment>();

            foreach (GetFeedComments_Result res in list)
                retList.Add(new Comment(res));

            return retList;

        }

        public List<Human> LoadFeedTags(int feedId)
        {
            List<GetFeedTags_Result> list = acceser.GetFeedTags(feedId);
            List<Human> retList = new List<Human>();

            foreach (GetFeedTags_Result res in list)
                retList.Add(new Human(res));

            return retList;

        }

        public List<Feed> LoadFeedsByFilter(string username, string location, DateTime startingTime, DateTime endingTime, string feedType)
        {
            throw new NotImplementedException();
        }

        public List<Feed> GetUserFeedByFilter(int userId, string location, DateTime startingTime, DateTime endingTime)
        {
            throw new NotImplementedException();
        }

        public List<Feed> GetUserFeeds(int userId)
        {
            throw new NotImplementedException();
        }

        public bool PublishComment(int feedID, Comment comment)
        {
            throw new NotImplementedException();
        }

        public List<Feed> LoadNewFeeds()
        {
            throw new NotImplementedException();
        }
    }
}
