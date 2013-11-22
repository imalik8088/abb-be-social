using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AjaxFeedComments
/// </summary>
public class AjaxFeedComments
{
    private int _feedId;
    private string _feedCommentsRawData;    // HTML of comments 

    public int FeedId
    {
        get { return this._feedId; }
        set { this._feedId = value; }
    }
    public string FeedCommentsRawData
    {
        get { return this._feedCommentsRawData; }
        set { this._feedCommentsRawData = value; }
    }
	public AjaxFeedComments(int feedId, string feedCommentsRawData)
	{
        this.FeedId = feedId;
        this.FeedCommentsRawData = feedCommentsRawData;
	}
}