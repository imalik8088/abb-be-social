using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableBLL
{
    public class Filter
    {

        public Filter()
        {
            this.iD = -1;
            this.location = "";
            this.name = "";
            this.startDate = DateTime.MinValue;
            this.endDate = DateTime.MinValue;
            this.usersOnFilter = new List<User>();
        }

        public Filter(PortableTransformationLayer.ABBConnectServiceRef.GetUserSavedFilters_Result entityFilter, List<User> taggedUsers)
        {
            this.iD = entityFilter.ID;
            this.location = entityFilter.Location;
            this.name = entityFilter.FilterName;
            this.startDate = (DateTime) entityFilter.StartDate;
            this.endDate = (DateTime) entityFilter.EndDate;

            if (entityFilter.Type.Equals("Human"))
                this.typeOfFeed = FeedType.FeedSource.Human;
            else if (entityFilter.Type.Equals("Sensor"))
                this.typeOfFeed = FeedType.FeedSource.Sensor;
            else
                this.typeOfFeed = FeedType.FeedSource.None;
            this.usersOnFilter = taggedUsers;
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

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }

        private FeedType.FeedSource typeOfFeed;
        public FeedType.FeedSource TypeOfFeed
        {
            get { return typeOfFeed; }
            set { typeOfFeed = value; }
        }

        private List<User> usersOnFilter;
        public List<User> UsersOnFilter
        {
            get { return usersOnFilter;}
        }

        private string categoryName;
        public string CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
            }
        }
    }
}
