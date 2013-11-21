using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transformation_Layer;
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
            DataTable infoTable = sensorDbData.GetSensorInformation(sensorID);
            Sensor selectedSensor = new Sensor();
            int tempInt = 0;
            bool castRes = false;

            foreach (DataRow row in infoTable.Rows)
            {
                castRes = CastStringToInt(row["Id"].ToString(), ref tempInt);

                if (castRes)
                {
                    selectedSensor.ID = tempInt;
                }

                if ( row["Name"].ToString() == null)
                    selectedSensor.Name = "";
                else
                    selectedSensor.Name = row["Name"].ToString();

                decimal tempDec = CastStringToDecimal(row["MIN_Critical"].ToString());
                if (tempDec != -1)
                {
                    selectedSensor.LowerBoundary = tempDec;
                }

                tempDec = CastStringToDecimal(row["MAX_Critical"].ToString());
                if (tempDec != -1)
                {
                    selectedSensor.UpperBoundary = tempDec;
                }

                selectedSensor.Location = "";

            }

            return selectedSensor;
        }

        public SensorHistoryData LoadHistoryValuesBySensor(int sensorID, DateTime startingTime, DateTime endingTime)
        {
            DataTable historyTable = new DataTable();

            historyTable = sensorDbData.RetrieveHistoricalSensorData(sensorID, startingTime, endingTime);

            SensorHistoryData senHistData = new SensorHistoryData();
            senHistData.Owner.ID = sensorID;

            int tempInt = 0;
            bool castRes = false;
            SensorVTData senData = new SensorVTData();

            foreach (DataRow row in historyTable.Rows)
            {
                castRes = CastStringToInt(row["RawValue"].ToString(), ref tempInt);

                if (castRes)
                    senData.RawData = tempInt;
                else
                    senData.RawData = -100000;

                senData.CreationTime = Convert.ToDateTime(row["CreationTimeStamp"].ToString());

                senHistData.Owner.SensorValues.Add(senData);
                senData = new SensorVTData();
            }

            return senHistData;
        }

        public int LoadCurrentValuesBySensor(int sensorID)
        {
            return this.sensorDbData.RetrieveCurrentSensorData(sensorID);
        }

        private static bool CastStringToInt(string str, ref int returnValue)
        {
            bool result = false;

            try
            {
                returnValue = Convert.ToInt32(str);
                result = true;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Input string is not a sequence of digits.");
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The number cannot fit in an Int32.");
            }

            return result;
        }

        private static decimal CastStringToDecimal(string str)
        {
            decimal returnValue = -1;

            try
            {
                returnValue = Convert.ToDecimal(str);
            }
            catch (System.OverflowException)
            {
                Console.WriteLine(
                    "The conversion from string to decimal overflowed.");
            }
            catch (System.FormatException)
            {
                Console.WriteLine(
                    "The string is not formatted as a decimal.");
            }
            catch (System.ArgumentNullException)
            {
                Console.WriteLine(
                    "The string is null.");
            }

            return returnValue;
        }
    }
}
