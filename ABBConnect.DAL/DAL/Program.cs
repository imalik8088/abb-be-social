using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable dt = new DataTable();

            UserData us = new UserData();
            Console.WriteLine(us.CheckCredentials("simpm", "password"));
            //dt = us.GetUserInformation("simpm");
            //foreach (DataRow row in dt.Rows)
            //{
            //    //string type = row["Name"].ToString();
            //    string userName = row["UserName"].ToString();
            //    string firstName = row["FirstName"].ToString();
            //    string lastName = row["LastName"].ToString();
            //    string phoneNumber = row["PhoneNumber"].ToString();
            //    string email = row["Email"].ToString();

            //    Console.WriteLine(userName + " " + firstName +
            //        " " + lastName + " " + phoneNumber + " " + email);
            //}
            //Console.ReadKey();

            //------------------------------------ Sensor methods basic testing (just for me) --------------------------------------------

            SensorData sd = new SensorData();

            //check the sensor information
            //dt = sd.RetrieveSensorInformation(1001);

            //foreach (DataRow row in dt.Rows)
            //{
            //    string id = row["Id"].ToString();
            //    string name = row["Name"].ToString();
            //    string minCr = row["MIN_Critical"].ToString();
            //    string maxCr = row["MAX_Critical"].ToString();

            //    Console.WriteLine(id + " " + name + " " + minCr + " " + maxCr);
            //}

            DateTime myDate = Convert.ToDateTime("2013-11-12 10:56:38");
            //get history data from a sensor
            //dt = sd.RetrieveHistoricalSensorData(1001,myDate,DateTime.Now);

            //foreach (DataRow row in dt.Rows)
            //{
            //    string rawVal = row["RawValue"].ToString();
            //    string timeSt = row["CreationTimeStamp"].ToString();

            //    Console.WriteLine(rawVal + " " + timeSt);
            //}

            //get current value
            //Console.WriteLine(sd.RetrieveCurrentSensorData(1001));

            //get all alarms for a sensor
            //dt = sd.RetrieveSensorAlarms(1001);

            //------------------------------------ Common Data methods------------------------------------------------------------------

            CommonData cd = new CommonData();

            //dt = cd.GetAllLocations();

            //foreach (DataRow row in dt.Rows)
            //{
            //    string rawVal = row["Place"].ToString();

            //    Console.WriteLine(rawVal);
            //}

            //dt = cd.GetPostGategories();

            //foreach (DataRow row in dt.Rows)
            //{
            //    string rawVal = row["Id"].ToString();
            //    string namVal = row["Name"].ToString();

            //    Console.WriteLine(rawVal + " " + namVal);
            //}

            //------------------------------------ Feed methods basic testing (just for me) --------------------------------------------

            FeedData fd = new FeedData();

            //code to test the publish method
            List<string> list = new List<string>();
            list.Add("dpa12003");
            //list.Add("rgn09003");
            //bool res = fd.PublishFeed("simpm",list,"Field","i post on the DB from DAL","Sticky Note",null,2);
            //Console.WriteLine(res);

            //dt = fd.GetLatestFeeds();
            //dt = fd.GetLatest10Feeds();
            //dt = fd.GetLatest20Feeds();
            //dt = fd.GetLatestFeedByFilter("ControlRoom 1A", DateTime.MinValue, DateTime.MinValue);
            //dt = fd.GetLatestFeedByFilter("Machine 1", myDate, DateTime.Now);
            //foreach (DataRow row in dt.Rows)
            //{
            //    string time = row["CreationTimeStamp"].ToString();
            //    string prioVal = row["PrioValue"].ToString();
            //    string categVal = row["PrioCategory"].ToString();
            //    string firstVal = row["FirstName"].ToString();
            //    string lastVal = row["LastName"].ToString();
            //    string userVal = row["Username"].ToString();
            //    string contVal = row["Content"].ToString();
            //    string fileVal = row["FilePath"].ToString();
            //    string locVal = row["Location"].ToString();
            //    string senNamVal = row["SensorName"].ToString();
            //    string sensorVal = row["SensorId"].ToString();

            //    Console.WriteLine(time + " " + prioVal + " " + categVal + " " +
            //        firstVal + " " + lastVal + " " + userVal + " " + contVal + " " +
            //        fileVal + " " + locVal + " " + senNamVal + " " + sensorVal);
            //}

            //test the publish comment
            //Console.WriteLine(fd.PublishComment(7, "simpm", "i do not agree :("));

            //------------------------------------ Sensor Feed methods basic testing (just for me) --------------------------------------------

            SensorFeedData sf = new SensorFeedData();

            //fill the table with sensor feed alarms
            //dt = sf.GetAllSensorFeeds();

            //dt = sf.GetSensorFeedsByFilter(1001, null, myDate, DateTime.Now);
            //dt = sf.GetSensorFeedsByFilter(1001, "Machine 1", myDate, DateTime.Now);
            //dt = sf.GetSensorFeedsByFilter(1001, "Machine 1", DateTime.MinValue, DateTime.MinValue);

            //dt = sf.GetAllSensorFeedsByFilter("Machine 1", DateTime.MinValue, DateTime.MinValue);
            //dt = sf.GetAllSensorFeedsByFilter(null, myDate, DateTime.Now);

            //dt = sf.GetSensorFeeds(1002);

            //check the data inside the table for sensor feeds
            //foreach (DataRow row in dt.Rows)
            //{
            //    //string type = row["Name"].ToString();
            //    string locationVal = row["Location"].ToString();
            //    string content = row["Text"].ToString();
            //    string timeStamp = row["CreationTimeStamp"].ToString();
            //    string name = row["SensorName"].ToString();
            //    string id = row["Id"].ToString();
            //    string prioVal = row["PrioValue"].ToString();
            //    string prioCate = row["PrioCategory"].ToString();

            //    Console.WriteLine(locationVal + " " + content +
            //        " " + timeStamp + " " + name + " " + id + " " +
            //        prioVal + " " + prioCate);
            //}

            //------------------------------------ Human Feed methods basic testing (just for me) --------------------------------------------

            HumanFeedData hf = new HumanFeedData();

            //fill the table with user feeds coming from one user
            //dt = hf.GetUserFeeds("rgn09003");

            //fill the table with user feeds coming from a specific location
            //dt = hf.RetrieveHumanFeedsFromLocation("Field");

            //fill the table with data of human feed from a time period
            //myDate = Convert.ToDateTime("2013-11-12 12:56:38");
            //dt = hf.RetrieveHumanFeedsByTime(myDate, DateTime.Now);

            //dt = hf.GetAllUserFeeds();

            //dt = hf.GetUserFeedsByFilter("simpm", null, myDate, DateTime.Now);
            //dt = hf.GetUserFeedsByFilter("dpa12003", "Field", DateTime.MinValue, DateTime.MinValue);

            //dt = hf.GetAllUserFeedsByFilter("Field", DateTime.MinValue, DateTime.MinValue);

            //check the data inside the table for human feeds
            //foreach (DataRow row in dt.Rows)
            //{
            //    string firstName = row["FirstName"].ToString();
            //    string lastName = row["LastName"].ToString();
            //    string usName = row["Username"].ToString();
            //    string timeStamp = row["CreationTimeStamp"].ToString();
            //    string content = row["Text"].ToString();
            //    string priorCat = row["PriorityCategory"].ToString();
            //    string priorVal = row["PrioValue"].ToString();
            //    string place = row["Place"].ToString();
            //    string feedId = row["FeedId"].ToString();

            //    Console.WriteLine(firstName + " " + lastName + " " + usName +
            //        " " + timeStamp + " " + content + " " + priorCat +
            //        " " + priorVal + " " + place + " " + feedId);
            //}

            Console.ReadKey();

        }
    }
}
