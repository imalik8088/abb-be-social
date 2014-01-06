using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.IO;
using BLL;

/*
 * Written by: 
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */

/// <summary>
/// Logic of the Home page which represents human and sensor feeds and followed sensors.
/// Logic for the filter handling, feed posting and commenting.
/// </summary>
public partial class _Home : System.Web.UI.Page
{
    #region User feed page
    /// <summary>
    /// Retrieving latest user feeds and producing HTML of the user feed page.
    /// </summary>
    /// <param name="lastLoadedFeedId"></param>
    /// <param name="filter"></param>
    [System.Web.Services.WebMethod]
    public static AjaxFeeds AjaxLoadMoreHumanFeeds(int lastLoadedFeedId, AjaxFeedFilter filter)
    {
        //NOTE: ajaxFeedsHTML is rendered FeedPage, so we return HTML thats going to be appended
        if (lastLoadedFeedId == -1) lastLoadedFeedId = int.MaxValue;

        AjaxFeeds ajaxFeedsHTML = new AjaxFeeds(lastLoadedFeedId);
        Page page = new Page();
        page.ClientIDMode = ClientIDMode.Static;

        controls_FeedPage feedPageContainer = (controls_FeedPage)page.LoadControl("controls/FeedPage.ascx");
        page.Controls.Add(feedPageContainer);
        feedPageContainer.EnableViewState = false;
        feedPageContainer.LastFeedId = lastLoadedFeedId;

        if (filter.StartDate != null) feedPageContainer.FilterStartDateValue = (DateTime)filter.StartDate;
        if (filter.EndDate != null) feedPageContainer.FilterEndDateValue = (DateTime)filter.EndDate;
        if (filter.UserId != null) feedPageContainer.FilterUserId = (int)filter.UserId;
        if (filter.Location != null) feedPageContainer.FilterLocation = (string)filter.Location;
        feedPageContainer.RenderFeedPage();

        StringWriter textWriter = new StringWriter();
        HttpContext.Current.Server.Execute(page, textWriter, false);

        ajaxFeedsHTML.FeedsRawData = textWriter.ToString();
        return ajaxFeedsHTML;
    }

    /// <summary>
    /// Retrieving feed categories.
    /// </summary>
    [System.Web.Services.WebMethod]
    public static List<Category> AjaxGetPostFeedTypes()
    {
        CommonDataManager commonDataManager = new CommonDataManager();
        List<Category> categories = commonDataManager.GetFeedCategories();

        return categories;
    }

    /// <summary>
    /// Getting all the data of a new feed and publishing it. 
    /// </summary>
    /// <param name="feedContent"></param>
    /// <param name="feedType"></param>
    /// <param name="feedTaggedUserIds"></param>
    [System.Web.Services.WebMethod]
    public static bool AjaxPublishHumanFeed(string feedContent, string feedType, string feedTaggedUserIds)
    {
        bool actionResult = false;
        UserManager userManager = new UserManager();
        FeedManager feedManager = new FeedManager();
        CommonDataManager commonDataManager = new CommonDataManager();

        Human feedOwner = new Human();
        feedOwner = userManager.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

        List<Human> taggedUsers = new List<Human>();
        if (feedTaggedUserIds.Length > 0)
        {
            string[] feedTaggedUserIdsList = feedTaggedUserIds.Split(',');
            foreach (string taggedUserId in feedTaggedUserIdsList)
            {
                Human fetchedUser = userManager.LoadHumanInformation(int.Parse(taggedUserId));
                taggedUsers.Add(fetchedUser);
            }
        }

        HumanFeed newHumanFeed = new HumanFeed();
        newHumanFeed.FeedType = feedType;
        newHumanFeed.Content = feedContent;
        newHumanFeed.Location = "Madagascar";
        newHumanFeed.TimeStamp = DateTime.Now;
        newHumanFeed.Category = commonDataManager.GetFeedCategories().Where(c => c.CategoryName == feedType).FirstOrDefault();
        newHumanFeed.Tags = taggedUsers;
        newHumanFeed.Owner = feedOwner;
        actionResult = feedManager.PublishFeed(newHumanFeed);
        return actionResult;
    }

    /// <summary>
    /// Retrieving a new feed and producing HTML with the feed contents.
    /// </summary>
    /// <param name="filter"></param>
    [System.Web.Services.WebMethod]
    public static AjaxFeeds AjaxDisplayNewPublishedHumanFeed(AjaxFeedFilter filter)
    {
        //NOTE: ajaxFeedsHTML is rendered FeedPage, so we return HTML thats going to be appended

        UserManager userManager = new UserManager();
        FeedManager feedManager = new FeedManager();

        Human loggedUser = new Human();
        loggedUser = userManager.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

        int loggedUserLastPostedHumanFeedId = -1;
        loggedUserLastPostedHumanFeedId = feedManager.LoadFeedsByFilter(loggedUser.ID, null, DateTime.MinValue, DateTime.MaxValue, FeedType.FeedSource.Human, null, 1).FirstOrDefault().ID;

        AjaxFeeds ajaxFeedsHTML = new AjaxFeeds(loggedUserLastPostedHumanFeedId);
        Page page = new Page();
        page.ClientIDMode = ClientIDMode.Static;

        controls_FeedPage feedPageContainer = (controls_FeedPage)page.LoadControl("controls/FeedPage.ascx");
        page.Controls.Add(feedPageContainer);
        feedPageContainer.EnableViewState = false;
        feedPageContainer.LastFeedId = loggedUserLastPostedHumanFeedId + 1;
        feedPageContainer.PageSize = 1;
        feedPageContainer.IsLoadMoreVisible = false;

        if (filter.StartDate != null) feedPageContainer.FilterStartDateValue = (DateTime)filter.StartDate;
        if (filter.EndDate != null) feedPageContainer.FilterEndDateValue = (DateTime)filter.EndDate;
        if (filter.UserId != null) feedPageContainer.FilterUserId = (int)filter.UserId;
        if (filter.Location != null) feedPageContainer.FilterLocation = (string)filter.Location;
        feedPageContainer.RenderFeedPage();

        StringWriter textWriter = new StringWriter();
        HttpContext.Current.Server.Execute(page, textWriter, false);

        ajaxFeedsHTML.FeedsRawData = textWriter.ToString();
        return ajaxFeedsHTML;
    }

    /// <summary>
    /// Publishing feeds with the picture.
    /// </summary>
    /// <param name="feedContent"></param>
    /// <param name="feedType"></param>
    /// <param name="feedTaggedUserIds"></param>
    /// <param name="base64Picture"></param>
    [System.Web.Services.WebMethod]
    public static bool AjaxPublishHumanPictureFeed(string feedContent, string feedType, string feedTaggedUserIds, string base64Picture)
    {
        bool actionResult = false;
        UserManager userManager = new UserManager();
        FeedManager feedManager = new FeedManager();
        CommonDataManager commonDataManager = new CommonDataManager();

        Human feedOwner = new Human();
        feedOwner = userManager.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

        List<Human> taggedUsers = new List<Human>();
        if (feedTaggedUserIds.Length > 0)
        {
            string[] feedTaggedUserIdsList = feedTaggedUserIds.Split(',');
            foreach (string taggedUserId in feedTaggedUserIdsList)
            {
                Human fetchedUser = userManager.LoadHumanInformation(int.Parse(taggedUserId));
                taggedUsers.Add(fetchedUser);
            }
        }

        HumanFeed newHumanFeed = new HumanFeed();
        newHumanFeed.FeedType = feedType;
        newHumanFeed.Content = feedContent;
        newHumanFeed.Location = "Madagascar";
        newHumanFeed.TimeStamp = DateTime.Now;
        newHumanFeed.Category = commonDataManager.GetFeedCategories().Where(c => c.CategoryName == feedType).FirstOrDefault();
        newHumanFeed.Tags = taggedUsers;
        newHumanFeed.Owner = feedOwner;
        //until this isn't changed to something more semantically correct, we use this field for storing images
        newHumanFeed.MediaFilePath = base64Picture;
        actionResult = feedManager.PublishFeed(newHumanFeed);
        return actionResult;
    }
    #endregion

    #region Sesnor page
    /// <summary>
    /// Loading the sensors and producing HTML for posting it.
    /// </summary>
    /// <param name="lastLoadedFeedId"></param>
    /// <param name="filter"></param>
    [System.Web.Services.WebMethod]
    public static AjaxFeeds AjaxLoadMoreRealTimeSensorFeeds(int lastLoadedFeedId, AjaxFeedFilter filter)
    {
        //NOTE: ajaxFeedsHTML is rendered FeedPage, so we return HTML thats going to be appended
        if (lastLoadedFeedId == -1) lastLoadedFeedId = int.MaxValue;

        AjaxFeeds ajaxFeedsHTML = new AjaxFeeds(lastLoadedFeedId);
        Page page = new Page();
        page.ClientIDMode = ClientIDMode.Static;

        controls_RealTimeSensorFeedPage feedPageContainer = (controls_RealTimeSensorFeedPage)page.LoadControl("controls/RealTimeSensorFeedPage.ascx");
        page.Controls.Add(feedPageContainer);
        feedPageContainer.EnableViewState = false;
        feedPageContainer.LastFeedId = lastLoadedFeedId;

        if (filter.StartDate != null) feedPageContainer.FilterStartDateValue = (DateTime)filter.StartDate;
        if (filter.EndDate != null) feedPageContainer.FilterEndDateValue = (DateTime)filter.EndDate;
        if (filter.UserId != null) feedPageContainer.FilterUserId = (int)filter.UserId;
        if (filter.Location != null) feedPageContainer.FilterLocation = (string)filter.Location;
        feedPageContainer.RenderFeedPage();

        StringWriter textWriter = new StringWriter();
        HttpContext.Current.Server.Execute(page, textWriter, false);

        ajaxFeedsHTML.FeedsRawData = textWriter.ToString();
        return ajaxFeedsHTML;
    }

    /// <summary>
    /// Unsubscribing from the sensor.
    /// </summary>
    /// <param name="sensorId"></param>
    [System.Web.Services.WebMethod]
    public static int AjaxUserUnFollowSensor(int sensorId)
    {
        bool returnValue = false;
        UserManager userManager = new UserManager();
        int userId = int.Parse(HttpContext.Current.Session["humanID"].ToString());
        returnValue = userManager.UnfollowSensor(userId, sensorId);
        if (returnValue == true) return sensorId;
        else return 0;
    }
    #endregion

    #region Comments
    /// <summary>
    /// Publishing user comments.
    /// </summary>
    /// <param name="feedId"></param>
    /// <param name="feedCommentData"></param>
    [System.Web.Services.WebMethod]
    public static int AjaxPostFeedComment(int feedId, string feedCommentData)
    {
        //Check if user is loged, if not, return null, since we cannot do redirect from WebMethod          

        FeedManager feedManager = new FeedManager();
        UserManager userManager = new UserManager();

        Human commentOwner = new Human();
        commentOwner = userManager.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

        Comment feedComment = new Comment();
        feedComment.Content = feedCommentData;
        feedComment.Owner = commentOwner;

        feedManager.PublishComment(feedId, feedComment);
        return feedId;
    }

    /// <summary>
    /// Retrieving feed comments and producing HTML with the content of the comments.
    /// </summary>
    /// <param name="feedId"></param>
    [System.Web.Services.WebMethod]
    public static AjaxFeedComments AjaxGetAllFeedComments(int feedId)
    {
        // Check if user is loged, if not, return null, since we cannot do redirect from WebMethod

        FeedManager feedManager = new FeedManager();
        Page page = new Page();
        page.ClientIDMode = ClientIDMode.Static;

        controls_FeedComments feedCommentsContainer = (controls_FeedComments)page.LoadControl("controls/FeedComments.ascx");
        page.Controls.Add(feedCommentsContainer);
        feedCommentsContainer.EnableViewState = false;
        feedCommentsContainer.FeedId = feedId;
        feedCommentsContainer.RenderFeedComments();

        StringWriter textWriter = new StringWriter();
        HttpContext.Current.Server.Execute(page, textWriter, false);
        String feedCommentsRawData = textWriter.ToString();

        AjaxFeedComments ajaxFeedCommentsHTML = new AjaxFeedComments(feedId, feedCommentsRawData);

        return ajaxFeedCommentsHTML;
    }
    #endregion

    #region Tagging
    /// <summary>
    /// Retrieving all the users for tagging.
    /// </summary>
    [System.Web.Services.WebMethod]
    public static List<Human> AjaxGetAvailableUsersToTag()
    {
        UserManager userManager = new UserManager();
        List<Human> approximateUserNames = userManager.GetAllHumanUsers();

        return approximateUserNames;
    }
    #endregion

    #region Search bar
    /// <summary>
    /// Logic for the Search bar.
    /// Getting the queried users. 
    /// </summary>
    /// <param name="queriedName"></param>
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
    #endregion

    #region User information
    /// <summary>
    /// Checking if the user is logged in.
    /// </summary>
    private void CheckUserLogin()
    {
        //If a login is not present, redirect to the signin page
        if (Session["humanID"] == null)
        {
            Response.Redirect("~/SignIn.aspx");
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //check if user is logged in
        CheckUserLogin();

        FeedManager feedManager = new FeedManager();

        // HumanFeeds
        var lastHumanFeed = (List<Feed>)null;

        // Get last Feed so we can get Id of it
        DateTime filterStartTime = DateTime.MinValue;
        DateTime filterEndTime = DateTime.MaxValue;
        lastHumanFeed = feedManager.LoadFeedsByFilter(-1, null, filterStartTime, filterEndTime, FeedType.FeedSource.Human, null, 1);
        FeedPage.LastFeedId = lastHumanFeed.First().ID + 1;
        FeedPage.RenderFeedPage();

        // SensorFeeds
        var lastSensorFeed = (List<Feed>)null;
        // Get last Feed so we can get Id of it
        lastSensorFeed = feedManager.LoadFeedsByType(FeedType.FeedSource.Sensor, 1);
        RealTimeSensorFeedPage.LastFeedId = lastSensorFeed.First().ID + 1;
        RealTimeSensorFeedPage.RenderFeedPage();

        // User followed RealTime Sensors
        UserFollowedRealTimeSensorPage.ContainerPrefix = "user";
        UserFollowedRealTimeSensorPage.IsFilteredByUser = true;
        UserFollowedRealTimeSensorPage.RenderSensorPage();

        //Call JS Methods
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "populatFeedPostTypes", "<script type='text/javascript'>AjaxPopulateSelectBoxPostFeedType()</script>", false);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "getAvailableUsersToTag", "<script type='text/javascript'>AjaxGetAvailableUsersToTag()</script>", false);
    }
}