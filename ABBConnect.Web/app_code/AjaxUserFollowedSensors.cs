using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AjaxUserFollowedSensors
/// </summary>
public class AjaxUserFollowedSensors
{	
    private string _sensorsRawData;    // HTML of Sensors 
    public string SensorsRawData
    {
        get { return this._sensorsRawData; }
        set { this._sensorsRawData = value; }
    }
    public AjaxUserFollowedSensors(string userFollowedSensorsRawData)
	{
        this._sensorsRawData = userFollowedSensorsRawData;
	}
}