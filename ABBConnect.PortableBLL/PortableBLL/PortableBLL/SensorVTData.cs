using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableBLL
{
    /// <summary>
    /// Class that represent a value regestired from a sensor
    /// </summary>
    public class SensorVTData
    {
        /// <summary>
        /// Constructor that instatiate automatically the attributes of the class with empty values
        /// </summary>
        public SensorVTData()
        {
            this.creationTime = DateTime.MinValue;
        }

        /// <summary>
        /// Attribute that rappresent the date of creation of the sensor value
        /// </summary>
        private DateTime creationTime;
        /// <summary>
        /// properties that allow to modify or take the date of creation of the sensor value
        /// </summary>
        public DateTime CreationTime
        {
            get
            {
                return creationTime;
            }
            set
            {
                creationTime = value;
            }
        }

        /// <summary>
        /// Attribute that rappresent the value regestered by the sensor
        /// </summary>
        private int rawData;
        /// <summary>
        /// properties that allow to modify or take the value regestered by the sensor
        /// </summary>
        public int RawData
        {
            get
            {
                return rawData;
            }
            set
            {
                rawData = value;
            }
        }
    }
}
