using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace ABBJSONService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ABBConnectWCF : IABBConnectWCF
    {
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "LogIn?username={username}&password={password}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHumanInfo?id={id}")]
        public GetHumanInformation_Result GetHumanInformation(string id)
        {
            int iD = Int32.Parse(id);
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHumanInfoByUsername?username={username}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLatestXFeeds?num={x}")]
        List<GetLatestXFeeds_Result> IABBConnectWCF.GetLatestXFeeds(string X)
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLatestXFeedsFromId?num={x}&id={id}")]
        List<GetLatestXFeedsFromId_Result> IABBConnectWCF.GetLatestXFeedsFromId(string X, string Id)
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


        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PostFeed?id={id}&text={text}&path={filepath}&priority={prioID}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCategories")]
        public List<GetPriorityCategories_Result> GetCategories()
        {
            List<GetPriorityCategories_Result> cats = new List<GetPriorityCategories_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSensors")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeedComments?feedId={feedId}")]
        public List<GetFeedComments_Result> GetFeedComments(string feedId)
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeedTags?feedId={feedId}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHumanFeeds")]
        List<GetAllHumanFeeds_Result> IABBConnectWCF.GetHumanFeeds()
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHumanFeedsByFilter?location={location}&start={startingTime}&end={endingTime}")]
        List<GetAllHumanFeedsByFilter_Result> IABBConnectWCF.GetHumanFeedsByFilter(string location, string startingTime, string endingTime)
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSensorFeeds")]
        List<GetAllSensorFeeds_Result> IABBConnectWCF.GetSensorFeeds()
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSensorFeedsByFilter?location={location}&start={startingTime}&end={endingTime}")]
        List<GetAllSensorFeedsByFilter_Result> IABBConnectWCF.GetSensorFeedsByFilter(string location, string startingTime, string endingTime)
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserFeeds")]
        List<GetUserFeeds_Result> IABBConnectWCF.GetUserFeeds()
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserFeedsByFilter?location={location}&start={startingTime}&end={endingTime}")]
        List<GetUserFeedsByFilter_Result> IABBConnectWCF.GetUserFeedsByFilter(string location, string startingTime, string endingTime)
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLocations")]
        List<string> IABBConnectWCF.GetLocations()
        {
            List<string> locations = new List<string>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSensorInfo?id={id}")]
        public GetSensorInformation_Result GetSensorInformation(string id)
        {
            int iD = Int32.Parse(id);
            GetSensorInformation_Result s = new GetSensorInformation_Result();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetSensorInformation";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@sensorID", SqlDbType.Int).Value = iD;
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetPastSensorData?id={id}&start={startingTime}&end={endingTime}")]
        List<GetHistoricalDataFromSensor_Result> IABBConnectWCF.GetHistoricalDataFromSensor(string id, string startingTime, string endingTime)
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLastSensorValue?id={id}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "PostComment?feedId={feedId}&username={username}&comment={text}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "AddTag?feedId={feedId}&username={username}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "FollowSensor?humanId={humanId}&sensorId={sensorId}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FilterLatestFeeds?location={location}&start={startingTime}&end={endingTime}")]
        List<GetLatestFeedsByFilter_Result> IABBConnectWCF.GetLatestFeedsByFilter(string location, string startingTime, string endingTime)
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FilterFeeds?name={name}&location={location}&start={startingTime}&end={endingTime}&type={feedType}")]
        List<GetFeedsByFilter_Result> IABBConnectWCF.GetFeedsByFilter(string name, string location, string startingTime, string endingTime, string feedType)
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FilterFeedsByFilter?userId={id}&location={location}&start={startingTime}&end={endingTime}&type={feedType}&feedId={startId}&numFeeds={numFeeds}")]
        List<GetLatestXFeeds_Result> IABBConnectWCF.GetXFeedsByFilter(string id, string location, string startingTime, string endingTime, string feedType, string startId, string numFeeds)
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveFilter?userId={userId}&name={filterName}&start={startingTime}&end={endingTime}&location={location}&feedType={feedType}")]
        public int SaveFilter(string userId, string filterName, string startingTime, string endingTime, string location, string feedType)
        {
            int retValue=-1;         

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "AddUserSavedFilter";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = int.Parse(userId);

                        if (location.Equals(""))
                            cmd.Parameters.Add("@Location", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@Location", SqlDbType.NVarChar, 50).Value = location;

                        if (startingTime.Equals(""))
                            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(startingTime);

                        if (endingTime.Equals(""))
                            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(endingTime);

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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "TagUserInFilter?userId={userId}&filterId={filterId}")]
        public bool AddFilterUser(string userId, string filterId)
        {
            int userIntId = Int32.Parse(userId);
            int filterIntId = Int32.Parse(filterId);
            int rowsAffected = 0;

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "AddUserToSavedFilter";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userIntId;
                    cmd.Parameters.Add("@FilterId", SqlDbType.Int).Value = filterIntId;

                    rowsAffected = cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }

            if (rowsAffected > 0)
                return true;
            else
                return false;
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserFilters?userId={userId}")]
        public List<GetUserSavedFilters_Result> GetSavedFilter(string userId)
        {
            int iD = Int32.Parse(userId);
            List<GetUserSavedFilters_Result> savedFilters = new List<GetUserSavedFilters_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetUserSavedFilters";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = iD;

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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SearchByName?name={query}")]
        List<GetUsersByName_Result> IABBConnectWCF.SearchUsersByName(string query)
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFilterTaggedUsers?filterId={filterId}")]
        public List<GetUserSavedFiltersTagedUsers_Result> GetFilterTaggedUsers(string filterId)
        {
            List<GetUserSavedFiltersTagedUsers_Result> taggedUsers = new List<GetUserSavedFiltersTagedUsers_Result>();

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "GetUserSavedFiltersTagedUsers";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FilterId", SqlDbType.Int).Value = Int32.Parse(filterId); ;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ;
                        while (reader.Read())
                        {
                            GetUserSavedFiltersTagedUsers_Result user = new GetUserSavedFiltersTagedUsers_Result();
                            user.Id = (int)reader[0];
                            user.Name = (string)reader[1];
                            user.FirstName = (string)reader[2];
                            user.LastName = (string)reader[3];

                            taggedUsers.Add(user);
                        }
                    }
                }
                sqlConn.Close();
            }
            return taggedUsers;
        }
    }
}

