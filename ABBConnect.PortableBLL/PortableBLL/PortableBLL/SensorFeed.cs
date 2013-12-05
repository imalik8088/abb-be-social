﻿using System.Collections.Generic;
using PortableTransformationLayer.ABBConnectServiceRef;

namespace PortableBLL
{
    public class SensorFeed: Feed
    {
        public SensorFeed()
        {
            owner = new Sensor();
        }

        public SensorFeed(GetLatestXFeeds_Result res, List<Comment> listCom, List<Human> listTag, Sensor owner)
        {
            base.Category = new Category();
            base.Comments = new List<Comment>();
            base.Tags = new List<Human>();
            this.owner = new Sensor();

            base.ID = res.FeedId;
            base.Comments = listCom;
            base.Tags = listTag;
            base.TimeStamp = res.CreationTimeStamp;
            base.Location = res.Location;
            base.Content = res.Text;
            base.Category.CategoryName = res.PrioCategory;
            base.Category.Priority = res.PrioValue;

            this.owner = owner;

        }

        

        private Sensor owner;
        public Sensor Owner
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
    }
}
