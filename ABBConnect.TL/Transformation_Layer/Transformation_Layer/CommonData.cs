using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Transformation_Layer
{
    class CommonData: ICommonData
    {

        public DataSet GetAllLocations()
        {
            DataSet locationInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[0];

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            locationInfo = servcClient.ExecuteDataSet("GetLocations", parameters);

            servcClient.Close();

            return locationInfo;
        }

        public DataSet GetPostGategories()
        {
            DataSet categoryInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[0];

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            categoryInfo = servcClient.ExecuteDataSet("GetPriorityCategories", parameters);

            servcClient.Close();

            return categoryInfo;
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
