using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
    public class Connection : IConnection
    {
        private SqlConnection sqlConnection;

        public Connection()
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString =
            "Data Source=www3.idt.mdh.se;" +
            "Initial Catalog=ABBConnect;" +
            "User id=rgn09003;" +
            "Password=ABBconnect1;";
        }

        public bool ConnectToDB()
        {
            return false;
        }

        public void OpenConnection()
        {
            sqlConnection.Open();
        }

        public void CloseConnection()
        {
            sqlConnection.Close();
        }

        public SqlConnection GetConnection()
        {
            return this.sqlConnection;
        }
    }

}
