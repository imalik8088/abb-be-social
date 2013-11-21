using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Transformation_Layer
{
    class HumanFeedData: IHumanFeedData
    {
        public DataSet GetAllHumanFeeds()
        {
            DataSet humanFeedInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[0];

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            humanFeedInfo = servcClient.ExecuteDataSet("GetAllHumanFeeds", parameters);

            servcClient.Close();

            return humanFeedInfo;
        }

        public DataSet GetAllHumanFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            DataSet humanFeedInfo = new DataSet();
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

            if (startingTime == DateTime.MinValue)
                tempParam.ParamaterValue = null;
            else
                tempParam.ParamaterValue = startingTime;

            tempParam.ParamaterName = "@startTime";
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.DateTime;
            tempParam.ParameterTypeSpecified = true;

            parameters[1] = tempParam;
            tempParam = new SQLParameter();

            if (endingTime == DateTime.MinValue)
                tempParam.ParamaterValue = null;
            else
                tempParam.ParamaterValue = endingTime;

            tempParam.ParamaterName = "@endTime";
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.DateTime;
            tempParam.ParameterTypeSpecified = true;

            parameters[2] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            humanFeedInfo = servcClient.ExecuteDataSet("GetAllHumanFeedsByFilter", parameters);

            servcClient.Close();

            return humanFeedInfo;
        }

        public object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }
    }
}
