using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SensorData : Connection
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public SensorData()
            : base()
        {
            this.sqlConnection = base.GetConnection();
            this.sqlCommand = this.sqlConnection.CreateCommand();
        }

        public DataTable RetrieveSensorInformation(int sensorID)
        {
            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetSensorInformation";
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
                    string name = row["Id"].ToString();
                    string description = row["Name"].ToString();
                    string icoFileName = row["MIN_Critical"].ToString();
                    string installScript = row["MAX_Critical"].ToString();

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

        public DataTable RetrieveHistoricalSensorData(int sensorID, DateTime from, DateTime to)
        {
            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetHistoricalDataFromSensor";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sensor_ID = new SqlParameter("@sensorID", SqlDbType.Int);
                sensor_ID.Direction = ParameterDirection.Input;
                sensor_ID.Value = sensorID == null ? -1 : sensorID;
                this.sqlCommand.Parameters.Add(sensor_ID);

                SqlParameter fromPar = new SqlParameter("@from", SqlDbType.DateTime);
                fromPar.Direction = ParameterDirection.Input;
                fromPar.Value = from == null ? DateTime.MinValue : from;
                this.sqlCommand.Parameters.Add(fromPar);

                SqlParameter toPar = new SqlParameter("@to", SqlDbType.DateTime);
                toPar.Direction = ParameterDirection.Input;
                toPar.Value = to == null ? DateTime.MinValue : to;
                this.sqlCommand.Parameters.Add(toPar);

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(this.sqlCommand);

                da.Fill(table);

                //foreach (DataRow row in table.Rows)
                //{
                //    string name = row["RawValue"].ToString();
                //    string description = row["CreationTimeStamp"].ToString();

                //    Console.WriteLine(name + " " + description);
                //}

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

        public int RetrieveCurrentSensorData(int sensorID)
        {
            int returnedValue = -1;

            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetLatestSensorValue";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sensor_ID = new SqlParameter("@sensorID", SqlDbType.Int);
                sensor_ID.Direction = ParameterDirection.Input;
                sensor_ID.Value = sensorID == null ? -1 : sensorID;
                this.sqlCommand.Parameters.Add(sensor_ID);

                try
                {
                    returnedValue = Convert.ToInt32(this.sqlCommand.ExecuteScalar());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Input string is not a sequence of digits.");
                }
                catch (OverflowException e)
                {
                    Console.WriteLine("The number cannot fit in an Int32.");
                }
            }
            catch (Exception e)
            {
                returnedValue = -1;
            }
            finally
            {
                this.sqlCommand.Parameters.Clear();
                this.sqlConnection.Close();
            }
            return returnedValue;
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
