using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    /// <summary>
    /// Class that rappresent a sensor
    /// </summary>
    public class Sensor: User
    {
        /// <summary>
        /// Constructor that automatically instatiate the attributes of the class
        /// </summary>
        public Sensor()
        {
            this.sensorValues = new List<SensorVTData>();
            base.UserName = "";
            base.Location = "";
            this.unitMetric = "";
        }

        /// <summary>
        /// Constructor that instantiate the attributes to the values given in input
        /// </summary>
        /// <param name="entitySensor">Class containing all the information of the sensor, except for the location</param>
        public Sensor(GetSensorInformation_Result entitySensor)
        {
            base.ID = entitySensor.Id;
            this.sensorValues = new List<SensorVTData>();
            base.UserName = entitySensor.Name;
            this.upperBoundary = entitySensor.MAX_Critical.GetValueOrDefault();
            this.lowerBoundary = entitySensor.MIN_Critical.GetValueOrDefault();
            base.Location = entitySensor.Location;
            this.unitMetric = entitySensor.Unit;
            base.Avatar = entitySensor.Image;
        }

        /// <summary>
        /// Attribute that represent the list of values that the sensor had
        /// </summary>
        private List<SensorVTData> sensorValues;
        /// <summary>
        /// Properties that allow to modify or take the valuues that the sensor had
        /// </summary>
        public List<SensorVTData> SensorValues
        {
            get
            {
                return sensorValues;
            }
            set
            {
                sensorValues = value;
            }
        }

        /// <summary>
        /// Attribute that represent the lowest value that the sensor can register 
        /// </summary>
        private decimal lowerBoundary;
        /// <summary>
        ///  Properties that allow to modify or take the lowest value that the sensor can register
        /// </summary>
        public decimal LowerBoundary
        {
            get
            {
                return lowerBoundary;
            }
            set
            {
                lowerBoundary = value;
            }
        }

        /// <summary>
        /// Attribute that represent the maximum value that the sensor can register 
        /// </summary>
        private decimal upperBoundary;
        /// <summary>
        ///  Properties that allow to modify or take the maximum value that the sensor can register 
        /// </summary>
        public decimal UpperBoundary
        {
            get
            {
                return upperBoundary;
            }
            set
            {
                upperBoundary = value;
            }
        }

        /// <summary>
        /// Attribute that represent the metric system used by the sensor 
        /// </summary>
        private string unitMetric;
        /// <summary>
        ///  Properties that allow to modify or take the metric system used by the sensor 
        /// </summary>
        public string UnitMetric
        {
            get
            {
                return unitMetric;
            }
            set
            {
                unitMetric = value;
            }
        }
    }
}
