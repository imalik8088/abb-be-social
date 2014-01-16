using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    /// <summary>
    /// Class that allow to retrieve additional information shared by the feeds
    /// </summary>
    public class CommonData : Connection
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        
        /// <summary>
        /// Constructor that automatically instantiate the attribute of the class
        /// </summary>
        public CommonData()
            : base()
        {
            this.sqlConnection = base.GetConnection();
            this.sqlCommand = this.sqlConnection.CreateCommand();
        }

        /// <summary>
        /// This method  get the categories of the feeds rappresented by the priority
        /// </summary>
        /// <returns>Asynchronous operation that contain the List of catetegories</returns>
        public List<GetPriorityCategories_Result> GetCategories()
        {
            List<GetPriorityCategories_Result> cats = new List<GetPriorityCategories_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=" + Properties.Resources.Data_Source + "Initial Catalog=" + Properties.Resources.Initial_Catalog + "User id=" + Properties.Resources.User_id + "Password=" + Properties.Resources.Password))
            {

                sqlConn.Open();
                string sqlQuery = "GetPriorityCategories";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetPriorityCategories_Result cat = new GetPriorityCategories_Result();
                            cat.Id = (int)reader[0];
                            cat.Name = (string)reader[1];


                            cats.Add(cat);
                        }
                    }
                }
                sqlConn.Close();
            }
            return cats;
        }

        /// <summary>
        /// This method get all locations that compose a specific workspace
        /// </summary>
        /// <returns>Asynchronous operation that contain the List of Locations</returns>
        public List<string> GetLocations()
        {
            List<string> locations = new List<string>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=" + Properties.Resources.Data_Source + "Initial Catalog=" + Properties.Resources.Initial_Catalog + "User id=" + Properties.Resources.User_id + "Password=" + Properties.Resources.Password)) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetLocations";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            string place = (string)reader[0];

                            locations.Add(place);
                        }
                    }
                }
                sqlConn.Close();
            }
            return locations;
        }
     }
}

