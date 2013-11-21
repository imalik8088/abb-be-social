using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ServerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    [ServiceKnownType(typeof(SqlParameter[]))]
    [ServiceKnownType(typeof(object))]
    [ServiceKnownType(typeof(SqlDbType))]
    [ServiceKnownType(typeof(ParameterDirection))]
    //[ServiceKnownType(typeof(SqlDateTime))]
    public interface IDALService
    {
        [OperationContract]
        DataSet ExecuteDataSet(string commandText, SQLParameter[] dsparams);

        [OperationContract]
        int ExecuteNonQueryOutput(string commandText, SQLParameter[] nonparams, ref object output);

        [OperationContract]
        int ExecuteNonQuery(string commandText, SQLParameter[] nonparams);

        [OperationContract]
        object ExecuteScalar(string commandText, SQLParameter[] nonparams);
    }
}
