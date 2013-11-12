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

        public DataTable RetrieveUserFeeds(string userName)
        {
            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetUserFeeds";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter user_Name = new SqlParameter("@username", SqlDbType.NVarChar, 50);
                user_Name.Direction = ParameterDirection.Input;
                user_Name.Value = userName == null ? "" : userName;
                this.sqlCommand.Parameters.Add(user_Name);

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

        public DataTable RetrieveHumanFeedsFromLocation(string location)
        {
            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetUserFeedsByLocation";
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

        public DataTable RetrieveHumanFeedsByTime(DateTime startTime, DateTime endTime)
        {
            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetUserFeedsByTime";
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
    }

}
