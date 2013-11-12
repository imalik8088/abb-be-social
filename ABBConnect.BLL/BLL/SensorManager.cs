using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd;
using System.Data;

namespace BLL
{
    public class SensorManager:ISensorManager
    {
        private SensorData sensorDbData;

        public SensorManager()
        {
            this.sensorDbData = new SensorData();
        }

        public Sensor LoadSensorInformation(int sensorID)
        {
            DataTable infoTable = sensorDbData.RetrieveSensorInformation(sensorID);
            Sensor selectedSensor = new Sensor();

            foreach (DataRow row in infoTable.Rows)
            {
                int tempInt = CastStringToInt(row["Id"].ToString());
                
                if (tempInt != -1)
                {
                    selectedSensor.ID = tempInt;
                }
                selectedSensor.Name = row["Name"].ToString();

                tempInt = CastStringToInt(row["MIN_Critical"].ToString());
                if (tempInt != -1)
                {
                    selectedSensor.LowerBoundary = tempInt;
                }

                tempInt = CastStringToInt(row["MAX_Critical"].ToString());
                if (tempInt != -1)
                {
                    selectedSensor.UpperBoundary = tempInt;
                }

            }

            return selectedSensor;
        }

        public SensorHistoryData LoadHistoryValuesBySensor(int sensorID)
        {
            throw new NotImplementedException();
        }

        public int LoadCurrentValuesBySensor(int sensorID)
        {
            return this.sensorDbData.RetrieveCurrentSensorData(sensorID);
        }

        private static int CastStringToInt(string str)
        {
            int returnValue = -1;

            try
            {
                returnValue = Convert.ToInt32(str);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Input string is not a sequence of digits.");
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The number cannot fit in an Int32.");
            }

            return returnValue;
        }
    }
}
