using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class SensorFeedData : Connection
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public SensorFeedData()
            : base()
        {
            this.sqlConnection = base.GetConnection();
            this.sqlCommand = this.sqlConnection.CreateCommand();
        }

        public DataTable RetrieveSensorFeedsByTime(int sensorID, DateTime startTime, DateTime endTime)
        {
            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetSensorFeedsByTime";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sensor_ID = new SqlParameter("@sensorID", SqlDbType.Int);
                sensor_ID.Direction = ParameterDirection.Input;
                sensor_ID.Value = sensorID == null ? -1 : sensorID;
                this.sqlCommand.Parameters.Add(sensor_ID);

                SqlParameter fromPar = new SqlParameter("@startTime", SqlDbType.DateTime);
                fromPar.Direction = ParameterDirection.Input;
                fromPar.Value = startTime == null ? DateTime.MinValue : startTime;
                this.sqlCommand.Parameters.Add(fromPar);

                SqlParameter toPar = new SqlParameter("@endTime", SqlDbType.DateTime);
                toPar.Direction = ParameterDirection.Input;
                toPar.Value = endTime == null ? DateTime.MinValue : endTime;
                this.sqlCommand.Parameters.Add(toPar);

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(this.sqlCommand);

                da.Fill(table);

                return table;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            finally
            {
                this.sqlCommand.Parameters.Clear();
                this.sqlConnection.Close();
            }
            return null;
        }

        public DataTable RetrieveSensorFeedsFromLocation(string location)
        {
            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetSensorFeedsFromLocation";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter locationParam = new SqlParameter("@location", SqlDbType.NVarChar, 50);
                locationParam.Direction = ParameterDirection.Input;
                locationParam.Value = location == null ? "" : location;
                this.sqlCommand.Parameters.Add(locationParam);

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(this.sqlCommand);

                da.Fill(table);

                return table;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            finally
            {
                this.sqlCommand.Parameters.Clear();
                this.sqlConnection.Close();
            }
            return null;
        }

        public DataTable RetrieveAlarmsFromSensorFeed()
        {
            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetAllAlarms";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(this.sqlCommand);

                da.Fill(table);

                return table;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            finally
            {
                this.sqlCommand.Parameters.Clear();
                this.sqlConnection.Close();
            }
            return null;
        }

        public DataTable RetrieveAllAlarmsByTime(DateTime startTime, DateTime endTime)
        {
            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetAllAlarmsbyTime";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter fromPar = new SqlParameter("@startTime", SqlDbType.DateTime);
                fromPar.Direction = ParameterDirection.Input;
                fromPar.Value = startTime == null ? DateTime.MinValue : startTime;
                this.sqlCommand.Parameters.Add(fromPar);

                SqlParameter toPar = new SqlParameter("@endTime", SqlDbType.DateTime);
                toPar.Direction = ParameterDirection.Input;
                toPar.Value = endTime == null ? DateTime.MinValue : endTime;
                this.sqlCommand.Parameters.Add(toPar);

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(this.sqlCommand);

                da.Fill(table);

                return table;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            finally
            {
                this.sqlCommand.Parameters.Clear();
                this.sqlConnection.Close();
            }
            return null;
        }

        public DataTable RetrieveSensorAlarms(int sensorID)
        {
            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetSensorAlarms";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sensor_ID = new SqlParameter("@sensorID", SqlDbType.Int);
                sensor_ID.Direction = ParameterDirection.Input;
                sensor_ID.Value = sensorID == null ? -1 : sensorID;
                this.sqlCommand.Parameters.Add(sensor_ID);

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(this.sqlCommand);

                da.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    string name = row["SensorName"].ToString();
                    string description = row["SensorID"].ToString();
                    string icoFileName = row["timestamp"].ToString();
                    string installScript = row["Value"].ToString();
                    string installssScript = row["PriorityName"].ToString();

                    Console.WriteLine(name + " " + description + " " + icoFileName + " " + installScript + " " + installssScript);

                }

                return table;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            finally
            {
                this.sqlCommand.Parameters.Clear();
                this.sqlConnection.Close();
            }
            return null;
        }
    }
}
