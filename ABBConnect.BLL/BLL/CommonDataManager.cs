using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BLL
{
    public class CommonDataManager: ICommonDataManager
    {
        private CommonData commonDbData;

        public CommonDataManager()
        {
            this.commonDbData = new CommonData();
        }

        public List<string> GetLocations()
        {
            DataTable locationTable = commonDbData.GetAllLocations();
            List<string> locList = new List<string>();

            foreach (DataRow row in locationTable.Rows)
            {
                string locationVal = row["Place"].ToString();

                if (locationVal == null)
                    locationVal = "";

                locList.Add(locationVal);
            }

            return locList;
        }

        public List<string> GetFeedCategories()
        {
            DataTable categoryTable = commonDbData.GetPostGategories();
            List<string> catList = new List<string>();

            foreach (DataRow row in categoryTable.Rows)
            {
                string category = row["Name"].ToString();

                if (category == null)
                    category = "";

                catList.Add(category);
            }

            return catList;
        }

    }
}
