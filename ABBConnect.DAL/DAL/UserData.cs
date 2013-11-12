using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class UserData : Connection
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        //private SqlDataReader data;

        public UserData()
            : base()
        {
            this.sqlConnection = base.GetConnection();
            this.sqlCommand = this.sqlConnection.CreateCommand();
        }

        public DataTable RetrieveUserInformation(string userName)
        {
            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetUserInformation";
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

        public bool CheckCredentials(string userName, string password)
        {

            bool result = false;
            int returnedValue = -1;

            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "Login";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter user_Name = new SqlParameter("@username", SqlDbType.NVarChar, 50);
                user_Name.Direction = ParameterDirection.Input;
                user_Name.Value = userName == null ? "" : userName;
                this.sqlCommand.Parameters.Add(user_Name);

                SqlParameter user_Password = new SqlParameter("@password", SqlDbType.NVarChar, 50);
                user_Password.Direction = ParameterDirection.Input;
                user_Password.Value = password == null ? "" : password;
                this.sqlCommand.Parameters.Add(user_Password);

                SqlParameter retVal = this.sqlCommand.Parameters.Add("@ret", SqlDbType.Int);
                retVal.Direction = ParameterDirection.Output;

                //this.sqlCommand.Parameters.Add(retVal);

                this.sqlCommand.ExecuteNonQuery();
                returnedValue = (int)this.sqlCommand.Parameters["@ret"].Value;
                result = true;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                this.sqlCommand.Parameters.Clear();
                this.sqlConnection.Close();
            }

            return result;
        }

        public bool UpdateUserData(string userName, string name, string email, string phoneNumber, string location)
        {
            return false;
        }
    }
}
