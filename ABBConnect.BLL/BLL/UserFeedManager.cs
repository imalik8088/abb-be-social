using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BackEnd;

namespace BLL
{
    public class UserFeedManager: IUserFeedManager
    {
        private HumanFeedData humanPostDbData;

        public UserFeedManager()
        {
            this.humanPostDbData = new HumanFeedData();
        }

        public List<Feed> LoadUserFeeds(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                DataTable infoTable = humanPostDbData.RetrieveUserFeeds(userName);
                List<Feed> feedList = new List<Feed>();

                foreach (DataRow row in infoTable.Rows)
                {
                    UserFeed tempFeed = new UserFeed();

                    foreach (DataColumn col in infoTable.Columns)
                    {
                        tempFeed.Owner.FirstName = row[col.ColumnName].ToString();
                        tempFeed.Owner.LastName = row["LastName"].ToString();
                        tempFeed.Owner.UserName = row["Username"].ToString();
                        tempFeed.TimeStamp = Convert.ToDateTime(row["CreationTimeStamp"]);
                        tempFeed.Content = row["Text"].ToString();
                        tempFeed.MediaFilePath = row["FilePath"].ToString();
                        tempFeed.Category = row["PriorityCategory"].ToString();

                        int tempInt = CastStringToInt(row["PrioValue"].ToString());
                        if (tempInt != -1)
                        {
                            tempFeed.Priority = tempInt;
                        }

                        tempFeed.Location = row["Place"].ToString();

                        tempInt = CastStringToInt(row["FeedId"].ToString());
                        if (tempInt != -1)
                        {
                            tempFeed.ID = tempInt;
                        }
                    }

                    feedList.Add(tempFeed);
                }

                return feedList;
            }
            return null;
        }

        private static int CastStringToInt(string str)
        {
            int returnValue = -1;

            try
            {
                returnValue = Convert.ToInt32(str);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Input string is not a sequence of digits.");
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The number cannot fit in an Int32.");
            }

            return returnValue;
        }

        public List<Feed> LoadUserFeedsByLocation(string location)
        {
            if (!string.IsNullOrEmpty(location))
            {
                DataTable infoTable = humanPostDbData.RetrieveHumanFeedsFromLocation(location);
                List<Feed> feedList = new List<Feed>();

                foreach (DataRow row in infoTable.Rows)
                {
                    UserFeed tempFeed = new UserFeed();

                    foreach (DataColumn col in infoTable.Columns)
                    {
                        tempFeed.Owner.FirstName = row[col.ColumnName].ToString();
                        tempFeed.Owner.LastName = row["LastName"].ToString();
                        tempFeed.Owner.UserName = row["Username"].ToString();
                        tempFeed.TimeStamp = Convert.ToDateTime(row["CreationTimeStamp"]);
                        tempFeed.Content = row["Text"].ToString();
                        tempFeed.MediaFilePath = row["FilePath"].ToString();
                        tempFeed.Category = row["PriorityCategory"].ToString();

                        int tempInt = CastStringToInt(row["PrioValue"].ToString());
                        if (tempInt != -1)
                        {
                            tempFeed.Priority = tempInt;
                        }

                        tempFeed.Location = row["Place"].ToString();

                        tempInt = CastStringToInt(row["FeedId"].ToString());
                        if (tempInt != -1)
                        {
                            tempFeed.ID = tempInt;
                        }
                    }

                    feedList.Add(tempFeed);
                }

                return feedList;
            }
            return null;
        }

        public List<Feed> LoadUserFeedsByTime(DateTime startTime, DateTime endTime)
        {
            DataTable infoTable = humanPostDbData.RetrieveHumanFeedsByTime(startTime, endTime);
            List<Feed> feedList = new List<Feed>();

            foreach (DataRow row in infoTable.Rows)
            {
                UserFeed tempFeed = new UserFeed();

                foreach (DataColumn col in infoTable.Columns)
                {
                    tempFeed.Owner.FirstName = row[col.ColumnName].ToString();
                    tempFeed.Owner.LastName = row["LastName"].ToString();
                    tempFeed.Owner.UserName = row["Username"].ToString();
                    tempFeed.TimeStamp = Convert.ToDateTime(row["CreationTimeStamp"]);
                    tempFeed.Content = row["Text"].ToString();
                    tempFeed.MediaFilePath = row["FilePath"].ToString();
                    tempFeed.Category = row["PriorityCategory"].ToString();

                    int tempInt = CastStringToInt(row["PrioValue"].ToString());
                    if (tempInt != -1)
                    {
                        tempFeed.Priority = tempInt;
                    }

                    tempFeed.Location = row["Place"].ToString();

                    tempInt = CastStringToInt(row["FeedId"].ToString());
                    if (tempInt != -1)
                    {
                        tempFeed.ID = tempInt;
                    }
                }

                feedList.Add(tempFeed);
            }

            return feedList;
        }
    }
}
