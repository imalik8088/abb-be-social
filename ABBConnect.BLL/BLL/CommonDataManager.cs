using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transformation_Layer;
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

        public List<Category> GetFeedCategories()
        {
            DataTable categoryTable = commonDbData.GetPostGategories();
            List<Category> catList = new List<Category>();
            bool castRes = false;
            int tempInt = -1;

            foreach (DataRow row in categoryTable.Rows)
            {
                Category tempCat = new Category();

                castRes = CastStringToInt(row["Id"].ToString(), ref tempInt);
                if (castRes)
                    tempCat.Id = tempInt;

                string categoryName = row["Name"].ToString();

                if (categoryName == null)
                    categoryName = "";
                else
                    tempCat.CategoryName = categoryName;

                if (!tempCat.CategoryName.Contains("Sensor"))
                    catList.Add(tempCat);

            }

            return catList;
        }

        private static bool CastStringToInt(string str, ref int returnValue)
        {
            bool result = false;

            try
            {
                returnValue = Convert.ToInt32(str);
                result = true;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Input string is not a sequence of digits.");
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The number cannot fit in an Int32.");
            }

            return result;
        }

    }
}
