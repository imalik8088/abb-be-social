﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Feed
    {
        public Feed()
        {
            this.comments = new List<Comment>();
            this.tags = new List<Human>();
            this.timeStamp = DateTime.MinValue;
            this.location = "";
            this.content = "";
            this.category = new Category();
            this.category.CategoryName = "";
        }

        private int iD;
        public int ID
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
        }

        private DateTime timeStamp;
        public DateTime TimeStamp
        {
            get
            {
                return timeStamp;
            }
            set
            {
                timeStamp = value;
            }
        }

        private List<Human> tags;
        public List<Human> Tags
        {
            get
            {
                return tags;
            }
            set
            {
                tags = value;
            }
        }

        private string location;
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        private string content;
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }

        private Category category;
        public Category Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        private List<Comment> comments;
        public List<Comment> Comments
        {
            get
            {
                return comments;
            }
            set
            {
                comments = value;
            }
        }

    }
}
