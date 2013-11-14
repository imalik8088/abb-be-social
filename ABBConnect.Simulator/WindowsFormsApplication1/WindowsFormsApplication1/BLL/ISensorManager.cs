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
namespace ABBConnectSimulator.BLL
{
    interface ISensorManager
    {
        /// <summary>
        /// Fills the internal list of sensors
        /// </summary>
        void FillSensorList();
        /// <summary>
        /// Changes the boundery of a sensor
        /// </summary>
        /// <param name="index"></param>
        /// <param name="uValue"></param>
        /// <param name="lValue"></param>
        /// <returns></returns>
        bool ChangeBoundery(int index, decimal uValue, decimal lValue);
        /// <summary>
        /// Auto generate values for a sensor
        /// </summary>
        /// <param name="index"></param>
        /// <param name="noOfValues"></param>
        void GenerateValues(int index, int noOfValues);
        /// <summary>
        /// Load values to a sensor from a file
        /// </summary>
        /// <param name="index"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        bool LoadValues(int index, string filePath);
        /// <summary>
        /// Insert all the sensors values to the database
        /// </summary>
        void PublishSensorValues();
        /// <summary>
        /// Get all the stored sensors
        /// </summary>
        /// <returns></returns>
        List<Sensor> GetSensors();
        /// <summary>
        /// Get the current boundery values of a sensor
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        decimal[] GetBounderyValues(int index);

    }
}