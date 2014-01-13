using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Class that allow to retrieve additional information shared by the feeds
    /// </summary>
    public class CommonDataManager: ICommonDataManager
    {
        CommonData commonData;

        /// <summary>
        /// Constructor of the class that instantiate the attribute
        /// </summary>
        public CommonDataManager()
        {
            commonData = new CommonData();
        }

        /// <summary>
        /// This method get all locations that compose a specific workspace, like control rooms or offices
        /// </summary>
        /// <returns>return a List of string rappresenting the name of the locations</returns>
        public List<string> GetLocations()
        {
            return commonData.GetLocations();
        }

        /// <summary>
        /// This method  get the category of the feed rappresented by the priority 
        /// 
        /// </summary>
        /// <returns>return List of categories</returns>
        public List<Category> GetFeedCategories()
        {
            List<DAL.GetPriorityCategories_Result> list = commonData.GetCategories();
            List<Category> retList = new List<Category>();

            foreach (GetPriorityCategories_Result res in list)
                retList.Add(new Category(res));

            return retList;
        }
    }
}
