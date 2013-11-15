using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class HumanFeedData : Connection
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public HumanFeedData()
            : base()
        {
            this.sqlConnection = base.GetConnection();
            this.sqlCommand = this.sqlConnection.CreateCommand();
        }

        public DataTable GetUserFeeds(string userName)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetUserFeeds";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter user_Name = new SqlParameter("@username", SqlDbType.NVarChar, 50);
                user_Name.Direction = ParameterDirection.Input;
                user_Name.Value = userName == null ? "" : userName;
                this.sqlCommand.Parameters.Add(user_Name);

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

        public DataTable GetAllUserFeeds()
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetAllUserFeeds";
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

        public DataTable GetUserFeedsByFilter(string userName, string location, DateTime startingTime, DateTime endingTime)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetUserFeedsByFilter";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter userNameParam = new SqlParameter("@username", SqlDbType.NVarChar, 50);
                userNameParam.Direction = ParameterDirection.Input;
                userNameParam.Value = userName == null ? "" : userName;
                this.sqlCommand.Parameters.Add(userNameParam);

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

        public DataTable GetAllUserFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetAllUserFeedsByFilter";
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

        public object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }
    }

}
