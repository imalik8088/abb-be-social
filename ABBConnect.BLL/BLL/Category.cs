using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    /// <summary>
    /// This class rappresent the category of a feed reporting its main informations
    /// </summary>
    public class Category
    {
        public Category()
        {

        }

        /// <summary>
        /// Constructor of the class that instantiate the attributes with the values given in input
        /// </summary>
        /// <param name="result">Class that rapresent the category of the feed</param>
        public Category(GetPriorityCategories_Result result)
        {
            categoryName = result.Name;
            id = result.Id;
        }

        /// <summary>
        /// attribute of the class that rappresent the name of the category
        /// </summary>
        private string categoryName;
        /// <summary>
        /// Properties that allow to modify or take the value of the attribute categoryName
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

        /// <summary>
        /// Attribute of the class that rappresent the identifier of the category
        /// </summary>
        private int id;
        /// <summary>
        /// Properties that allow to modify or take the value of the ID of the category
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// Attribute of the class that rappresent priority of the feed
        /// </summary>
        private int priority;
        /// <summary>
        /// Properties that allow to modify or take the value of the priority of the feed
        /// </summary>
        public int Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }
    }
}
