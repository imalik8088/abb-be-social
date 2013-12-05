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

        public UserData()
            : base()
        {
            this.sqlConnection = base.GetConnection();
            this.sqlCommand = this.sqlConnection.CreateCommand();
        }

        public DataTable GetUserInformation(string userName)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetUserInformation";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter user_Name = new SqlParameter("@username", SqlDbType.NVarChar, 50);
                user_Name.Direction = ParameterDirection.Input;
                user_Name.Value = userName == null ? "" : userName;
                this.sqlCommand.Parameters.Add(user_Name);

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

        public bool CheckCredentials(string userName, string password)
        {

            bool result = false;
            int returnedValue = -10000;

            try
            {
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

                this.sqlConnection.Open();
                result = Convert.ToBoolean(this.sqlCommand.ExecuteNonQuery());
                returnedValue = (int)this.sqlCommand.Parameters["@ret"].Value;

                if (returnedValue == 1)
                    result = true;
                else
                    result = false;
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
