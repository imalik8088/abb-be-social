using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer;
using PortableTransformationLayer.ABBConnectServiceRef;
using System.Threading.Tasks;

namespace BLL
{
    public class CommonDataManager: ICommonDataManager
    {
        Accesser acceser;

        public CommonDataManager()
        {
            acceser = new Accesser();
        }

        public List<string> GetLocations()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetFeedCategories()
        {
            List<GetPriorityCategories_Result> list = await acceser.GetCategories().ConfigureAwait(false);
            List<Category> retList = new List<Category>();

            foreach (GetPriorityCategories_Result res in list)
                retList.Add(new Category(res));

            return retList;
        }
    }
}
