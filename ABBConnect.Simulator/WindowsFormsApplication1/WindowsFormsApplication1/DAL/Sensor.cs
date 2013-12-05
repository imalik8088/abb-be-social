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

namespace ABBConnectSimulator.DAL
{
    class Sensor
    {
    #region field
        string name;
        int iD;
        decimal upperBoundery, lowerBoundery;
        List<int> values;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public decimal LowerBoundery
        {
            get { return lowerBoundery; }
            set { lowerBoundery = value; }
        }
        public decimal UpperBoundery
        {
            get { return upperBoundery; }
            set { upperBoundery = value; }
        }
    #endregion
        
        public Sensor()
        {
            this.values = new List<int>();
        }

        /// <summary>
        /// Returns all the values from the sensor
        /// </summary>
        /// <returns></returns>
        public List<int> GetValues()
        {
            return this.values;
        }
        /// <summary>
        /// Get a specific value from the index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetValue(int index)
        {
            return this.values[index];
        }
       /// <summary>
       /// Add a value to the sensor
       /// </summary>
       /// <param name="val"></param>
        public void AddValue(int val)
        {
            this.values.Add(val);
        }
        /// <summary>
        /// Check how many values the sensor has stored
        /// </summary>
        /// <returns></returns>
        public int NumberOfValues()
        {
            return this.values.Count;
        }
    }
}
