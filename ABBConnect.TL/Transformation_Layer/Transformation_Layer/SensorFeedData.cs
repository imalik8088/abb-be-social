using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Transformation_Layer
{
    class SensorFeedData: ISensorFeedData
    {
        public DataSet GetAllSensorFeeds()
        {
            DataSet humanFeedInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[0];

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            humanFeedInfo = servcClient.ExecuteDataSet("GetAllSensorFeeds", parameters);

            servcClient.Close();

            return humanFeedInfo;
        }

        public DataSet GetAllSensorFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            DataSet sensorFeedInfo = new DataSet();
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

            sensorFeedInfo = servcClient.ExecuteDataSet("GetAllSensorFeedsByFilter", parameters);

            servcClient.Close();

            return sensorFeedInfo;
        }

        public object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }
    }
}
