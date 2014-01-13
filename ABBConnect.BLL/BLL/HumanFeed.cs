
using System.Collections.Generic;
using DAL;

namespace BLL
{
    public class HumanFeed: Feed
    {
        public HumanFeed()
        {
            owner = new Human();
            mediaFilePath = "";
        }

        public HumanFeed(GetLatestXFeeds_Result res, List<Comment> listCom, List<Human> listTag, Human owner)
        {
            base.Category = new Category();
            base.Comments = new List<Comment>();
            base.Tags = new List<Human>();
            this.owner = new Human();

            base.ID = res.FeedId;
            base.Comments = listCom;
            base.Tags = listTag;
            base.TimeStamp = res.CreationTimeStamp;
            base.Location = res.Location;
            base.Content = res.Text;
            base.Category.CategoryName = res.PrioCategory;
            base.Category.Priority = res.PrioValue;

            this.owner = owner;
            this.owner.ID = res.UserId;
            
            mediaFilePath = res.FilePath;

        }


        private Human owner;
        public Human Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        private string mediaFilePath;
        public string MediaFilePath
        {
            get
            {
                return mediaFilePath;
            }
            set
            {
                mediaFilePath = value;
            }
        }
    }
}
