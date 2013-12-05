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

        public DataTable GetSensorInformation(int sensorID)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetSensorInformation";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sensor_ID = new SqlParameter("@sensorID", SqlDbType.Int);
                sensor_ID.Direction = ParameterDirection.Input;
                sensor_ID.Value = sensorID == -1 ? -1 : sensorID;
                this.sqlCommand.Parameters.Add(sensor_ID);

                this.sqlConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter(this.sqlCommand);

                da.Fill(table);
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
            return table;
        }

        public DataTable RetrieveHistoricalSensorData(int sensorID, DateTime startingTime, DateTime endingTime)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetHistoricalDataFromSensor";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sensor_ID = new SqlParameter("@sensorID", SqlDbType.Int);
                sensor_ID.Direction = ParameterDirection.Input;
                sensor_ID.Value = sensorID == -1 ? -1 : sensorID;
                this.sqlCommand.Parameters.Add(sensor_ID);

                SqlParameter startingTimeParam = new SqlParameter("@from", SqlDbType.DateTime);
                startingTimeParam.Direction = ParameterDirection.Input;

                if (startingTime == DateTime.MinValue)
                    startingTimeParam.Value = DBNull.Value;
                else
                    startingTimeParam.Value = startingTime;

                this.sqlCommand.Parameters.Add(startingTimeParam);

                SqlParameter endingTimeParam = new SqlParameter("@to", SqlDbType.DateTime);
                endingTimeParam.Direction = ParameterDirection.Input;

                if (endingTime == DateTime.MinValue)
                    endingTimeParam.Value = DBNull.Value;
                else
                    endingTimeParam.Value = endingTime;

                this.sqlCommand.Parameters.Add(endingTimeParam);

                this.sqlConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter(this.sqlCommand);

                da.Fill(table);
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
            return table;
        }

        public int RetrieveCurrentSensorData(int sensorID)
        {
            int returnedValue = -1;

            try
            {
                this.sqlCommand.CommandText = "GetLatestSensorValue";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sensor_ID = new SqlParameter("@sensorID", SqlDbType.Int);
                sensor_ID.Direction = ParameterDirection.Input;
                sensor_ID.Value = sensorID == null ? -1 : sensorID;
                this.sqlCommand.Parameters.Add(sensor_ID);

                try
                {
                    this.sqlConnection.Open();
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
    }
}
