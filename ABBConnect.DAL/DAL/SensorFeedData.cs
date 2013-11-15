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

        public DataTable GetAllSensorFeeds()
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetAllSensorFeeds";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

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

        public DataTable GetAllSensorFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetAllSensorFeedsByFilter";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter locationParam = new SqlParameter("@location", SqlDbType.NVarChar, 50);
                locationParam.Direction = ParameterDirection.Input;
                locationParam.Value = this.ToDBNull(location);
                this.sqlCommand.Parameters.Add(locationParam);

                SqlParameter startingTimeParam = new SqlParameter("@startTime", SqlDbType.DateTime);
                startingTimeParam.Direction = ParameterDirection.Input;

                if (startingTime == DateTime.MinValue)
                    startingTimeParam.Value = DBNull.Value;
                else
                    startingTimeParam.Value = startingTime;

                this.sqlCommand.Parameters.Add(startingTimeParam);

                SqlParameter endingTimeParam = new SqlParameter("@endTime", SqlDbType.DateTime);
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

        public DataTable GetSensorFeeds(int sensorId)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetSensorFeeds";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sensorIdParam = new SqlParameter("@sensorID", SqlDbType.Int);
                sensorIdParam.Direction = ParameterDirection.Input;
                sensorIdParam.Value = sensorId == -1 ? -1 : sensorId;
                this.sqlCommand.Parameters.Add(sensorIdParam);

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

        public DataTable GetSensorFeedsByFilter(int sensorId, string location, DateTime startingTime, DateTime endingTime)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetSensorFeedsByFilter";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sensorIdParam = new SqlParameter("@sensorID", SqlDbType.Int);
                sensorIdParam.Direction = ParameterDirection.Input;
                sensorIdParam.Value = sensorId == -1 ? -1 : sensorId;
                this.sqlCommand.Parameters.Add(sensorIdParam);

                SqlParameter locationParam = new SqlParameter("@location", SqlDbType.NVarChar, 50);
                locationParam.Direction = ParameterDirection.Input;
                locationParam.Value = this.ToDBNull(location);
                this.sqlCommand.Parameters.Add(locationParam);

                SqlParameter startingTimeParam = new SqlParameter("@startTime", SqlDbType.DateTime);
                startingTimeParam.Direction = ParameterDirection.Input;

                if (startingTime == DateTime.MinValue)
                    startingTimeParam.Value = DBNull.Value;
                else
                    startingTimeParam.Value = startingTime;

                this.sqlCommand.Parameters.Add(startingTimeParam);

                SqlParameter endingTimeParam = new SqlParameter("@endTime", SqlDbType.DateTime);
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

        public object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }
    }
}
