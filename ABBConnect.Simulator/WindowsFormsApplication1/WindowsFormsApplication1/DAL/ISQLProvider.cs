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
    interface ISQLProvider
    {
        /// <summary>
        /// Load sensor from the database
        /// </summary>
        /// <returns></returns>
        List<Sensor> LoadSensorData();

        /// <summary>
        /// Change the boundery levels of a specific sensor
        /// </summary>
        /// <param name="sensorID"></param>
        /// <param name="lBoundery"></param>
        /// <param name="uBoundery"></param>
        /// <returns></returns>
        bool ChangeBounderyValue(int sensorID, decimal lBoundery, decimal uBoundery);

        /// <summary>
        /// Insert a specific value for a specific sensor in the database
        /// </summary>
        /// <param name="sensorID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool PublishSensorData(int sensorID, int value);
    }
}
