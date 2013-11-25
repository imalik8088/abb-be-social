using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Web.Script.Serialization;


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
    public static Boolean AjaxPostNewFeed(string feedContentData, string feedType, int feedPriority)
    {
        /* Check if user is loged, if not, return null, since we cannot do redirect from WebMethod
         * 
         */
        CommonDataManager cdm = new CommonDataManager();
        FeedManager fm = new FeedManager();
        HumanManager hm = new HumanManager();

        Human feedOwner = new Human();
        feedOwner = hm.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

        HumanFeed newFeed = new HumanFeed();
        newFeed.Content = feedContentData;
        newFeed.Owner = feedOwner;
        newFeed.TimeStamp = DateTime.Now;
        newFeed.Category = cdm.GetFeedCategories().Where(tempCat => tempCat.CategoryName == feedType).Single();

        //TODO create dynamic priority fetching
        newFeed.Priority = feedPriority;

        Boolean result = fm.PublishFeed(newFeed);
        return result;
    }

    [System.Web.Services.WebMethod]
    public static string AjaxGetPostTypes()
    {
        CommonDataManager commonDataManager = new CommonDataManager();
        List<Category> categories = commonDataManager.GetFeedCategories();

        JavaScriptSerializer serializer = new JavaScriptSerializer();

        string returnString = serializer.Serialize(categories);

        return returnString;
    }

    [System.Web.Services.WebMethod]
    public static string AjaxGetPostPriorities()
    {
        //TODO delete in favor of dynamic fetching
        //convert the enums into a dictionary
        var enumType = typeof(Priority);
        var enumDictionary = enumType
            .GetFields()
            .Where(x => x.IsLiteral)
            .ToDictionary(x => x.Name, x => (int)Enum.Parse(enumType, x.Name));

        JavaScriptSerializer serializer = new JavaScriptSerializer();

        string returnString = serializer.Serialize(enumDictionary);

        return returnString;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}