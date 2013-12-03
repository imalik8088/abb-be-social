using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;

namespace BLL
{
    public class Sensor
    {
        public Sensor()
        {
            this.sensorValues = new List<SensorVTData>();
            this.name = "";
            this.location = "";
        }

        public Sensor(GetSensorInformation_Result entitySensor)
        {
            this.iD = entitySensor.Id;
            this.sensorValues = new List<SensorVTData>();
            this.name = entitySensor.Name;
            this.upperBoundary = entitySensor.MAX_Critical.GetValueOrDefault();
            this.lowerBoundary = entitySensor.MIN_Critical.GetValueOrDefault();
            this.location = "";
        }

        private int iD;
        public int ID
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
        }

        

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string location;
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        private List<SensorVTData> sensorValues;
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

        private decimal lowerBoundary;
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

        private decimal upperBoundary;
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
    }
}
