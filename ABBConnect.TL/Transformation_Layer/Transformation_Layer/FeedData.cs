using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Transformation_Layer
{
    public class FeedData: IFeedData
    {
        public DataSet GetLatestXFeeds(int numberOfFeeds)
        {
            DataSet feedInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[1];
            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@X";
            tempParam.ParamaterValue = numberOfFeeds;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[0] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            feedInfo = servcClient.ExecuteDataSet("GetLatestXFeeds", parameters);

            servcClient.Close();

            return feedInfo;
        }

        public DataSet GetFeedTags(int feedId)
        {
            DataSet tagInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[1];
            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@feedId";
            tempParam.ParamaterValue = feedId;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[0] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            tagInfo = servcClient.ExecuteDataSet("GetFeedTags", parameters);

            servcClient.Close();

            return tagInfo;
        }

        public DataSet GetFeedComments(int feedId)
        {
            DataSet commentsInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[1];
            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@feedId";
            tempParam.ParamaterValue = feedId;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[0] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            commentsInfo = servcClient.ExecuteDataSet("GetFeedComments", parameters);

            servcClient.Close();

            return commentsInfo;
        }

        public DataSet GetFeedsByFilter(string name, string location, DateTime startingTime, DateTime endingTime, string feedType)
        {
            DataSet latestFeedInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[5];

            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@Name";
            tempParam.ParamaterValue = name;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.NVarChar;
            tempParam.ParameterTypeSpecified = true;
            tempParam.Size = "50";

            parameters[0] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@location";
            tempParam.ParamaterValue = location;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.NVarChar;
            tempParam.ParameterTypeSpecified = true;
            tempParam.Size = "50";

            parameters[1] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@startTime";
            if (startingTime == DateTime.MinValue)
                tempParam.ParamaterValue = null;
            else
                tempParam.ParamaterValue = startingTime;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.DateTime;
            tempParam.ParameterTypeSpecified = true;

            parameters[2] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@endTime";
            if (endingTime == DateTime.MinValue)
                tempParam.ParamaterValue = null;
            else
                tempParam.ParamaterValue = endingTime;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.DateTime;
            tempParam.ParameterTypeSpecified = true;

            parameters[3] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@FeedType";
            tempParam.ParamaterValue = feedType;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.NVarChar;
            tempParam.ParameterTypeSpecified = true;
            tempParam.Size = "50";

            parameters[4] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            latestFeedInfo = servcClient.ExecuteDataSet("GetFeedsByFilter", parameters);

            servcClient.Close();

            return latestFeedInfo;
        }

        public bool PublishComment(int feedID, string username, string comment)
        {
            bool comRes = false;
            int rowRes;

            SQLParameter[] parameters = new SQLParameter[3];
            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@feedId";
            tempParam.ParamaterValue = feedID;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[0] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@username";
            tempParam.ParamaterValue = username;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.NVarChar;
            tempParam.ParameterTypeSpecified = true;
            tempParam.Size = "50";

            parameters[1] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@text";
            tempParam.ParamaterValue = comment;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.NVarChar;
            tempParam.ParameterTypeSpecified = true;

            parameters[2] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            rowRes = servcClient.ExecuteNonQuery("AddCommentToFeed", parameters);
            if (rowRes > 0)
                comRes = true;

            servcClient.Close();

            return comRes;
        }

        public bool IncludeTagFeed(int feedId, string username)
        {
            bool tagRes = false;
            int rowRes;

            SQLParameter[] parameters = new SQLParameter[2];
            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@feedId";
            tempParam.ParamaterValue = feedId;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[0] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@username";
            tempParam.ParamaterValue = username;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.NVarChar;
            tempParam.ParameterTypeSpecified = true;
            tempParam.Size = "50";

            parameters[1] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            rowRes = servcClient.ExecuteNonQuery("AddTagToFeed", parameters);
            if (rowRes > 0)
                tagRes = true;

            servcClient.Close();

            return tagRes;
        }

        public bool PublishFeed(int userId, List<string> tags, string location, string content, string category, string filePath, int priorityID)
        {
            bool result = false;
            object retOutput = -1;

            SQLParameter[] parameters = new SQLParameter[5];

            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@UserId";
            tempParam.ParamaterValue = userId;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[0] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@text";
            tempParam.ParamaterValue = content;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.NVarChar;
            tempParam.ParameterTypeSpecified = true;
            //tempParam.Size = "50";

            parameters[1] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@filePath";
            tempParam.ParamaterValue = filePath;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.NVarChar;
            tempParam.ParameterTypeSpecified = true;
            //tempParam.Size = "50";

            parameters[2] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@feedPriorityId";
            tempParam.ParamaterValue = priorityID;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[3] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@retFeedId";
            tempParam.ParamaterDirection = ParameterDirection.Output;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[4] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            int tempRes = servcClient.ExecuteNonQueryOutput("AddFeed", parameters, ref retOutput);

            servcClient.Close();

            foreach (string taggedUser in tags)
            {
                this.IncludeTagFeed(Convert.ToInt32(retOutput), taggedUser);
            }

            if (tempRes > 0)
                result = true;

            return result;
        }

        public DataSet GetLatestFeedByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            DataSet latestFeedInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[3];

            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@location";
            tempParam.ParamaterValue = location;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.NVarChar;
            tempParam.ParameterTypeSpecified = true;
            tempParam.Size = "50";

            parameters[0] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@startTime";
            if (startingTime == DateTime.MinValue)
                tempParam.ParamaterValue = null;
            else
                tempParam.ParamaterValue = startingTime;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.DateTime;
            tempParam.ParameterTypeSpecified = true;

            parameters[1] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@endTime";
            if (endingTime == DateTime.MinValue)
                tempParam.ParamaterValue = null;
            else
                tempParam.ParamaterValue = endingTime;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.DateTime;
            tempParam.ParameterTypeSpecified = true;

            parameters[2] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            latestFeedInfo = servcClient.ExecuteDataSet("GetLatestFeedsByFilter", parameters);

            servcClient.Close();

            return latestFeedInfo;
        }

        public DataSet GetLatestFeeds()
        {
            DataSet feedInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[0];

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            feedInfo = servcClient.ExecuteDataSet("GetLatestFeeds", parameters);

            servcClient.Close();

            return feedInfo;
        }

        public DataSet GetUserFeeds(int userId)
        {
            DataSet userFeedInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[1];
            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@UserId";
            tempParam.ParamaterValue = userId;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[0] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            userFeedInfo = servcClient.ExecuteDataSet("GetUserFeeds", parameters);

            servcClient.Close();

            return userFeedInfo;
        }

        public DataSet GetUserFeedsByFilter(int userId, string location, DateTime startingTime, DateTime endingTime)
        {
            DataSet userFeedByFilter = new DataSet();
            SQLParameter[] parameters = new SQLParameter[4];
            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@UserId";
            tempParam.ParamaterValue = userId;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[0] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@location";
            tempParam.ParamaterValue = location;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.NVarChar;
            tempParam.ParameterTypeSpecified = true;
            tempParam.Size = "50";

            parameters[1] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@startTime";
            if (startingTime == DateTime.MinValue)
                tempParam.ParamaterValue = null;
            else
                tempParam.ParamaterValue = startingTime;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.DateTime;
            tempParam.ParameterTypeSpecified = true;

            parameters[2] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@endTime";
            if (endingTime == DateTime.MinValue)
                tempParam.ParamaterValue = null;
            else
                tempParam.ParamaterValue = endingTime;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.DateTime;
            tempParam.ParameterTypeSpecified = true;

            parameters[3] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            userFeedByFilter = servcClient.ExecuteDataSet("GetUserFeedsByFilter", parameters);

            servcClient.Close();

            return userFeedByFilter;

        }
    }
}
