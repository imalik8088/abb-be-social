using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Transformation_Layer
{
    public class HumanData: IHumanData
    {

        public bool CheckCredentials(string username, string password)
        {
            bool result = false;
            object retOutput = -1;

            SQLParameter[] parameters = new SQLParameter[3];

            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@username";
            tempParam.ParamaterValue = username;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.NVarChar;
            tempParam.ParameterTypeSpecified = true;
            tempParam.Size = "50";

            parameters[0] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@password";
            tempParam.ParamaterValue = password;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.NVarChar;
            tempParam.ParameterTypeSpecified = true;
            tempParam.Size = "50";

            parameters[1] = tempParam;
            tempParam = new SQLParameter();

            tempParam.ParamaterName = "@ret";
            tempParam.ParamaterDirection = ParameterDirection.Output;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.Int;
            tempParam.ParameterTypeSpecified = true;

            parameters[2] = tempParam;            

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            int tempRes = servcClient.ExecuteNonQueryOutput("Login", parameters, ref retOutput);

            if ((int) retOutput == 1)
                result = true;

            servcClient.Close();

            return result;

        }

        public DataSet GetHumanInformation(int humanId)
        {
            DataSet humanInfo = new DataSet();
            SQLParameter[] parameters = new SQLParameter[1];
            SQLParameter tempParam = new SQLParameter();

            tempParam.ParamaterName = "@UserId";
            tempParam.ParamaterValue = humanId;
            tempParam.ParamaterDirection = ParameterDirection.Input;
            tempParam.ParamaterDirectionSpecified = true;
            tempParam.ParameterType = SqlDbType.NVarChar;
            tempParam.ParameterTypeSpecified = true;

            parameters[0] = tempParam;

            DALServiceClient servcClient = new DALServiceClient();
            servcClient.Open();

            humanInfo = servcClient.ExecuteDataSet("GetHumanInformation", parameters);

            servcClient.Close();

            return humanInfo;
        }
    }
}
