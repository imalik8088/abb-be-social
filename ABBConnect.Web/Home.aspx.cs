using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;


public partial class _Home : System.Web.UI.Page
{
    [System.Web.Services.WebMethod]
    public static string AjaxPostFeedComment(int feedId)
    {
        FeedManager fm = new FeedManager();

        List<Feed> last20Feeds = fm.LoadLatestXFeeds(20);
        return last20Feeds[0].Content.ToString() + "@" + DateTime.Now.ToLongTimeString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}