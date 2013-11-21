using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transformation_Layer;
using System.Data;

namespace BLL
{
    public class SensorFeedManager: ISensorFeedManager
    {
        private SensorFeedData senFeedDbData;

        public SensorFeedManager()
        {
            senFeedDbData = new SensorFeedData();
        }

        public List<SensorFeed> GetAllSensorFeeds()
        {
            DataSet senFeedSet = senFeedDbData.GetAllSensorFeeds();
            List<SensorFeed> lsSenFeed = new List<SensorFeed>();
            SensorFeed senFeed = new SensorFeed();
            int tempInt = 0;
            bool castRes = false;

            DataTableCollection senFeedCollection = senFeedSet.Tables;
            DataTable senFeedTable = senFeedCollection[0];

            foreach (DataRow row in senFeedTable.Rows)
            {
                StringBuilder tempContainer = new StringBuilder();
                tempContainer.Append(row["Location"].ToString());

                if (tempContainer.ToString() == null)
                    senFeed.Location = "";
                else
                    senFeed.Location = tempContainer.ToString();

                tempContainer.Clear();
                tempContainer.Append(row["Text"].ToString());

                if (tempContainer.ToString() == null)
                    senFeed.Content = "";
                else
                    senFeed.Content = tempContainer.ToString();

                tempContainer.Clear();
                tempContainer.Append(row["CreationTimeStamp"].ToString());

                if (tempContainer.ToString() == null)
                    senFeed.TimeStamp = DateTime.MinValue;
                else
                    senFeed.TimeStamp = Convert.ToDateTime(tempContainer.ToString());

                tempContainer.Clear();
                tempContainer.Append(row["SensorName"].ToString());

                if (tempContainer.ToString() == null)
                    senFeed.Owner.Name = "";
                else
                    senFeed.Owner.Name = tempContainer.ToString();

                tempContainer.Clear();
                tempContainer.Append(row["Id"].ToString());

                castRes = CastStringToInt(tempContainer.ToString(), ref tempInt);
                if (castRes)
                    senFeed.Owner.ID = tempInt;

                tempContainer.Clear();
                tempContainer.Append(row["PrioValue"].ToString());

                castRes = CastStringToInt(tempContainer.ToString(), ref tempInt);
                if (castRes)
                    senFeed.Priority = tempInt;

                tempContainer.Clear();
                tempContainer.Append(row["PrioCategory"].ToString());

                if (tempContainer.ToString() == null)
                    senFeed.Category = "";
                else
                    senFeed.Category = tempContainer.ToString();

                tempContainer.Clear();
                lsSenFeed.Add(senFeed);
                senFeed = new SensorFeed();
            }

            return lsSenFeed;
        }

        public List<SensorFeed> GetAllSensorFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            if (String.IsNullOrEmpty(location))
                location = null;

            DataSet senFeedSet = senFeedDbData.GetAllSensorFeedsByFilter(location, startingTime, endingTime);
            List<SensorFeed> lsSenFeed = new List<SensorFeed>();
            SensorFeed senFeed = new SensorFeed();
            int tempInt = 0;
            bool castRes = false;

            DataTableCollection senFeedCollection = senFeedSet.Tables;
            DataTable senFeedTable = senFeedCollection[0];

            foreach (DataRow row in senFeedTable.Rows)
            {
                StringBuilder tempContainer = new StringBuilder();
                tempContainer.Append(row["Location"].ToString());

                if (tempContainer.ToString() == null)
                    senFeed.Location = "";
                else
                    senFeed.Location = tempContainer.ToString();

                tempContainer.Clear();
                tempContainer.Append(row["Text"].ToString());

                if (tempContainer.ToString() == null)
                    senFeed.Content = "";
                else
                    senFeed.Content = tempContainer.ToString();

                tempContainer.Clear();
                tempContainer.Append(row["CreationTimeStamp"].ToString());

                if (tempContainer.ToString() == null)
                    senFeed.TimeStamp = DateTime.MinValue;
                else
                    senFeed.TimeStamp = Convert.ToDateTime(tempContainer.ToString());

                tempContainer.Clear();
                tempContainer.Append(row["SensorName"].ToString());

                if (tempContainer.ToString() == null)
                    senFeed.Owner.Name = "";
                else
                    senFeed.Owner.Name = tempContainer.ToString();

                tempContainer.Clear();
                tempContainer.Append(row["Id"].ToString());

                castRes = CastStringToInt(tempContainer.ToString(), ref tempInt);
                if (castRes)
                    senFeed.Owner.ID = tempInt;

                tempContainer.Clear();
                tempContainer.Append(row["PrioValue"].ToString());

                castRes = CastStringToInt(tempContainer.ToString(), ref tempInt);
                if (castRes)
                    senFeed.Priority = tempInt;

                tempContainer.Clear();
                tempContainer.Append(row["PrioCategory"].ToString());

                if (tempContainer.ToString() == null)
                    senFeed.Category = "";
                else
                    senFeed.Category = tempContainer.ToString();

                tempContainer.Clear();
                lsSenFeed.Add(senFeed);
                senFeed = new SensorFeed();
            }

            return lsSenFeed;
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
