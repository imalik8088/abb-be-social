using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableBLL
{
    /// <summary>
    /// Class that represent a time period of a specific sensor
    /// </summary>
    public class SensorHistoryData
    {
        /// <summary>
        /// Constructor that instanciate the attribute of the class with a specific sensor
        /// </summary>
        /// <param name="owner">Class that represent the sensor</param>
        public SensorHistoryData(Sensor owner)
        {
            this.owner = owner;
            this.startingTime = DateTime.MinValue;
            this.endingTime = DateTime.MinValue;
        }

        /// <summary>
        /// Attribute that represent the sensor
        /// </summary>
        private Sensor owner;
        /// <summary>
        /// Properties that allow to modify or take the sensor
        /// </summary>
        public Sensor Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        /// <summary>
        /// Attribute that represent the starting time when the sensor values are considered
        /// </summary>
        private DateTime startingTime;
        /// <summary>
        /// Properties that allow to modify or take the starting time when the sensor values are considered
        /// </summary>
        public DateTime StartingTime
        {
            get
            {
                return startingTime;
            }
            set
            {
                startingTime = value;
            }
        }

        /// <summary>
        /// Attribute that represent the ending time when the sensor values are considered
        /// </summary>
        private DateTime endingTime;
        /// <summary>
        /// Properties that allow to modify or take the ending time when the sensor values are considered
        /// </summary>
        public DateTime EndingTime
        {
            get
            {
                return endingTime;
            }
            set
            {
                endingTime = value;
            }
        }
    }
}
