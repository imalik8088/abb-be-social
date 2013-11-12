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

        public DataTable RetrieveLatestFeeds()
        {
            return null;
        }

        public bool PublishFeed(string userName, List<string> tags, string location, string content, string category, string filePath, int priorityID)
        {
            bool returnValue = false;

            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "AddFeed";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter user_Name = new SqlParameter("@userName", SqlDbType.NVarChar, 50);
                user_Name.Direction = ParameterDirection.Input;
                user_Name.Value = userName == null ? "" : userName;
                this.sqlCommand.Parameters.Add(user_Name);

                SqlParameter text_Feed = new SqlParameter("@text", SqlDbType.NVarChar);
                text_Feed.Direction = ParameterDirection.Input;
                text_Feed.Value = content == null ? "" : content;
                this.sqlCommand.Parameters.Add(text_Feed);

                SqlParameter filePath_Par = new SqlParameter("@filePath", SqlDbType.NVarChar);
                filePath_Par.Direction = ParameterDirection.Input;
                filePath_Par.Value = filePath == null ? "" : filePath;
                this.sqlCommand.Parameters.Add(filePath_Par);

                SqlParameter feedPriorityID = new SqlParameter("@feedPriorityId", SqlDbType.Int);
                feedPriorityID.Direction = ParameterDirection.Input;
                feedPriorityID.Value = priorityID == -1 ? -1 : priorityID;
                this.sqlCommand.Parameters.Add(feedPriorityID);

                SqlParameter retVal = this.sqlCommand.Parameters.Add("@retFeedId", SqlDbType.Int);
                retVal.Direction = ParameterDirection.Output;

                //returnValue = Convert.ToBoolean(this.sqlCommand.ExecuteNonQuery());
                this.sqlCommand.ExecuteNonQuery();
                int insertedFeedId = (int)this.sqlCommand.Parameters["@retFeedId"].Value;

                this.sqlCommand.CommandText = "AddTagToFeed";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (string taggedUser in tags)
                {
                    this.sqlCommand.Parameters.Clear();
                    this.sqlCommand.Parameters.AddWithValue("@feedId", insertedFeedId);
                    this.sqlCommand.Parameters.AddWithValue("@username", taggedUser);
                    returnValue = Convert.ToBoolean(this.sqlCommand.ExecuteNonQuery());
                }
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

            return returnValue;
        }

        public bool PublishComment(int feedID, string userName, string comment)
        {
            bool returnValue = false;

            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "AddCommentToFeed";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter feedIdParam = new SqlParameter("@feedId", SqlDbType.Int);
                feedIdParam.Direction = ParameterDirection.Input;
                feedIdParam.Value = feedID == null ? -1 : feedID;
                this.sqlCommand.Parameters.Add(feedIdParam);

                SqlParameter user_Name = new SqlParameter("@userName", SqlDbType.NVarChar, 50);
                user_Name.Direction = ParameterDirection.Input;
                user_Name.Value = userName == null ? "" : userName;
                this.sqlCommand.Parameters.Add(user_Name);

                SqlParameter text_Feed = new SqlParameter("@text", SqlDbType.NVarChar);
                text_Feed.Direction = ParameterDirection.Input;
                text_Feed.Value = comment == null ? "" : comment;
                this.sqlCommand.Parameters.Add(text_Feed);

                returnValue = Convert.ToBoolean(this.sqlCommand.ExecuteNonQuery());

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

            return returnValue;
        }

        public DataTable RetrieveUserHistoryFeeds(string userName, DateTime from, DateTime to)
        {
            return null;
        }


    }
}
