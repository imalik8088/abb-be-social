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
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "LogIn/{username}&{password}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHumanInfo/{id}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHumanInfoByUsername/{username}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLatestXFeeds/{x}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLatestXFeeds/{x}&{id}")]
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


        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PostFeed/{id}&{text}&{filepath}&{prioID}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeedComments/{feedId}")]
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFeedTags/{feedId}")]
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
    }
}

