using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class FeedData : Connection
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public FeedData()
            : base()
        {
            this.sqlConnection = base.GetConnection();
            this.sqlCommand = this.sqlConnection.CreateCommand();
        }



        List<GetLatestXFeeds_Result> GetLatestXFeeds(string X)
        {
            List<GetLatestXFeeds_Result> feeds = new List<GetLatestXFeeds_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetLatestXFeeds";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@X", SqlDbType.Int).Value = int.Parse(X);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetLatestXFeeds_Result feed = new GetLatestXFeeds_Result();
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            feed.FilePath = (string)reader[5] != null ? (string)reader[5] : "";
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];

                            feeds.Add(feed);
                        }
                    }
                }
                sqlConn.Close();
            }
            return feeds;
        }

        List<GetLatestXFeedsFromId_Result> GetLatestXFeedsFromId(string X, string Id)
        {
            List<GetLatestXFeedsFromId_Result> feeds = new List<GetLatestXFeedsFromId_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetLatestXFeedsFromId";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@X", SqlDbType.NVarChar).Value = X;
                    cmd.Parameters.Add("@feedID", SqlDbType.NVarChar).Value = Id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetLatestXFeedsFromId_Result feed = new GetLatestXFeedsFromId_Result();
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            feed.FilePath = (string)reader[5] != null ? (string)reader[5] : "";
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];

                            feeds.Add(feed);
                        }
                    }
                }
                sqlConn.Close();
            }
            return feeds;
        }

        public int PostFeed(string id, string text, string filepath, string prioId)
        {
            int result = 0;

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "AddFeed";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter("@retFeedId", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@text", SqlDbType.NVarChar).Value = text;
                    cmd.Parameters.Add("@filePath", SqlDbType.NVarChar).Value = filepath;
                    cmd.Parameters.Add("@feedPriorityId", SqlDbType.Int).Value = prioId;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();

                    result = (int)cmd.Parameters["@retFeedId"].Value;
                }
                sqlConn.Close();
            }
            return result;
        }

        public List<GetFeedComments_Result> GetFeedComments(string feedId, string randomGuid)
        {
            List<GetFeedComments_Result> comments = new List<GetFeedComments_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetFeedComments";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@feedId", SqlDbType.Int).Value = feedId;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetFeedComments_Result comment = new GetFeedComments_Result();
                            comment.FirstName = (string)reader[0];
                            comment.LastName = (string)reader[1];
                            comment.UserName = (string)reader[2];
                            comment.CommentText = (string)reader[3];
                            comment.CreationTimeStamp = (DateTime)reader[4];


                            comments.Add(comment);
                        }
                    }
                }
                sqlConn.Close();
            }
            return comments;
        }

        public List<GetFeedTags_Result> GetFeedTags(string feedId)
        {
            List<GetFeedTags_Result> tags = new List<GetFeedTags_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetFeedTags";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@feedId", SqlDbType.Int).Value = feedId;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            try
                            {
                                GetFeedTags_Result tag = new GetFeedTags_Result();
                                tag.UserName = (string)reader[0];
                                tag.FirstName = (string)reader[1];
                                tag.LastName = (string)reader[2];
                                tag.UserId = (int)reader[3];

                                tags.Add(tag);
                            }
                            catch
                            {
                                break;
                            }
                        }
                    }
                }
                sqlConn.Close();
            }
            return tags;
        }

        List<GetAllHumanFeeds_Result> GetHumanFeeds()
        {
            List<GetAllHumanFeeds_Result> feeds = new List<GetAllHumanFeeds_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetAllHumanFeeds";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetAllHumanFeeds_Result feed = new GetAllHumanFeeds_Result();
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            feed.FilePath = (string)reader[5] != null ? (string)reader[5] : "";
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];

                            feeds.Add(feed);
                        }
                    }
                }
                sqlConn.Close();
            }
            return feeds;
        }

        List<GetAllHumanFeedsByFilter_Result> GetHumanFeedsByFilter(string location, string startingTime, string endingTime)
        {
            List<GetAllHumanFeedsByFilter_Result> feeds = new List<GetAllHumanFeedsByFilter_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetAllHumanFeedsByFilter";
                DateTime minValue = DateTime.MinValue;

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@location", SqlDbType.NVarChar, 50).Value = location;

                    if (startingTime.Equals(minValue.ToString()))
                        cmd.Parameters.Add("@startTime", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@startTime", SqlDbType.DateTime).Value = Convert.ToDateTime(startingTime);

                    if (endingTime.Equals(minValue.ToString()))
                        cmd.Parameters.Add("@endTime", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@endTime", SqlDbType.DateTime).Value = Convert.ToDateTime(endingTime);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetAllHumanFeedsByFilter_Result feed = new GetAllHumanFeedsByFilter_Result();
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            feed.FilePath = (string)reader[5] != null ? (string)reader[5] : "";
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];

                            feeds.Add(feed);
                        }
                    }
                }
                sqlConn.Close();
            }
            return feeds;
        }

        List<GetAllSensorFeeds_Result> GetSensorFeeds()
        {
            List<GetAllSensorFeeds_Result> feeds = new List<GetAllSensorFeeds_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetAllSensorFeeds";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetAllSensorFeeds_Result feed = new GetAllSensorFeeds_Result();
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            feed.FilePath = (string)reader[5] != null ? (string)reader[5] : "";
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];

                            feeds.Add(feed);
                        }
                    }
                }
                sqlConn.Close();
            }
            return feeds;
        }

        List<GetAllSensorFeedsByFilter_Result> GetSensorFeedsByFilter(string location, string startingTime, string endingTime)
        {
            List<GetAllSensorFeedsByFilter_Result> feeds = new List<GetAllSensorFeedsByFilter_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetAllSensorFeedsByFilter";
                DateTime minValue = DateTime.MinValue;

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@location", SqlDbType.NVarChar, 50).Value = location;

                    if (startingTime.Equals(minValue.ToString()))
                        cmd.Parameters.Add("@startTime", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@startTime", SqlDbType.DateTime).Value = Convert.ToDateTime(startingTime);

                    if (endingTime.Equals(minValue.ToString()))
                        cmd.Parameters.Add("@endTime", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@endTime", SqlDbType.DateTime).Value = Convert.ToDateTime(endingTime);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetAllSensorFeedsByFilter_Result feed = new GetAllSensorFeedsByFilter_Result();
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            feed.FilePath = (string)reader[5] != null ? (string)reader[5] : "";
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];

                            feeds.Add(feed);
                        }
                    }
                }
                sqlConn.Close();
            }
            return feeds;
        }

        List<GetUserFeeds_Result> GetUserFeeds()
        {
            List<GetUserFeeds_Result> feeds = new List<GetUserFeeds_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetUserFeeds";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetUserFeeds_Result feed = new GetUserFeeds_Result();
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            feed.FilePath = (string)reader[5] != null ? (string)reader[5] : "";
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];

                            feeds.Add(feed);
                        }
                    }
                }
                sqlConn.Close();
            }
            return feeds;
        }

        List<GetUserFeedsByFilter_Result> GetUserFeedsByFilter(string location, string startingTime, string endingTime)
        {
            List<GetUserFeedsByFilter_Result> feeds = new List<GetUserFeedsByFilter_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetUserFeedsByFilter";

                DateTime minValue = DateTime.MinValue;

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@location", SqlDbType.NVarChar, 50).Value = location;

                    if (startingTime.Equals(minValue.ToString()))
                        cmd.Parameters.Add("@startTime", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@startTime", SqlDbType.DateTime).Value = Convert.ToDateTime(startingTime);

                    if (endingTime.Equals(minValue.ToString()))
                        cmd.Parameters.Add("@endTime", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@endTime", SqlDbType.DateTime).Value = Convert.ToDateTime(endingTime);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetUserFeedsByFilter_Result feed = new GetUserFeedsByFilter_Result();
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            feed.FilePath = (string)reader[5] != null ? (string)reader[5] : "";
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];

                            feeds.Add(feed);
                        }
                    }
                }
                sqlConn.Close();
            }
            return feeds;
        }

        List<GetHistoricalDataFromSensor_Result> GetHistoricalDataFromSensor(string id, string startingTime, string endingTime)
        {
            List<GetHistoricalDataFromSensor_Result> histData = new List<GetHistoricalDataFromSensor_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetHistoricalDataFromSensor";

                DateTime minValue = DateTime.MinValue;

                int iD = Int32.Parse(id);

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@sensorID", SqlDbType.Int).Value = iD;

                    if (startingTime.Equals(minValue.ToString()))
                        cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = Convert.ToDateTime(startingTime);

                    if (endingTime.Equals(minValue.ToString()))
                        cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = Convert.ToDateTime(endingTime);

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

        public int GetLastSensorValue(string id)
        {
            int iD = Int32.Parse(id);
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

        public bool PostComment(string feedId, string username, string text)
        {
            int iD = Int32.Parse(feedId);
            int rowsAffected = 0;

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "AddCommentToFeed";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@feedId", SqlDbType.Int).Value = iD;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = username;
                    cmd.Parameters.Add("@text", SqlDbType.NVarChar).Value = text;

                    rowsAffected = cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }

            if (rowsAffected > 0)
                return true;
            else
                return false;
        }

        public bool AddTag(string feedId, string username)
        {
            int iD = Int32.Parse(feedId);
            int rowsAffected = 0;

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "AddTagToFeed";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@feedId", SqlDbType.Int).Value = iD;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = username;

                    rowsAffected = cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }

            if (rowsAffected > 0)
                return true;
            else
                return false;
        }

        List<GetLatestFeedsByFilter_Result> GetLatestFeedsByFilter(string location, string startingTime, string endingTime)
        {
            List<GetLatestFeedsByFilter_Result> feeds = new List<GetLatestFeedsByFilter_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetLatestFeedsByFilter";
                DateTime minValue = DateTime.MinValue;

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@location", SqlDbType.NVarChar, 50).Value = location;

                    if (startingTime.Equals(minValue.ToString()))
                        cmd.Parameters.Add("@startTime", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@startTime", SqlDbType.DateTime).Value = Convert.ToDateTime(startingTime);

                    if (endingTime.Equals(minValue.ToString()))
                        cmd.Parameters.Add("@endTime", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@endTime", SqlDbType.DateTime).Value = Convert.ToDateTime(endingTime);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetLatestFeedsByFilter_Result feed = new GetLatestFeedsByFilter_Result();
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            feed.FilePath = (string)reader[5] != null ? (string)reader[5] : "";
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];

                            feeds.Add(feed);
                        }
                    }
                }
                sqlConn.Close();
            }
            return feeds;
        }

        List<GetFeedsByFilter_Result> GetFeedsByFilter(string name, string location, string startingTime, string endingTime, string feedType)
        {
            List<GetFeedsByFilter_Result> feeds = new List<GetFeedsByFilter_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetFeedsByFilter";
                DateTime minValue = DateTime.MinValue;

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                    cmd.Parameters.Add("@location", SqlDbType.NVarChar, 50).Value = location;

                    if (startingTime.Equals(minValue.ToString()))
                        cmd.Parameters.Add("@startTime", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@startTime", SqlDbType.DateTime).Value = Convert.ToDateTime(startingTime);

                    if (endingTime.Equals(minValue.ToString()))
                        cmd.Parameters.Add("@endTime", SqlDbType.DateTime).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@endTime", SqlDbType.DateTime).Value = Convert.ToDateTime(endingTime);

                    cmd.Parameters.Add("@FeedType", SqlDbType.NVarChar, 50).Value = feedType;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetFeedsByFilter_Result feed = new GetFeedsByFilter_Result();
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            feed.FilePath = (string)reader[5] != null ? (string)reader[5] : "";
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];

                            feeds.Add(feed);
                        }
                    }
                }
                sqlConn.Close();
            }
            return feeds;
        }

        List<GetLatestXFeeds_Result> GetXFeedsByFilter(string id, string location, string startingTime, string endingTime, string feedType, string startId, string numFeeds, string randomGuid)
        {
            List<GetLatestXFeeds_Result> feeds = new List<GetLatestXFeeds_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {
                sqlConn.Open();
                string sqlQuery = "GetXFeedsByFilter";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        if (id.Equals(""))
                            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = int.Parse(id);

                        if (location.Equals(""))
                            cmd.Parameters.Add("@location", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@location", SqlDbType.NVarChar, 50).Value = location;

                        if (startingTime.Equals(""))
                            cmd.Parameters.Add("@startTime", SqlDbType.DateTime).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@startTime", SqlDbType.DateTime).Value = Convert.ToDateTime(startingTime);

                        if (endingTime.Equals(""))
                            cmd.Parameters.Add("@endTime", SqlDbType.DateTime).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@endTime", SqlDbType.DateTime).Value = Convert.ToDateTime(endingTime);

                        if (feedType.Equals(""))
                            cmd.Parameters.Add("@FeedType", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@FeedType", SqlDbType.NVarChar, 50).Value = feedType;

                        if (startId.Equals(""))
                            cmd.Parameters.Add("@startingFeedId", SqlDbType.Int).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@startingFeedId", SqlDbType.Int).Value = int.Parse(startId);

                        if (numFeeds.Equals(""))
                            cmd.Parameters.Add("@numOfFeeds", SqlDbType.Int).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@numOfFeeds", SqlDbType.Int).Value = int.Parse(numFeeds);
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
                            GetLatestXFeeds_Result feed = new GetLatestXFeeds_Result();
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            object sqlCell = reader[5];
                            feed.FilePath = (sqlCell == System.DBNull.Value)
                                ? ""
                                : (string)sqlCell;
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];

                            feeds.Add(feed);
                        }
                    }
                }
                sqlConn.Close();
            }
            return feeds;
        }

        public GetLatestXFeeds_Result GetFeedByFeedId(string feedId, string randomGuid)
        {
            GetLatestXFeeds_Result feed = new GetLatestXFeeds_Result();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {
                sqlConn.Open();
                string sqlQuery = "GetFeedByFeedId";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        cmd.Parameters.Add("@feedId", SqlDbType.Int).Value = int.Parse(feedId);
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
                        if (reader.Read())
                        {
                            feed.Username = (string)reader[0];
                            feed.UserId = (int)reader[1];
                            feed.Type = (string)reader[2];
                            feed.CreationTimeStamp = (DateTime)reader[3];
                            feed.Text = (string)reader[4];
                            object sqlCell = reader[5];
                            feed.FilePath = (sqlCell == System.DBNull.Value)
                                ? ""
                                : (string)sqlCell;
                            feed.PrioCategory = (string)reader[6];
                            feed.PrioValue = (int)reader[7];
                            feed.FeedId = (int)reader[8];
                            feed.Location = (string)reader[9];
                        }
                    }
                }
                sqlConn.Close();
            }
            return feed;
        }

        public bool FollowSensor(string humanId, string sensorId)
        {
            int humanIntId = Int32.Parse(humanId);
            int sensorIntId = Int32.Parse(sensorId);
            int rowsAffected = 0;

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "AddFollowSensor";

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
      

    }
}
