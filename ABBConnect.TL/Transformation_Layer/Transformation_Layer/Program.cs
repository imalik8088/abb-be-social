using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Transformation_Layer
{
    class Program
    {
        static void Main(string[] args)
        {
            HumanData ud = new HumanData();
            //bool  res = ud.CheckCredentials("rg09003","password"); 
            //Console.WriteLine(res);

            CommonData cd = new CommonData();
            HumanFeedData hfd = new HumanFeedData();
            SensorFeedData sfd = new SensorFeedData();
            FeedData fd = new FeedData();
            SensorData sd = new SensorData();

            DateTime myDate = Convert.ToDateTime("2013-11-16 10:56:38");

            //bool tag = fd.PublishComment(9, "rgn09003", "transformation layer");
            //DataTableCollection collection = sd.RetrieveHistoricalSensorData(8, myDate, DateTime.Now).Tables;

            //Console.WriteLine("raw val " + sd.RetrieveCurrentSensorData(8));
            //DataTableCollection collection = cd.GetPostGategories().Tables;

            //List<string> ls = new List<string>();

            //bool post = fd.PublishFeed(3, ls, "", "maybe the end", "", "", 3);

            DataTableCollection collection = cd.GetUserFeedsByFilter(1,"ControlRoom 1A", DateTime.MinValue, DateTime.MinValue).Tables;

            //------------------------------user information data----------------------
            //for (int i = 0; i < collection.Count; i++)
            //{
            //    DataTable table = collection[i];
            //    Console.WriteLine("{0}: {1}", i, table.TableName);

            //    foreach (DataRow row in table.Rows) // Loop over the rows.
            //    {
            //        string userName = row["Name"].ToString();
            //        string firstName = row["FirstName"].ToString();
            //        string lastName = row["LastName"].ToString();
            //        string phoneNumber = row["PhoneNumber"].ToString();
            //        string email = row["Email"].ToString();
            //        string loc = row["Location"].ToString();

            //        Console.WriteLine(userName + " " + firstName +
            //            " " + lastName + " " + phoneNumber + " " + email + " " + loc);
            //    }
            //}

            //----------------------location data-----------------------------
            //for (int i = 0; i < collection.Count; i++)
            //{
            //    DataTable table = collection[i];
            //    Console.WriteLine("{0}: {1}", i, table.TableName);

            //    foreach (DataRow row in table.Rows) // Loop over the rows.
            //    {
            //        string place = row["Place"].ToString();

            //        Console.WriteLine(place);
            //    }
            //}

            //----------------------category data-----------------------------
            //for (int i = 0; i < collection.Count; i++)
            //{
            //    DataTable table = collection[i];
            //    Console.WriteLine("{0}: {1}", i, table.TableName);

            //    foreach (DataRow row in table.Rows) // Loop over the rows.
            //    {
            //        string id = row["Id"].ToString();
            //        string name = row["Name"].ToString();

            //        Console.WriteLine(id + " " + name);
            //    }
            //}

            //-------------------------human feed data--------------------------
            for (int i = 0; i < collection.Count; i++)
            {
                DataTable table = collection[i];
                Console.WriteLine("{0}: {1}", i, table.TableName);

                foreach (DataRow row in table.Rows) // Loop over the rows.
                {
                    string usnam = row["Username"].ToString();
                    string id = row["UserId"].ToString();
                    string type = row["Type"].ToString();
                    string tm = row["CreationTimeStamp"].ToString();
                    string con = row["Text"].ToString();
                    string fpath = row["FilePath"].ToString();
                    string priCat = row["PrioCategory"].ToString();
                    string prioVal = row["PrioValue"].ToString();
                    string fid = row["FeedId"].ToString();
                    string loc = row["Location"].ToString();

                    Console.WriteLine(usnam + " " + id + " " + type + " " + tm +
                        " " + con + " " + fpath + " " + priCat + " " + prioVal +
                        " " + fid + " " + loc);
                }
            }


            //----------------------sensor feed data-----------------------------
            //for (int i = 0; i < collection.Count; i++)
            //{
            //    DataTable table = collection[i];
            //    Console.WriteLine("{0}: {1}", i, table.TableName);

            //    foreach (DataRow row in table.Rows) // Loop over the rows.
            //    {
            //        string usnam = row["Username"].ToString();
            //        string id = row["UserId"].ToString();
            //        string type = row["Type"].ToString();
            //        string tm = row["CreationTimeStamp"].ToString();
            //        string con = row["Text"].ToString();
            //        string fpath = row["FilePath"].ToString();
            //        string priCat = row["PrioCategory"].ToString();
            //        string prioVal = row["PrioValue"].ToString();
            //        string fid = row["FeedId"].ToString();
            //        string loc = row["Location"].ToString();

            //        Console.WriteLine(usnam + " " + id + " " + type + " " + tm +
            //            " " + con + " " + fpath + " " + priCat + " " + prioVal +
            //            " " + fid + " " + loc);
            //    }
            //}

            //----------------------tag data-----------------------------
            //for (int i = 0; i < collection.Count; i++)
            //{
            //    DataTable table = collection[i];
            //    Console.WriteLine("{0}: {1}", i, table.TableName);

            //    foreach (DataRow row in table.Rows) // Loop over the rows.
            //    {
            //        string us = row["UserName"].ToString();
            //        string fs = row["FirstName"].ToString();
            //        string ls = row["LastName"].ToString();
            //        string id = row["UserId"].ToString();

            //        Console.WriteLine(us + " " + fs + " " + ls + " " + id);
            //    }
            //}

            //----------------------comment data-----------------------------
            //for (int i = 0; i < collection.Count; i++)
            //{
            //    DataTable table = collection[i];
            //    Console.WriteLine("{0}: {1}", i, table.TableName);

            //    foreach (DataRow row in table.Rows) // Loop over the rows.
            //    {
            //        string us = row["UserName"].ToString();
            //        string fs = row["FirstName"].ToString();
            //        string ls = row["LastName"].ToString();
            //        string ct = row["CommentText"].ToString();
            //        string crt = row["CreationTimeStamp"].ToString();

            //        Console.WriteLine(us + " " + fs + " " + ls + " " + ct + " " + crt);
            //    }
            //}

            //----------------------sensor information data-----------------------------
            //for (int i = 0; i < collection.Count; i++)
            //{
            //    DataTable table = collection[i];
            //    Console.WriteLine("{0}: {1}", i, table.TableName);

            //    foreach (DataRow row in table.Rows) // Loop over the rows.
            //    {
            //        string us = row["Id"].ToString();
            //        string fs = row["Name"].ToString();
            //        string ls = row["MIN_Critical"].ToString();
            //        string ct = row["MAX_Critical"].ToString();
            //        //string crt = row["CreationTimeStamp"].ToString();

            //        Console.WriteLine(us + " " + fs + " " + ls + " " + ct);
            //    }
            //}

            //----------------------sensor historical information data-----------------------------
            //for (int i = 0; i < collection.Count; i++)
            //{
            //    DataTable table = collection[i];
            //    Console.WriteLine("{0}: {1}", i, table.TableName);

            //    foreach (DataRow row in table.Rows) // Loop over the rows.
            //    {
            //        string us = row["RawValue"].ToString();
            //        string fs = row["CreationTimeStamp"].ToString();

            //        Console.WriteLine(us + " " + fs);
            //    }
            //}
            Console.Read();
        }
    }
}
