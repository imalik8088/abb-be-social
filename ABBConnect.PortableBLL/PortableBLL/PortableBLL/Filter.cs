using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableBLL
{
    /// <summary>
    /// Class that represent a filtering option
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Constructor that authomaticaly instanciate the attributes of the class with null values
        /// </summary>
        public Filter()
        {
            this.iD = -1;
            this.location = "";
            this.name = "";
            this.startDate = DateTime.MinValue;
            this.endDate = DateTime.MinValue;
            this.usersOnFilter = new List<User>();
        }

        /// <summary>
        /// Constructor that instantiate the attributes with the given values
        /// </summary>
        /// <param name="entityFilter">Class containing all the information about a user saved filter</param>
        /// <param name="taggedUsers"></param>
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

        /// <summary>
        /// Attribute that represent  the identificator of the  filter option
        /// </summary>
        private int iD;
        /// <summary>
        /// Properties that allow to modify or take the identificator of the  filter option
        /// </summary>
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

        /// <summary>
        ///  Attribute that represent  the location of the filter option
        /// </summary>
        private string location;
        /// <summary>
        /// Properties that allow to modify or take the location of the filter option
        /// </summary>
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

        /// <summary>
        ///  Attribute that represent  the name of the filter option
        /// </summary>
        private string name;
        /// <summary>
        /// Properties that allow to modify or take the name of the filter option
        /// </summary>
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

        /// <summary>
        ///  Attribute that represent  the date when filter option begin
        /// </summary>
        private DateTime startDate;
        /// <summary>
        /// Properties that allow to modify or take the date when filter option begin
        /// </summary>
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

        /// <summary>
        ///  Attribute that represent  the date when filter option end
        /// </summary>
        private DateTime endDate;
        /// <summary>
        /// Properties that allow to modify or take the date when filter option end
        /// </summary>
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

        /// <summary>
        ///  Attribute that rappresent the type of the user that made the feed
        /// </summary>
        private FeedType.FeedSource typeOfFeed;
        /// <summary>
        /// Properties that allow to modify or take the type of the user that made the feed
        /// </summary>
        public FeedType.FeedSource TypeOfFeed
        {
            get { return typeOfFeed; }
            set { typeOfFeed = value; }
        }

        /// <summary>
        ///  Attribute that list all the users (sensor and human) refered into the filter
        /// </summary>
        private List<User> usersOnFilter;
        /// <summary>
        /// Properties that allow to modify or take the list of all the users (sensor and human) refered into the filter
        /// </summary>
        public List<User> UsersOnFilter
        {
            get { return usersOnFilter;}
        }

        /// <summary>
        /// Attribute for relating the category name.
        /// </summary>
        private string categoryName;
        /// <summary>
        /// Property for relating the category name.
        /// </summary>
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
