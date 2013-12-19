using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AjaxFeeds
/// </summary>
public class AjaxFeeds
{
    private int _lastLoadedFeedID;  // Id of LastLoadedFeed from previous container
    private string _feedsRawData;    // HTML of Feeds 

	public int LastLoadedFeedID
    {
        get { return this._lastLoadedFeedID; }
        set { this._lastLoadedFeedID = value; }
    }
    public string FeedsRawData
    {
        get { return this._feedsRawData; }
        set { this._feedsRawData = value; }
    }
    public AjaxFeeds(int lastLoadedFeedID)
	{
        this._lastLoadedFeedID = lastLoadedFeedID;
	}
    public AjaxFeeds()
    {
        this._lastLoadedFeedID = 0;
    }
}