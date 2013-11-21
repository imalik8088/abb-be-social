﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BLL
{
    public class FeedManager: IFeedManager
    {
        private FeedData postDbData;

        public FeedManager()
        {
            this.postDbData = new FeedData();
        }

        public List<Feed> LoadNewFeeds()
        {
            DataTable newFeedsTable = postDbData.GetLatestFeeds();
            List<Feed> lsFeed = new List<Feed>();
            UserFeed usFeed;
            SensorFeed senFeed;
            int tempInt = 0;

            foreach (DataRow row in newFeedsTable.Rows)
            {
                StringBuilder tempContainer = new StringBuilder();
                tempContainer.Append(AvoidStringNulls(row["FeedType"].ToString()));

                if (tempContainer.ToString().Equals("Human"))
                {
                    tempContainer.Clear();
                    usFeed = new UserFeed();

                    tempContainer.Append(AvoidStringNulls(row["PrioValue"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                        usFeed.Priority = tempInt;
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["PrioCategory"].ToString()));
                    usFeed.Category = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FirstName"].ToString()));
                    usFeed.Owner.FirstName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["LastName"].ToString()));
                    usFeed.Owner.LastName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Name"].ToString()));
                    usFeed.Owner.UserName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Content"].ToString()));
                    usFeed.Content = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FilePath"].ToString()));
                    usFeed.MediaFilePath = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Location"].ToString()));
                    usFeed.Location = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FeedId"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                    {
                        usFeed.Tags = LoadFeedTags(tempInt);
                        usFeed.Comments = LoadFeedComments(tempInt);
                    }
                    tempContainer.Clear();

                    lsFeed.Add(usFeed);
                }
                else
                {
                    tempContainer.Clear();
                    senFeed = new SensorFeed();

                    tempContainer.Append(AvoidStringNulls(row["PrioValue"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                        senFeed.Priority = tempInt;
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["PrioCategory"].ToString()));
                    senFeed.Category = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Name"].ToString()));
                    senFeed.Owner.Name = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Content"].ToString()));
                    senFeed.Content = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Location"].ToString()));
                    senFeed.Location = tempContainer.ToString();
                    tempContainer.Clear();

                    lsFeed.Add(senFeed);
                }
            }

            return lsFeed;
        }

        public List<Feed> LoadLatest10Feeds()
        {
            DataTable newFeedsTable = postDbData.GetLatest10Feeds();
            List<Feed> lsFeed = new List<Feed>();
            UserFeed usFeed;
            SensorFeed senFeed;
            int tempInt = 0;

            foreach (DataRow row in newFeedsTable.Rows)
            {
                StringBuilder tempContainer = new StringBuilder();
                tempContainer.Append(AvoidStringNulls(row["FeedType"].ToString()));

                if (tempContainer.ToString().Equals("Human"))
                {
                    tempContainer.Clear();
                    usFeed = new UserFeed();

                    tempContainer.Append(AvoidStringNulls(row["PrioValue"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                        usFeed.Priority = tempInt;
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["PrioCategory"].ToString()));
                    usFeed.Category = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FirstName"].ToString()));
                    usFeed.Owner.FirstName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["LastName"].ToString()));
                    usFeed.Owner.LastName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Name"].ToString()));
                    usFeed.Owner.UserName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Content"].ToString()));
                    usFeed.Content = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FilePath"].ToString()));
                    usFeed.MediaFilePath = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Location"].ToString()));
                    usFeed.Location = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FeedId"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                    {
                        usFeed.Tags = LoadFeedTags(tempInt);
                        usFeed.Comments = LoadFeedComments(tempInt);
                    }
                    tempContainer.Clear();

                    lsFeed.Add(usFeed);
                }
                else
                {
                    tempContainer.Clear();
                    senFeed = new SensorFeed();

                    tempContainer.Append(AvoidStringNulls(row["PrioValue"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                        senFeed.Priority = tempInt;
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["PrioCategory"].ToString()));
                    senFeed.Category = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Name"].ToString()));
                    senFeed.Owner.Name = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Content"].ToString()));
                    senFeed.Content = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Location"].ToString()));
                    senFeed.Location = tempContainer.ToString();
                    tempContainer.Clear();

                    lsFeed.Add(senFeed);
                }
            }

            return lsFeed;
        }

        public List<Feed> LoadLatest20Feeds()
        {
            DataTable newFeedsTable = postDbData.GetLatest20Feeds();
            List<Feed> lsFeed = new List<Feed>();
            UserFeed usFeed;
            SensorFeed senFeed;
            int tempInt = 0;

            foreach (DataRow row in newFeedsTable.Rows)
            {
                StringBuilder tempContainer = new StringBuilder();
                tempContainer.Append(AvoidStringNulls(row["FeedType"].ToString()));

                if (tempContainer.ToString().Equals("Human"))
                {
                    tempContainer.Clear();
                    usFeed = new UserFeed();

                    tempContainer.Append(AvoidStringNulls(row["PrioValue"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                        usFeed.Priority = tempInt;
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["PrioCategory"].ToString()));
                    usFeed.Category = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FirstName"].ToString()));
                    usFeed.Owner.FirstName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["LastName"].ToString()));
                    usFeed.Owner.LastName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Name"].ToString()));
                    usFeed.Owner.UserName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Content"].ToString()));
                    usFeed.Content = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FilePath"].ToString()));
                    usFeed.MediaFilePath = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Location"].ToString()));
                    usFeed.Location = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FeedId"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                    {
                        usFeed.Tags = LoadFeedTags(tempInt);
                        usFeed.Comments = LoadFeedComments(tempInt);
                    }
                    tempContainer.Clear();

                    lsFeed.Add(usFeed);
                }
                else
                {
                    tempContainer.Clear();
                    senFeed = new SensorFeed();

                    tempContainer.Append(AvoidStringNulls(row["PrioValue"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                        senFeed.Priority = tempInt;
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["PrioCategory"].ToString()));
                    senFeed.Category = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Name"].ToString()));
                    senFeed.Owner.Name = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Content"].ToString()));
                    senFeed.Content = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Location"].ToString()));
                    senFeed.Location = tempContainer.ToString();
                    tempContainer.Clear();

                    lsFeed.Add(senFeed);
                }
            }

            return lsFeed;
        }

        public List<Feed> LoadLatestXFeeds(int feedNum)
        {
            DataTable newFeedsTable = postDbData.GetLatestXFeeds(feedNum);
            List<Feed> lsFeed = new List<Feed>();
            UserFeed usFeed;
            SensorFeed senFeed;
            int tempInt = 0;

            foreach (DataRow row in newFeedsTable.Rows)
            {
                StringBuilder tempContainer = new StringBuilder();
                tempContainer.Append(AvoidStringNulls(row["FeedType"].ToString()));

                if (tempContainer.ToString().Equals("Human"))
                {
                    tempContainer.Clear();
                    usFeed = new UserFeed();

                    tempContainer.Append(AvoidStringNulls(row["PrioValue"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                        usFeed.Priority = tempInt;
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["PrioCategory"].ToString()));
                    usFeed.Category = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FirstName"].ToString()));
                    usFeed.Owner.FirstName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["LastName"].ToString()));
                    usFeed.Owner.LastName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Name"].ToString()));
                    usFeed.Owner.UserName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Content"].ToString()));
                    usFeed.Content = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FilePath"].ToString()));
                    usFeed.MediaFilePath = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Location"].ToString()));
                    usFeed.Location = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FeedId"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                    {
                        usFeed.Tags = LoadFeedTags(tempInt);
                        usFeed.Comments = LoadFeedComments(tempInt);
                    }
                    tempContainer.Clear();

                    lsFeed.Add(usFeed);
                }
                else
                {
                    tempContainer.Clear();
                    senFeed = new SensorFeed();

                    tempContainer.Append(AvoidStringNulls(row["PrioValue"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                        senFeed.Priority = tempInt;
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["PrioCategory"].ToString()));
                    senFeed.Category = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Name"].ToString()));
                    senFeed.Owner.Name = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Content"].ToString()));
                    senFeed.Content = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Location"].ToString()));
                    senFeed.Location = tempContainer.ToString();
                    tempContainer.Clear();

                    lsFeed.Add(senFeed);
                }
            }

            return lsFeed;
        }

        public List<Feed> LoadNewFeedsByFilter(string location, DateTime startingTime, DateTime endingTime)
        {
            if (String.IsNullOrEmpty(location))
                location = null;

            DataTable newFeedsTable = postDbData.GetLatestFeedByFilter(location, startingTime, endingTime);
            List<Feed> lsFeed = new List<Feed>();
            UserFeed usFeed;
            SensorFeed senFeed;
            int tempInt = 0;

            foreach (DataRow row in newFeedsTable.Rows)
            {
                StringBuilder tempContainer = new StringBuilder();
                tempContainer.Append(AvoidStringNulls(row["FeedType"].ToString()));

                if (tempContainer.ToString().Equals("Human"))
                {
                    tempContainer.Clear();
                    usFeed = new UserFeed();

                    tempContainer.Append(AvoidStringNulls(row["PrioValue"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                        usFeed.Priority = tempInt;
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["PrioCategory"].ToString()));
                    usFeed.Category = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FirstName"].ToString()));
                    usFeed.Owner.FirstName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["LastName"].ToString()));
                    usFeed.Owner.LastName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Name"].ToString()));
                    usFeed.Owner.UserName = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Content"].ToString()));
                    usFeed.Content = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FilePath"].ToString()));
                    usFeed.MediaFilePath = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Location"].ToString()));
                    usFeed.Location = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["FeedId"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                    {
                        usFeed.Tags = LoadFeedTags(tempInt);
                        usFeed.Comments = LoadFeedComments(tempInt);
                    }
                    tempContainer.Clear();

                    lsFeed.Add(usFeed);
                }
                else
                {
                    tempContainer.Clear();
                    senFeed = new SensorFeed();

                    tempContainer.Append(AvoidStringNulls(row["PrioValue"].ToString()));

                    if (CastStringToInt(tempContainer.ToString(), ref tempInt))
                        senFeed.Priority = tempInt;
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["PrioCategory"].ToString()));
                    senFeed.Category = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Name"].ToString()));
                    senFeed.Owner.Name = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Content"].ToString()));
                    senFeed.Content = tempContainer.ToString();
                    tempContainer.Clear();

                    tempContainer.Append(AvoidStringNulls(row["Location"].ToString()));
                    senFeed.Location = tempContainer.ToString();
                    tempContainer.Clear();

                    lsFeed.Add(senFeed);
                }
            }

            return lsFeed;
        }

        public List<Feed> LoadHistoryFeedsBySensor(int sensorID)
        {
            throw new NotImplementedException();
        }

        public bool PublishFeed(UserFeed feed)
        {
            List<string> taggedUsers = new List<string>();

            if (String.IsNullOrEmpty(feed.Owner.UserName))
                return false;
            else if (String.IsNullOrEmpty(feed.Content) && String.IsNullOrEmpty(feed.MediaFilePath))
                return false;
            else if (String.IsNullOrEmpty(feed.Category) || feed.Priority == 0)
                return false;
            else if (feed.ID <= 0)
                return false;
            else
            {
                foreach (User u in feed.Tags)
                    taggedUsers.Add(u.UserName);

                return this.postDbData.PublishFeed(feed.Owner.UserName, taggedUsers, feed.Location,
                    feed.Content, feed.Category, feed.MediaFilePath, feed.ID);
            }
        }

        public bool AddTagToFeed(int feedId, string username)
        {
            if (feedId <= 0)
                return false;
            else if (String.IsNullOrEmpty(username))
                return false;
            else
                return this.postDbData.IncludeTagFeed(feedId, username);
        }

        public bool PublishComment(int feedID, Comment comment)
        {
            if (feedID <= 0)
                return false;
            else if (String.IsNullOrEmpty(comment.Owner.UserName))
                return false;
            else if (String.IsNullOrEmpty(comment.Content))
                return false;
            else
                return this.postDbData.PublishComment(feedID, comment.Owner.UserName, comment.Content);
        }

        public List<Comment> LoadFeedComments(int feedId)
        {
            DataTable commentsTable = postDbData.GetFeedComments(feedId);

            List<Comment> lsComments = new List<Comment>();
            Comment tempComment = new Comment();

            foreach (DataRow row in commentsTable.Rows)
            {
                StringBuilder tempContainer = new StringBuilder();
                tempContainer.Append(AvoidStringNulls(row["FirstName"].ToString()));
                tempComment.Owner.FirstName = tempContainer.ToString();
                tempContainer.Clear();

                tempContainer.Append(AvoidStringNulls(row["LastName"].ToString()));
                tempComment.Owner.LastName = tempContainer.ToString();
                tempContainer.Clear();

                tempContainer.Append(AvoidStringNulls(row["UserName"].ToString()));
                tempComment.Owner.UserName = tempContainer.ToString();
                tempContainer.Clear();

                tempContainer.Append(AvoidStringNulls(row["CommentText"].ToString()));
                tempComment.Content = tempContainer.ToString();
                tempContainer.Clear();

                tempContainer.Append(AvoidStringNulls(row["CreationTimeStamp"].ToString()));
                tempComment.TimeStamp = Convert.ToDateTime(tempContainer.ToString());
                tempContainer.Clear();

                lsComments.Add(tempComment);
                tempComment = new Comment();
            }

            return lsComments;
        }

        public List<User> LoadFeedTags(int feedId)
        {
            DataTable tagsTable = postDbData.GetFeedTags(feedId);

            List<User> lsUsers = new List<User>();
            User tempUser = new User();

            foreach (DataRow row in tagsTable.Rows)
            {
                StringBuilder tempContainer = new StringBuilder();
                tempContainer.Append(AvoidStringNulls(row["FirstName"].ToString()));
                tempUser.FirstName = tempContainer.ToString();
                tempContainer.Clear();

                tempContainer.Append(AvoidStringNulls(row["LastName"].ToString()));
                tempUser.LastName = tempContainer.ToString();
                tempContainer.Clear();

                tempContainer.Append(AvoidStringNulls(row["UserName"].ToString()));
                tempUser.UserName = tempContainer.ToString();
                tempContainer.Clear();

                lsUsers.Add(tempUser);
                tempUser = new User();
            }

            return lsUsers;
        }

        private static string AvoidStringNulls(string str)
        {
            string resStr = "";

            if (str != null)
                resStr = str;

            return resStr;
        }

        private static bool CastStringToInt(string str, ref int returnValue)
        {
            bool result = false;

            try
            {
                returnValue = Convert.ToInt32(str);
                result = true;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Input string is not a sequence of digits.");
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The number cannot fit in an Int32.");
            }

            return result;
        }
    }
}