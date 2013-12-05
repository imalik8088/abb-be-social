using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace PortableBLL
{
    public class CommonDataManager: ICommonDataManager
    {
        CommonData commonData;

        public CommonDataManager()
        {
            commonData = new CommonData();
        }

        public async Task<List<string>> GetLocations()
        {
            return await commonData.GetLocations().ConfigureAwait(false);
        }

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
