using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Threading.Tasks;

namespace BLL
{
    public class CommonDataManager: ICommonDataManager
    {
        CommonData commonData;

        public CommonDataManager()
        {
            commonData = new CommonData();
        }

        public List<string> GetLocations()
        {
            return commonData.GetLocations();
        }

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
