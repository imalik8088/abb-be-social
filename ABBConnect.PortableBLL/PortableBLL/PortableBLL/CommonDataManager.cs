using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace PortableBLL
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
        public async Task<List<string>> GetLocations()
        {
            return await commonData.GetLocations().ConfigureAwait(false);
        }

        /// <summary>
        /// This method  get the category of the feed rappresented by the priority 
        /// 
        /// </summary>
        /// <returns>return List of categories</returns>
        public async Task<List<Category>> GetFeedCategories()
        {
            List<GetPriorityCategories_Result> list = await commonData.GetCategories().ConfigureAwait(false);
            List<Category> retList = new List<Category>();

            foreach (GetPriorityCategories_Result res in list)
                retList.Add(new Category(res));

            return retList;
        }
    }
}
