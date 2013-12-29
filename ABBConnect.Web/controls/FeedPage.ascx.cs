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

public partial class controls_FeedPage : System.Web.UI.UserControl
{
    // Default Feed Settings
    public int PageSize = 10;
    public int LastFeedId = 0;
    public bool IsLoadMoreVisible = true;

    public DateTime FilterStartDateValue;
    public DateTime FilterEndDateValue;
    public int FilterUserId = -1;
    public string FilterLocation = null;

    protected void Page_Load(object sender, EventArgs e)
    {
     
    }
    public void RenderFeedPage()
    {        
        FeedManager feedManager = new FeedManager();
        List<Feed> singlePageFeeds = new List<Feed>();
        
        singlePageFeeds = feedManager.LoadFeedsByFilter(this.FilterUserId, this.FilterLocation, this.FilterStartDateValue, this.FilterEndDateValue, FeedType.FeedSource.Human, LastFeedId, this.PageSize);
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
            HumanFeed currentHumanFeed = (HumanFeed)currentFeed;

            l = (Literal)e.Item.FindControl("litFeedPosterName");
            //l.Text = currentHumanFeed.Owner.UserName;  
            l.Text = currentHumanFeed.Owner.FirstName + " " + currentHumanFeed.Owner.LastName;

            l = (Literal)e.Item.FindControl("litFeedContent");
            l.Text = currentFeed.Content;

            // Render Comments
            Repeater feedCommentsRepeater = (Repeater)e.Item.FindControl("feedCommentsRepeater");
            
            // Reverse loaded Comments order because of mobile BLL
            if (currentFeed.Comments.Count <= 5)
            {
                feedCommentsRepeater.DataSource = currentFeed.Comments.OrderBy(c => c.TimeStamp);
                feedCommentsRepeater.DataBind();
                ((HtmlGenericControl)e.Item.FindControl("feed_show_all_comments")).Attributes.Add("class", "feed-show-all-comments-container dont-show");     
            }

            // Render Tags
            HtmlInputText hit = (HtmlInputText)e.Item.FindControl("feed_input_tags");
            string feedTags = string.Empty;

            if (currentFeed.Tags.Count > 0)
            {
                foreach (Human tagedHuman in currentFeed.Tags)
                {
                    //feedTags = feedTags + "," + tagedHuman.UserName;
                    feedTags = feedTags + "," + tagedHuman.FirstName + " " + tagedHuman.LastName;
                }
                // Quick fix for laziness
                if (feedTags.Length > 0)
                {
                    feedTags = feedTags.Remove(0, 1);
                   // feedTags = feedTags.Remove(feedTags.LastIndexOf(","));
                }
            }
            else
            {
                feedTags = feedTags + "Unfortunately there are no tags available";
            }
            hit.Attributes.Add("value", feedTags);

            // Render LoadMore Link
            a = (HtmlAnchor)this.FindControl("load_more");
            a.Attributes.Add("onclick", "$(this).fadeOut(300); AjaxLoadMoreHumanFeeds(" + currentFeed.ID +  ")");
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
        if (IsLoadMoreVisible == false) feed_page_load_more_container.Visible = false;
        RenderChildren(writer);
    }
}