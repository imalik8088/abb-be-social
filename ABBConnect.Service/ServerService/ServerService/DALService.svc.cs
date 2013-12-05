using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

namespace ServerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class DALService : IDALService
    {
        SqlConnection connection;
        SqlCommand command;

        #region Constructor

        public DALService()
        {
            connection = new SqlConnection();
            connection.ConnectionString =
            "Data Source=www3.idt.mdh.se;" +
            "Initial Catalog=ABBConnect;" +
            "User id=rgn09003;" +
            "Password=ABBconnect1;";
        }

        public SqlParameter[] ArrayToSQL(SQLParameter[] parameters)
        {
            // bool outahere = false;
            int count = 0;
            foreach (SQLParameter array in parameters)
                count++;
            SqlParameter[] unner = new SqlParameter[count];
            for (int i = 0; i < count; i++)
            {
                unner[i] = new SqlParameter();
                unner[i].ParameterName = parameters[i].ParamaterName;
                unner[i].SqlDbType = parameters[i].ParameterType;
                int sizeVal = -1;

                //if(parama[i] is DateTime)
                //TypeConverter tc = TypeDescriptor.GetConverter(parama[i].ParamaterValue);

                //try
                //{
                //    sqldbtype slt = (sqldbtype)tc.convertfrom(thetype.name);
                //}
                //catch { }

                //SqlMetadata.InferFromValue();

                if (parameters[i].ParamaterValue is int)
                    unner[i].Value = (int)parameters[i].ParamaterValue;
                else if (parameters[i].ParamaterValue is string)
                    unner[i].Value = (string)parameters[i].ParamaterValue;
                else if (parameters[i].ParamaterValue is DateTime)
                    unner[i].Value = (DateTime)parameters[i].ParamaterValue;

                if (Int32.TryParse(parameters[i].Size, out sizeVal))
                {
                    unner[i].Size = sizeVal;
                }

                unner[i].Direction = parameters[i].ParamaterDirection;
            }
            return unner;
        }
        #endregion

        public void Open()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        public void Close()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        #region Methods
        /// <summary>
        /// Executes the non query. For Insert, Update and Delete
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public int ExecuteNonQueryOutput(string commandText, SQLParameter[] nonparams, ref object output)
        {
            int paramCount = nonparams.Length;
            Open();
            command = new SqlCommand(commandText, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(ArrayToSQL(nonparams));
            int returnValue = command.ExecuteNonQuery();

            output = (int)command.Parameters[nonparams[paramCount-1].ParamaterName].Value;

            command.Parameters.Clear();
            Close();
            return returnValue;
        }

        public int ExecuteNonQuery(string commandText, SQLParameter[] nonparams)
        {
            int paramCount = nonparams.Length;
            Open();
            command = new SqlCommand(commandText, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(ArrayToSQL(nonparams));
            int returnValue = command.ExecuteNonQuery();

            command.Parameters.Clear();
            Close();
            return returnValue;
        }

        public object ExecuteScalar(string commandText, SQLParameter[] nonparams)
        {
            int paramCount = nonparams.Length;
            Open();
            command = new SqlCommand(commandText, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(ArrayToSQL(nonparams));
            object returnValue = command.ExecuteScalar();

            command.Parameters.Clear();
            Close();
            return returnValue;
        }

        public DataSet ExecuteDataSet(string commandText, SQLParameter[] dsparams)
        {

            Open();
            command = new SqlCommand(commandText, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (dsparams != null)
            {
                command.Parameters.AddRange(ArrayToSQL(dsparams));
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command;
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            command.Parameters.Clear();
            Close();

            return dataSet;

        }
        #endregion

    }
}
