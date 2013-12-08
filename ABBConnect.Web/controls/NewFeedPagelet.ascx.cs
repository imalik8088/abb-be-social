using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Web.UI.HtmlControls;

public partial class controls_NewFeedPagelet : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FeedManager fm = new FeedManager();
        List<Feed> singlePageFeeds = fm.LoadLatestXFeeds(1);

        FeedRepeater.DataSource = singlePageFeeds;
        FeedRepeater.DataBind();
    }

    protected void FeedRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
            l.Text = "Jacques Cousteau";

            l = (Literal)e.Item.FindControl("litFeedContent");
            l.Text = currentFeed.Content;

            // Render Comments
            Repeater feedCommentsRepeater = (Repeater)e.Item.FindControl("feedCommentsRepeater");
            feedCommentsRepeater.DataSource = currentFeed.Comments;
            feedCommentsRepeater.DataBind();
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        RenderChildren(writer);
    }
}