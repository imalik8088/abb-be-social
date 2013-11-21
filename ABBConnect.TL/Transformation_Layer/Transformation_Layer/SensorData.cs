using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Transformation_Layer
{
    public class SensorData: ISensorData
    {
        public int RetrieveCurrentSensorData(int sensorID)
        {
            int sensorVal = -1;
            SQLParameter[] parameters = new SQLParameter[1];
            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@sensorID";
            tempParam.ParamaterValue = sensorID;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[0] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            sensorVal = Convert.ToInt32(servcClient.ExecuteScalar("GetLatestSensorValue", parameters));

            servcClient.Close();

            return sensorVal;
        }

        public DataSet GetSensorInformation(int sensorID)
        {
            DataSet sensorInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[1];
            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@sensorID";
            tempParam.ParamaterValue = sensorID;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[0] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            sensorInfo = servcClient.ExecuteDataSet("GetSensorInformation", parameters);

            servcClient.Close();

            return sensorInfo;
        }

        public DataSet RetrieveHistoricalSensorData(int sensorID, DateTime startingTime, DateTime endingTime)
        {
            DataSet humanFeedInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[3];

            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@sensorID";
            tempParam.ParamaterValue = sensorID;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[0] = tempParam;
            tempParam = new SQLParameter();

            if (startingTime == DateTime.MinValue)
                tempParam.ParamaterValue = DBNull.Value;
            else
                tempParam.ParamaterValue = startingTime;

            tempParam.ParamaterName = "@from";
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.DateTime;
            tempParam.ParameterTypeSpecified = true;

            parameters[1] = tempParam;
            tempParam = new SQLParameter();

            if (endingTime == DateTime.MinValue)
                tempParam.ParamaterValue = DBNull.Value;
            else
                tempParam.ParamaterValue = endingTime;

            tempParam.ParamaterName = "@to";
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.DateTime;
            tempParam.ParameterTypeSpecified = true;

            parameters[2] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            humanFeedInfo = servcClient.ExecuteDataSet("GetHistoricalDataFromSensor", parameters);

            servcClient.Close();

            return humanFeedInfo;
        }
    }
}
