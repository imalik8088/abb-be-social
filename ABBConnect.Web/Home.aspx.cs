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
    public static int AjaxPostFeedComment(int feedId, string feedCommentData)
    {
        /* Check if user is loged, if not, return null, since we cannot do redirect from WebMethod
         * 
         */
        
       FeedManager fm = new FeedManager();      
       HumanManager hm = new HumanManager();

       Human commentOwner = new Human();
       commentOwner = hm.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));
        
       Comment feedComment = new Comment();
       feedComment.Content = feedCommentData;
       feedComment.Owner = commentOwner;
       feedComment.TimeStamp = DateTime.Now;
        
       fm.PublishComment(feedId, feedComment);
       return feedId;

       //DEBUG return "INSERT EXECUTED {" + feedId + " , " + int.Parse(HttpContext.Current.Session["humanID"].ToString()) + "} @" + DateTime.Now.ToLongTimeString();
    }
    [System.Web.Services.WebMethod]
    public static AjaxFeedComments AjaxGetAllFeedComments(int feedId)
    {
        /* Check if user is loged, if not, return null, since we cannot do redirect from WebMethod
         * 
         */
        AjaxFeedComments afc = new AjaxFeedComments(feedId, "hello");
        return afc;
    }

    [System.Web.Services.WebMethod]
    public static Boolean AjaxPostNewFeed(string feedContentData)
    {
        /* Check if user is loged, if not, return null, since we cannot do redirect from WebMethod
         * 
         */

        FeedManager fm = new FeedManager();
        HumanManager hm = new HumanManager();

        Human feedOwner = new Human();
        //feedOwner = hm.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

        HumanFeed newFeed = new HumanFeed();
        newFeed.Content = feedContentData;
        newFeed.Owner = feedOwner;
        newFeed.TimeStamp = DateTime.Now;

        //Boolean result = fm.PublishFeed(newFeed);
        //return result;
        return true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}