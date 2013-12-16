using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Web.Script.Serialization;
using System.IO;


public partial class _Home : System.Web.UI.Page
{
    [System.Web.Services.WebMethod]
    public static AjaxFeeds AjaxLoadMoreFeeds(int lastLoadedFeedId)
    {
        AjaxFeeds af = new AjaxFeeds(lastLoadedFeedId);
        Page page = new Page();
        page.ClientIDMode = ClientIDMode.Static;

        controls_FeedPage fp = (controls_FeedPage)page.LoadControl("controls/FeedPage.ascx");
        page.Controls.Add(fp);
        fp.EnableViewState = false;
        fp.LastFeedId = lastLoadedFeedId;

        StringWriter textWriter = new StringWriter();
        HttpContext.Current.Server.Execute(page, textWriter, false);

        af.FeedsRawData = textWriter.ToString();
        return af;
    }
    [System.Web.Services.WebMethod]
    public static int AjaxPostFeedComment(int feedId, string feedCommentData)
    {
        /* Check if user is loged, if not, return null, since we cannot do redirect from WebMethod
         * 
         */

        FeedManager fm = new FeedManager();
        UserManager hm = new UserManager();

        Human commentOwner = new Human();
        commentOwner = hm.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

        Comment feedComment = new Comment();
        feedComment.Content = feedCommentData;
        feedComment.Owner = commentOwner;
        //feedComment.TimeStamp = DateTime.Now;

        fm.PublishComment(feedId, feedComment);
        return feedId;
    }

    [System.Web.Services.WebMethod]
    public static AjaxFeedComments AjaxGetAllFeedComments(int feedId)
    {
        /* Check if user is loged, if not, return null, since we cannot do redirect from WebMethod
         * 
         */
        FeedManager fm = new FeedManager();

        Page page = new Page();
        page.ClientIDMode = ClientIDMode.Static;

        controls_Commentlet fp = (controls_Commentlet)page.LoadControl("controls/Commentlet.ascx");

        fp.FeedId = feedId;

        page.Controls.Add(fp);
        fp.EnableViewState = false;

        StringWriter textWriter = new StringWriter();
        HttpContext.Current.Server.Execute(page, textWriter, false);

        String commentsRawData = textWriter.ToString();


        AjaxFeedComments afc = new AjaxFeedComments(feedId, commentsRawData);

        return afc;
    }

    [System.Web.Services.WebMethod]
    public static Boolean AjaxPostNewFeed(string feedContentData, string feedType)
    {
        /* Check if user is loged, if not, return null, since we cannot do redirect from WebMethod
         * 
         */
        CommonDataManager cdm = new CommonDataManager();
        FeedManager fm = new FeedManager();
        UserManager hm = new UserManager();

        Human feedOwner = new Human();
        feedOwner = hm.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

        HumanFeed newFeed = new HumanFeed();
        newFeed.Content = feedContentData;
        newFeed.Owner = feedOwner;
        newFeed.TimeStamp = DateTime.Now;
        newFeed.Category = cdm.GetFeedCategories().Where(tempCat => tempCat.CategoryName == feedType).Single();
        Boolean result = fm.PublishFeed(newFeed);

        return result;
    }

    [System.Web.Services.WebMethod]
    public static Boolean AjaxPostNewFeed(string feedContentData, string feedType, string taggedUsersString)
    {
        /* Check if user is loged, if not, return null, since we cannot do redirect from WebMethod
         * 
         */
        CommonDataManager cdm = new CommonDataManager();
        FeedManager fm = new FeedManager();
        UserManager hm = new UserManager();

        string[] taggedUsernames = taggedUsersString.Split(',');

        Human feedOwner = new Human();
        feedOwner = hm.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

        //let's fetch all the tagged users by username
        List<Human> taggedHumans = new List<Human>();

        foreach(string taggedUsername in taggedUsernames)
        {
            Human fetchedUser = hm.LoadHumanInformationByUsername(taggedUsername);
            taggedHumans.Add(fetchedUser);
        }

        HumanFeed newFeed = new HumanFeed();
        newFeed.Content = feedContentData;
        newFeed.Owner = feedOwner;
        newFeed.TimeStamp = DateTime.Now;
        newFeed.Category = cdm.GetFeedCategories().Where(tempCat => tempCat.CategoryName == feedType).Single();
        newFeed.Tags = taggedHumans;
        Boolean result = fm.PublishFeed(newFeed);

        return result;
    }

    [System.Web.Services.WebMethod]
    public static AjaxFeeds AjaxDisplayNewFeed()
    {
        /* Check if user is loged, if not, return null, since we cannot do redirect from WebMethod
         * 
         */
        CommonDataManager cdm = new CommonDataManager();
        FeedManager fm = new FeedManager();
        UserManager hm = new UserManager();

        HumanFeed lastFeed = (HumanFeed)fm.LoadLatestXFeeds(1).Single();

        AjaxFeeds af = new AjaxFeeds();
        Page page = new Page();
        page.ClientIDMode = ClientIDMode.Static;

        controls_NewFeedPagelet fp = (controls_NewFeedPagelet)page.LoadControl("controls/NewFeedPagelet.ascx");

        page.Controls.Add(fp);
        fp.EnableViewState = false;

        StringWriter textWriter = new StringWriter();
        HttpContext.Current.Server.Execute(page, textWriter, false);

        af.FeedsRawData = textWriter.ToString();
        return af;
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
    public static string AjaxGetQueriedUsers(string queriedName)
    {
        UserManager um = new UserManager();
        List<User> approximateUserNames = um.SearchUserByName(queriedName);

        //filter out the humans
        List<Human> approximateHumans = new List<Human>();
        //some casting magic from User to Human
        approximateHumans.AddRange(approximateUserNames.Where(x => x is Human).Cast<Human>());

        JavaScriptSerializer serializer = new JavaScriptSerializer();

        string returnString = serializer.Serialize(approximateHumans);

        return returnString;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        FeedManager fm = new FeedManager();

        FeedPage.LastFeedId = fm.LoadLatestXFeeds(1).Single().ID + 1;
    }
}