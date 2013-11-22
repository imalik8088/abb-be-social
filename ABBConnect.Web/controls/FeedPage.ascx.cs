using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BLL;

public partial class controls_FeedPage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FeedManager fm = new FeedManager();
        List<Feed> last20Feeds = fm.LoadLatestXFeeds(100);

        FeedRepeater.DataSource = last20Feeds;
        FeedRepeater.DataBind();        
    }

    protected void FeedRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {      
        Literal l = new Literal();
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