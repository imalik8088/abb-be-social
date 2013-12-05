using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class CommonData : Connection
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public CommonData()
            : base()
        {
            this.sqlConnection = base.GetConnection();
            this.sqlCommand = this.sqlConnection.CreateCommand();
        }

        public DataTable GetAllLocations()
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetLocations";
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

        public DataTable GetPostGategories()
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetPriorityCategories";
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
    }
}
