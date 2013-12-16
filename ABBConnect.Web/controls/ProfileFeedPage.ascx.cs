using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BLL;

public partial class controls_ProfileFeedPage : System.Web.UI.UserControl
{
    // Default Feed Settings
    public int PageSize = 10;
    public int LastFeedId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void RenderFeedPage()
    {
        FeedManager feedManager = new FeedManager();
        List<Feed> singlePageFeeds = new List<Feed>();

        UserManager userManager = new UserManager();
        Human human = new Human();

        if (Session["humanID"] != null)
        {
            human = userManager.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

            singlePageFeeds = feedManager.LoadFeedsByUser(human.ID, this.PageSize, LastFeedId + 1);
            FeedRepeater.DataSource = singlePageFeeds;
            FeedRepeater.DataBind();
        }
    }

    protected void FeedRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Literal l = new Literal();
        HtmlAnchor a = new HtmlAnchor();
        HtmlGenericControl hgc = new HtmlGenericControl();

        UserManager userManager = new UserManager();
        Human human = new Human();

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            /* Determine feedType (Human, Sensor) and render innerMessage (FeedContent) for it (TextMessage, SensorWarning, SensorGauge)
             * 
             */

            Feed currentFeed = ((Feed)e.Item.DataItem);

            if (Session["humanID"] != null)
            {
                human = userManager.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

                l = (Literal)e.Item.FindControl("litFeedPosterName");
                l.Text = human.FirstName + " " + human.LastName;
            }

            l = (Literal)e.Item.FindControl("litFeedContent");
            l.Text = currentFeed.Content;

            // Render Comments
            Repeater feedCommentsRepeater = (Repeater)e.Item.FindControl("feedCommentsRepeater");

            if (currentFeed.Comments.Count <= 5)
            {
                feedCommentsRepeater.DataSource = currentFeed.Comments.OrderBy(c => c.TimeStamp);
                feedCommentsRepeater.DataBind();
                ((HtmlGenericControl)e.Item.FindControl("feed_show_all_comments")).Attributes.Add("class", "feed-show-all-comments-container dont-show");
            }

            // Render LoadMore Link
            a = (HtmlAnchor)this.FindControl("load_more");
            a.Attributes.Add("onclick", "$(this).fadeOut(300); AjaxLoadMoreHumanFeeds(" + currentFeed.ID + ")");
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        if (FeedRepeater.Items.Count >= this.PageSize)
        {
            feed_page_load_more_container.Visible = true;
        }
        else
        {
            feed_page_load_more_container.Visible = false;
        }
        RenderChildren(writer);
    }
}