﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Core
/// </summary>
public class Core
{
    static public string GetPriorityCssClass(string strPriorityId)
    {
        string cssClassName = "default";
        int intPriorityId = 5;
        int.TryParse(strPriorityId, out intPriorityId);

        switch (intPriorityId)
        {
            case 5:
                cssClassName = "sensor";
                break;
            case 4:
                cssClassName = "sticky";
                break;
            case 3:
                cssClassName = "work";
                break;
            case 2:
                cssClassName = "vacation";
                break;
            case 1:
                cssClassName = "default";
                break;
        }
        return cssClassName;
    }
    static public string ConvertStringToUppercaseFirst(string strInput)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(strInput))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(strInput[0]) + strInput.Substring(1);
    }
}