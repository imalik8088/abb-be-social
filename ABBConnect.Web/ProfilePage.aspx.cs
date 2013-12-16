using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

public partial class _UserPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserManager hm = new UserManager();
        Human human = new Human();
        FeedManager fm = new FeedManager();

        if (Session["humanID"] != null)
        {
            human = hm.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));
        }

        //FeedPage.LastFeedId = fm.LoadLatestXFeeds(1).Single().ID + 1;
    }
}