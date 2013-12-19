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

        public List<GetHistoricalDataFromSensor_Result> GetHistoricalDataFromSensor(int id, DateTime startingTime, DateTime endingTime)
        {
            List<GetHistoricalDataFromSensor_Result> histData = new List<GetHistoricalDataFromSensor_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetHistoricalDataFromSensor";


                int iD = id;

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@sensorID", SqlDbType.Int).Value = iD;

                    if (startingTime == DateTime.MinValue)
                        cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = startingTime;

                    if (endingTime == DateTime.MinValue)
                        cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = endingTime;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetHistoricalDataFromSensor_Result senData = new GetHistoricalDataFromSensor_Result();
                            senData.RawValue = (string)reader[0];
                            senData.CreationTimeStamp = (DateTime)reader[1];

                            histData.Add(senData);
                        }
                    }
                }
                sqlConn.Close();
            }
            return histData;
        }

        public bool UnfollowSensor(int humanUserId, int sensorUserId)
        {
            int humanIntId = humanUserId;
            int sensorIntId = sensorUserId;
            int rowsAffected = 0;

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "RemoveFollowSensor";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@HumanUserId", SqlDbType.Int).Value = humanIntId;
                    cmd.Parameters.Add("@SensorUserId", SqlDbType.Int).Value = sensorIntId;

                    rowsAffected = cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }

            if (rowsAffected > 0)
                return true;
            else
                return false;
        }

        public List<int> GetFollowedSensors(int humanUserId)
        {
            List<int> sensorIdList = new List<int>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {
                sqlConn.Open();
                string sqlQuery = "GetFollowedSensors";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        cmd.Parameters.Add("@HumanUserId", SqlDbType.Int).Value = humanUserId;
                    }

                    catch (Exception)
                    {
                        cmd.Dispose();
                        sqlConn.Close();
                        return null;
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            sensorIdList.Add((int)reader[0]);
                        }
                    }
                }
                sqlConn.Close();
            }
            return sensorIdList;
        }

        public GetSensorInformation_Result GetSensorInformation(int id)
        {
            GetSensorInformation_Result s = new GetSensorInformation_Result();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetSensorInformation";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@sensorID", SqlDbType.Int).Value = id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        if (reader.Read())
                        {
                            s.Id = (int)reader[0];
                            s.Unit = (string)reader[1];
                            s.Name = (string)reader[2];
                            s.MIN_Critical = (decimal)reader[3];
                            s.MAX_Critical = (decimal)reader[4];
                        }
                    }
                }
                sqlConn.Close();
            }
            return s;
        }

        public int SaveFilter(int userId, string filterName, DateTime startingTime, DateTime endingTime, string location, string feedType)
        {
            int retValue = -1;

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "AddUserSavedFilter";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                        if (location.Equals(""))
                            cmd.Parameters.Add("@Location", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@Location", SqlDbType.NVarChar, 50).Value = location;

                        if (startingTime == DateTime.MinValue)
                            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startingTime;

                        if (endingTime == DateTime.MinValue)
                            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endingTime;

                        if (feedType.Equals(""))
                            cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 50).Value = feedType;

                        cmd.Parameters.Add("@FilterName", SqlDbType.NVarChar, 50).Value = filterName;
                    }

                    catch (Exception)
                    {
                        cmd.Dispose();
                        sqlConn.Close();
                        return -1;
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        if (reader.Read())
                        {
                            retValue = (int)reader[0];
                        }
                    }
                }

                sqlConn.Close();
            }
            return retValue;
        }

        public List<GetUserSavedFilters_Result> GetSavedFilter(int userId)
        {
            List<GetUserSavedFilters_Result> savedFilters = new List<GetUserSavedFilters_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetUserSavedFilters";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetUserSavedFilters_Result filter = new GetUserSavedFilters_Result();
                            filter.ID = (int)reader[0];
                            filter.UserId = (int)reader[1];
                            filter.FilterName = (string)reader[2];
                            object sqlCell = reader[3];
                            filter.StartDate = (sqlCell == System.DBNull.Value)
                                ? DateTime.MinValue.ToUniversalTime()
                                : Convert.ToDateTime(sqlCell);
                            sqlCell = reader[4];
                            filter.EndDate = (sqlCell == System.DBNull.Value)
                                ? DateTime.MinValue.ToUniversalTime()
                                : Convert.ToDateTime(sqlCell);
                            sqlCell = reader[5];
                            filter.Location = (sqlCell == System.DBNull.Value)
                                ? ""
                                : (string)sqlCell;
                            sqlCell = reader[6];
                            filter.Type = (sqlCell == System.DBNull.Value)
                                ? ""
                                : (string)sqlCell;

                            savedFilters.Add(filter);
                        }
                    }
                }
                sqlConn.Close();
            }
            return savedFilters;
        }

        public List<GetUsersByName_Result> SearchUsersByName(string query)
        {
            List<GetUsersByName_Result> users = new List<GetUsersByName_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetUsersByName";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@query", SqlDbType.NVarChar, 50).Value = query;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetUsersByName_Result user = new GetUsersByName_Result();
                            user.Id = (int)reader[0];
                            user.Name = (string)reader[1];
                            object sqlCell = reader[2];
                            user.FirstName = (sqlCell == System.DBNull.Value)
                                ? ""
                                : (string)sqlCell;
                            sqlCell = reader[3];
                            user.LastName = (sqlCell == System.DBNull.Value)
                                ? ""
                                : (string)sqlCell;
                            user.isHuman = (bool)reader[4];

                            users.Add(user);
                        }
                    }
                }
                sqlConn.Close();
            }
            return users;
        }

        public List<GetUserSavedFiltersTagedUsers_Result> GetFilterTaggedUsers(int filterId)
        {
            List<GetUserSavedFiltersTagedUsers_Result> taggedUsers = new List<GetUserSavedFiltersTagedUsers_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetUserSavedFiltersTagedUsers";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FilterId", SqlDbType.Int).Value = filterId; ;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetUserSavedFiltersTagedUsers_Result user = new GetUserSavedFiltersTagedUsers_Result();
                            user.Id = (int)reader[0];
                            user.Name = (string)reader[1];
                            object sqlCell = reader[2];
                            if (sqlCell != System.DBNull.Value)
                                user.FirstName = (string)sqlCell;
                            sqlCell = reader[3];
                            if (sqlCell != System.DBNull.Value)
                                user.LastName = (string)sqlCell;

                            taggedUsers.Add(user);
                        }
                    }
                }
                sqlConn.Close();
            }
            return taggedUsers;
        }

        public List<GetUserActivity_Result> GetUserActivity(int userId)
        {
            List<GetUserActivity_Result> activityList = new List<GetUserActivity_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {
                sqlConn.Open();
                string sqlQuery = "GetUserActivity";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                    }

                    catch (Exception)
                    {
                        cmd.Dispose();
                        sqlConn.Close();
                        return null;
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetUserActivity_Result userActivity = new GetUserActivity_Result();
                            userActivity.Id = (int)reader[0];
                            userActivity.UserId = (int)reader[1];
                            userActivity.FeedId = (int)reader[2];
                            userActivity.Type = (string)reader[3];
                            userActivity.Text = (string)reader[4];
                            userActivity.Timestamp = (DateTime)reader[5];

                            activityList.Add(userActivity);
                        }
                    }
                }
                sqlConn.Close();
            }
            return activityList;
        }

        public bool AddFilterUser(int userId, int filterId)
        {
            int rowsAffected = 0;

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "AddUserToSavedFilter";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@FilterId", SqlDbType.Int).Value = filterId;

                    rowsAffected = cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }

            if (rowsAffected > 0)
                return true;
            else
                return false;
        }

        public List<GetAllSensors_Result> GetAllSensors()
        {
            List<GetAllSensors_Result> sensors = new List<GetAllSensors_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetAllSensors";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetAllSensors_Result sensor = new GetAllSensors_Result();
                            sensor.Id = (int)reader[0];
                            sensor.Name = (string)reader[1];
                            sensor.MIN_Critical = (decimal)reader[2];
                            sensor.MAX_Critical = (decimal)reader[3];


                            sensors.Add(sensor);
                        }
                    }
                }
                sqlConn.Close();
            }
            return sensors;
        }

        public GetHumanInformation_Result GetHumanInformation(int id)
        {

            GetHumanInformation_Result h = new GetHumanInformation_Result();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetHumanInformation";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        if (reader.Read())
                        {
                            h.Name = (string)reader[0];
                            h.FirstName = (string)reader[1];
                            h.LastName = (string)reader[2];
                            h.PhoneNumber = (string)reader[3];
                            h.Email = (string)reader[4];
                            h.Location = (string)reader[5];
                        }
                    }
                }
                sqlConn.Close();
            }
            return h;
        }

        public GetHumanInformationByUsername_Result GetHumanInformationByUsername(string username)
        {
            GetHumanInformationByUsername_Result h = new GetHumanInformationByUsername_Result();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetHumanInformationByUsername";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = username;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        if (reader.Read())
                        {
                            h.Id = (int)reader[0];
                            h.Name = (string)reader[1];
                            h.FirstName = (string)reader[2];
                            h.LastName = (string)reader[3];
                            h.PhoneNumber = (string)reader[4];
                            h.Email = (string)reader[5];
                            h.Location = (string)reader[6];
                        }
                    }
                }
                sqlConn.Close();
            }
            return h;
        }

        public bool LogIn(string username, string password)
        {
            int result = 0;

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "LogIn";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter("@ret", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = username;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();

                    result = (int)cmd.Parameters["@ret"].Value;
                }
                sqlConn.Close();
            }
            return (result == 1 ? true : false);
        }

        public int GetLastSensorValue(int id)
        {
            int iD = id;
            string value = "";

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetLatestSensorValue";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@sensorID", SqlDbType.Int).Value = iD;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        if (reader.Read())
                        {
                            value = (string)reader[0];
                        }
                    }
                }
                sqlConn.Close();
            }

            int numValue;
            bool parsed = Int32.TryParse(value, out numValue);



            return numValue;
        }

    }
}
