using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BLL
{
    public class SensorManager:ISensorManager
    {
    

        public SensorManager()
        {
            ;
        }

        public Sensor LoadSensorInformation(int sensorID)
        {
            throw new NotImplementedException();

        }

        public SensorHistoryData LoadHistoryValuesBySensor(int sensorID, DateTime startingTime, DateTime endingTime)
        {
            throw new NotImplementedException();

        }

        public int LoadCurrentValuesBySensor(int sensorID)
        {
            throw new NotImplementedException();

        }

        private static bool CastStringToInt(string str, ref int returnValue)
        {
            throw new NotImplementedException();

        }

        private static decimal CastStringToDecimal(string str)
        {
            throw new NotImplementedException();

        }
    }
}
