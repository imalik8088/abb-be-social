using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

public partial class controls_FeedComments : System.Web.UI.UserControl
{
    public int FeedId;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void RenderFeedComments()
    {
        FeedManager feedManager = new FeedManager();
        List<Comment> feedComments = feedManager.LoadFeedComments(this.FeedId);

        feedCommentsRepeater.DataSource = feedComments.OrderBy(c=> c.TimeStamp);
        feedCommentsRepeater.DataBind();      
    }
}