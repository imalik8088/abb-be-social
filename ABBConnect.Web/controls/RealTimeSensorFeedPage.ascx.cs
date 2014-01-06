using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BLL;

/*
 * Written by: 
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */

/// <summary>
/// Logic for rendering sensor feed page.
/// </summary>
public partial class controls_RealTimeSensorFeedPage : System.Web.UI.UserControl
{
    // Default Feed Settings
    public int PageSize = 5;
    public int LastFeedId = 0;
    public bool IsLoadMoreVisible = true;

    public DateTime FilterStartDateValue;
    public DateTime FilterEndDateValue;
    public int FilterUserId = -1;
    public string FilterLocation = null;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Loading and rendering sensor feeds for the page.
    /// </summary>
    public void RenderFeedPage()
    {        
        FeedManager feedManager = new FeedManager();

        List<Feed> singlePageFeeds = new List<Feed>();
        singlePageFeeds = feedManager.LoadFeedsByFilter(this.FilterUserId, this.FilterLocation, this.FilterStartDateValue, this.FilterEndDateValue, FeedType.FeedSource.Sensor, LastFeedId, this.PageSize);
      
        RealTimeSensorFeedRepeater.DataSource = singlePageFeeds;
        RealTimeSensorFeedRepeater.DataBind();
    }

    /// <summary>
    /// Rendering feed content and load more link.
    /// </summary>
    protected void RealTimeSensorFeedRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Literal l = new Literal();  
        HtmlAnchor a = new HtmlAnchor();
        HtmlGenericControl hgc = new HtmlGenericControl();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            /* Determine feedType (Human, Sensor) and render innerMessage (FeedContent) for it (TextMessage, SensorWarning, SensorGauge)
             * 
             */

            Feed currentFeed = ((Feed)e.Item.DataItem);

            l = (Literal)e.Item.FindControl("litFeedPosterName");
            l.Text = "Sensor";

            l = (Literal)e.Item.FindControl("litFeedContent");
            l.Text = currentFeed.Content;
       
            // Render LoadMore Link
            a = (HtmlAnchor)this.FindControl("load_more");
            a.Attributes.Add("onclick", "$(this).fadeOut(300); AjaxLoadMoreRealTimeSensorFeeds(" + currentFeed.ID + ")");
        }
    }

    /// <summary>
    /// Determining the state of Load more container and rendering data.
    /// </summary>
    protected override void Render(HtmlTextWriter writer)
    {
        if (RealTimeSensorFeedRepeater.Items.Count >= this.PageSize)
        {
            feed_page_load_more_container.Visible = true;
        }
        else
        {
            feed_page_load_more_container.Visible = false;
        }
        if (IsLoadMoreVisible == false) feed_page_load_more_container.Visible = false;
        RenderChildren(writer);
    }
}