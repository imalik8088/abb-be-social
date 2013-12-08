using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

public partial class controls_Commentlet : System.Web.UI.UserControl
{
    AjaxFeedComments comments;
    public int FeedId
    {
        get
        {
            return this.comments.FeedId;
        }
        set
        {
            this.comments = new AjaxFeedComments(value, "");

            this.comments.FeedId = value;

            FeedManager fm = new FeedManager();

            //TODO change!
            this.feed = (HumanFeed)fm.LoadLatestXFeedsFromId(value + 1, 1).Single();

            feedCommentsRepeater.DataSource = this.feed.Comments;
            feedCommentsRepeater.DataBind();
        }
    }

    HumanFeed feed;
    public HumanFeed Feed
    {
        get
        {
            return this.feed;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}