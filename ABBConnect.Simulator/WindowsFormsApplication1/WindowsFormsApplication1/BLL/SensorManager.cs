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
using ABBConnectSimulator.DAL;
using System.Threading;
using System.IO;
namespace ABBConnectSimulator.BLL
{
    class SensorManager : ISensorManager
    {

        List<Sensor> sensors;
        ISQLProvider sqlProvider;
        int maxValues;

        public SensorManager()
        {
            this.sensors = new List<Sensor>();
            sqlProvider = new SQLProvider();
            maxValues = 0;
            FillSensorList();
        }
        /// <summary>
        /// Fill the internal list of sensors
        /// </summary>
        public  void FillSensorList()
        {
            this.sensors = sqlProvider.LoadSensorData();
        }

        /// <summary>
        /// Change the boundery of a sensor
        /// </summary>
        /// <param name="index"></param>
        /// <param name="uValue"></param>
        /// <param name="lValue"></param>
        /// <returns></returns>
        public bool ChangeBoundery(int index, decimal uValue, decimal lValue)
        {
            try
            {
                this.sensors[index].UpperBoundery = uValue;
                this.sensors[index].LowerBoundery = lValue;

                bool ret = sqlProvider.ChangeBounderyValue(sensors[index].ID, lValue, uValue);
                if (ret)
                    return true;
                else
                    return false;
            }   
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Generate values and insert them to the selected sensor
        /// </summary>
        /// <param name="index"></param>
        /// <param name="noOfValues"></param>
        public void GenerateValues(int index, int noOfValues)
        {
            if (maxValues < noOfValues)
                maxValues = noOfValues;

            decimal uBoundery = this.sensors[index].UpperBoundery;
            decimal lBoundery = this.sensors[index].LowerBoundery;
            Random rand = new Random();

            for (int i = 0; i < noOfValues; i++)
                this.sensors[index].AddValue(rand.Next((int)lBoundery - 1, (int)uBoundery + 1));

        }

        /// <summary>
        /// Load values from a .txt file and insert them to the selected sensor
        /// </summary>
        /// <param name="index"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool LoadValues(int index, string filePath)
        {
            try
            {
                int  counter = 0;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    int value;
                    while ((line = sr.ReadLine()) != null)
                    {

                        if (Int32.TryParse(line, out value))
                        {
                            sensors[index].AddValue(value);
                            counter++;
                        }
                        else
                            return false;
                    }
                }
                if (maxValues < counter)
                    maxValues = counter;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Publish the values from each sensor to the DB with a timing on 2sec
        /// </summary>
        public void PublishSensorValues(int timeIntervall)
        {
            for (int j = 0; j < maxValues; j++)
            {
                for (int i = 0; i < this.sensors.Count; i++)
                {
                    try
                    {
                        if( j <= sensors[i].NumberOfValues() && sensors[i].NumberOfValues() > 0)
                            sqlProvider.PublishSensorData(sensors[i].ID, sensors[i].GetValue(j));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                //sleep for X sec, so sensors are providing sensor data every x seconds.
                Thread.Sleep(timeIntervall);
            }          
        }

        /// <summary>
        /// returns the boundery values of the selected sensor
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public decimal[] GetBounderyValues(int index)
        {
            decimal[] retVals = new decimal[2];

            retVals[1] = this.sensors[index].UpperBoundery;
            retVals[0] = this.sensors[index].LowerBoundery;

            return retVals;
        
        }

        /// <summary>
        /// Returns all the sensors that has been read from the DB
        /// </summary>
        /// <returns></returns>
        public List<Sensor> GetSensors()
        {
            return this.sensors;
        }
    }
}
