using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class Sensor: User
    {
        public Sensor()
        {
            this.sensorValues = new List<SensorVTData>();
            base.UserName = "";
            base.Location = "";
            this.unitMetric = "";
        }

        public Sensor(GetSensorInformation_Result entitySensor)
        {
            base.ID = entitySensor.Id;
            this.sensorValues = new List<SensorVTData>();
            base.UserName = entitySensor.Name;
            this.upperBoundary = entitySensor.MAX_Critical.GetValueOrDefault();
            this.lowerBoundary = entitySensor.MIN_Critical.GetValueOrDefault();
            base.Location = entitySensor.Location;
            this.unitMetric = entitySensor.Unit;
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

        private string unitMetric;
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
