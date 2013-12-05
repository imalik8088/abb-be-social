/*
 * Written by: Robert Gustavsson
 * Date: 11.11.2013
 * Project: ABBConnect
 * Revised:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ABBConnectSimulator.DAL
{
    class SQLProvider :ISQLProvider
    {
        
        SqlConnection conn;

        /// <summary>
        /// Constructor
        /// </summary>
        public SQLProvider()
        {
            conn = new SqlConnection();
            conn.ConnectionString = "Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;";
            try
            {
                conn.Open();
                conn.Close();
            }
            catch (Exception)
            {           
                throw;
            }
        }
        /// <summary>
        /// Loads sensors from the database
        /// </summary>
        /// <returns></returns>
        public List<Sensor> LoadSensorData()
        {
            List<Sensor> list = new List<Sensor>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "GetAllSensors";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            conn.Open();

            reader = cmd.ExecuteReader();

            //for each line in the DB
            while (reader.Read())
            {
                Sensor s = new Sensor();
                s.ID = reader.GetInt32(0);
                s.Name = reader.GetValue(1).ToString();
                s.LowerBoundery = reader.GetDecimal(2);
                s.UpperBoundery = reader.GetDecimal(3);
                list.Add(s);
            }

            conn.Close();
            return list;
           
        }

        /// <summary>
        /// Change the bounderies of a sensor in the DB
        /// </summary>
        /// <param name="sensorID"></param>
        /// <param name="lBoundery"></param>
        /// <param name="uBoundery"></param>
        /// <returns></returns>
        public bool ChangeBounderyValue(int sensorID, decimal lBoundery, decimal uBoundery)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SetBounderyValue";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("@sensorID", SqlDbType.Int).Value = sensorID;
                cmd.Parameters.Add("@lBoundery", SqlDbType.Decimal).Value = lBoundery;
                cmd.Parameters.Add("@uBoundery", SqlDbType.Decimal).Value = uBoundery;

                conn.Open();
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            { 
                if(conn != null)
                    conn.Close();
            }

        }
        
        /// <summary>
        /// Add new values for a sensor in the DB
        /// </summary>
        /// <param name="sensorID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool PublishSensorData(int sensorID, int value)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AddSensorValues";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("@sensorID", SqlDbType.Int).Value = sensorID;
                cmd.Parameters.Add("@data", SqlDbType.Int).Value = value;

                conn.Open();
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }

        }
    }
}
