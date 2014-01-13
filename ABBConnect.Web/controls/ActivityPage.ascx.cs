using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class controls_ActivityPage : System.Web.UI.UserControl
{
    // Default Feed Settings
    public int PageSize = 10;
    public int LastFeedId = 0;
    public bool IsLoadMoreVisible = true;

    public int UserId = -1;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void RenderActivityPage()
    {
        UserManager um = new UserManager();
        List<Activity> activities = new List<Activity>();

        activities = um.GetUserActivity(UserId, PageSize, LastFeedId);
        ActivityRepeater.DataSource = activities;
        ActivityRepeater.DataBind();

    }

    protected void ActivityRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Literal l = new Literal();
        HtmlAnchor a = new HtmlAnchor();
        HtmlGenericControl hgc = new HtmlGenericControl();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            /* Determine feedType (Human, Sensor) and render innerMessage (FeedContent) for it (TextMessage, SensorWarning, SensorGauge)
             * 
             */

            Activity currentActivity = ((Activity)e.Item.DataItem);

            l = (Literal)e.Item.FindControl("litFeedContent");
            l.Text = currentActivity.Text;


            // Render LoadMore Link
            a = (HtmlAnchor)this.FindControl("load_more");
            a.Attributes.Add("onclick", "$(this).fadeOut(300); AjaxLoadMoreHumanActivities(" + currentActivity.ID + ")");
        }
    }
    protected override void Render(HtmlTextWriter writer)
    {
        if (ActivityRepeater.Items.Count >= this.PageSize)
        {
            activity_page_load_more_container.Visible = true;
        }
        else
        {
            activity_page_load_more_container.Visible = false;
        }
        if (IsLoadMoreVisible == false) activity_page_load_more_container.Visible = false;
        RenderChildren(writer);
    }
}