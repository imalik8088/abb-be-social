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

        /// <summary>
        /// A method used to get all the feeds posted in the db.
        /// </summary>
        /// <returns>A DataTable containing</returns>
        public DataTable GetLatestFeeds()
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetLatestFeeds";
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

        public DataTable GetLatest10Feeds()
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetLatest10Feeds";
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

        public DataTable GetLatest20Feeds()
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetLatest20Feeds";
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

        public DataTable GetLatestFeedByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetLatestFeedsByFilter";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter locationParam = new SqlParameter("@location", SqlDbType.NVarChar, 50);
                locationParam.Direction = ParameterDirection.Input;
                locationParam.Value = this.ToDBNull(location);
                this.sqlCommand.Parameters.Add(locationParam);

                SqlParameter startingTimeParam = new SqlParameter("@startTime", SqlDbType.DateTime);
                startingTimeParam.Direction = ParameterDirection.Input;

                if (startingTime == DateTime.MinValue)
                    startingTimeParam.Value = DBNull.Value;
                else
                    startingTimeParam.Value = startingTime;

                this.sqlCommand.Parameters.Add(startingTimeParam);

                SqlParameter endingTimeParam = new SqlParameter("@endTime", SqlDbType.DateTime);
                endingTimeParam.Direction = ParameterDirection.Input;

                if (endingTime == DateTime.MinValue)
                    endingTimeParam.Value = DBNull.Value;
                else
                    endingTimeParam.Value = endingTime;

                this.sqlCommand.Parameters.Add(endingTimeParam);

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

        public bool PublishFeed(string userName, List<string> tags, string location, string content, string category, string filePath, int priorityID)
        {
            bool returnValue = false;

            try
            {
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
                this.sqlConnection.Open();
                returnValue = Convert.ToBoolean(this.sqlCommand.ExecuteNonQuery());
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

        public DataTable GetUserHistoryFeeds(string userName, DateTime from, DateTime to)
        {
            return null;
        }

        public DataTable GetFeedsByFilter(string name, string location, DateTime startingTime, DateTime endingTime, string feedType)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlCommand.CommandText = "GetFeedsByFilter";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter userNameParam = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
                userNameParam.Direction = ParameterDirection.Input;
                userNameParam.Value = this.ToDBNull(name);
                this.sqlCommand.Parameters.Add(userNameParam);

                SqlParameter locationParam = new SqlParameter("@location", SqlDbType.NVarChar, 50);
                locationParam.Direction = ParameterDirection.Input;
                locationParam.Value = this.ToDBNull(location);
                this.sqlCommand.Parameters.Add(locationParam);

                SqlParameter startingTimeParam = new SqlParameter("@startTime", SqlDbType.DateTime);
                startingTimeParam.Direction = ParameterDirection.Input;

                if (startingTime == DateTime.MinValue)
                    startingTimeParam.Value = DBNull.Value;
                else
                    startingTimeParam.Value = startingTime;

                this.sqlCommand.Parameters.Add(startingTimeParam);

                SqlParameter endingTimeParam = new SqlParameter("@endTime", SqlDbType.DateTime);
                endingTimeParam.Direction = ParameterDirection.Input;

                if (endingTime == DateTime.MinValue)
                    endingTimeParam.Value = DBNull.Value;
                else
                    endingTimeParam.Value = endingTime;

                this.sqlCommand.Parameters.Add(endingTimeParam);

                SqlParameter feedTypeParam = new SqlParameter("@FeedType", SqlDbType.NVarChar, 50);
                feedTypeParam.Direction = ParameterDirection.Input;
                feedTypeParam.Value = this.ToDBNull(feedType);
                this.sqlCommand.Parameters.Add(feedTypeParam);

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

        public DataTable GetFeedComments(int feedId)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetFeedComments";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter feedIdParam = new SqlParameter("@feedId", SqlDbType.Int);
                feedIdParam.Direction = ParameterDirection.Input;
                feedIdParam.Value = feedId == -1 ? -1 : feedId;
                this.sqlCommand.Parameters.Add(feedIdParam);

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

        public DataTable GetFeedTags(int feedId)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetFeedTags";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter feedIdParam = new SqlParameter("@feedId", SqlDbType.Int);
                feedIdParam.Direction = ParameterDirection.Input;
                feedIdParam.Value = feedId == -1 ? -1 : feedId;
                this.sqlCommand.Parameters.Add(feedIdParam);

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

        public DataTable GetLatestXFeeds(int numberOfFeeds)
        {
            DataTable table = new DataTable();

            try
            {
                this.sqlConnection.Open();
                this.sqlCommand.CommandText = "GetLatestXFeeds";
                this.sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter numberOfFeedsParam = new SqlParameter("@X", SqlDbType.Int);
                numberOfFeedsParam.Direction = ParameterDirection.Input;
                numberOfFeedsParam.Value = numberOfFeeds == -1 ? -1 : numberOfFeeds;
                this.sqlCommand.Parameters.Add(numberOfFeedsParam);

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

        public object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }
    }
}
