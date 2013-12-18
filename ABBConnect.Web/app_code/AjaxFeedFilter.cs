using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AjaxFeedFilter
/// </summary>
public class AjaxFeedFilter
{
	private DateTime? _startDate;
    private DateTime? _endDate;
    private int? _userId;
    private string _location;

    public DateTime? StartDate
    {
        get { return this._startDate; }
        set { this._startDate = value; }
    }
    public DateTime? EndDate
    {
        get { return this._endDate; }
        set { this._endDate = value; }
    }
    public int? UserId
    {
        get { return this._userId; }
        set { this._userId = value; }
    }
    public string Location
    {
        get { return this._location; }
        set { this._location = value; }
    }

    public AjaxFeedFilter()
    {
    }
    public AjaxFeedFilter(DateTime? startDate, DateTime? endDate, int? userId, string location)
	{
        this.StartDate = startDate;
        this.EndDate = endDate;
	}
}