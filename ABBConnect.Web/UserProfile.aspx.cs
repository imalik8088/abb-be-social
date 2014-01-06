

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Web.Script.Serialization;

/*
 * Written by: 
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */

/// <summary>
/// Logic of the User Profile page which represents user information, user feeds, activity and activity charts.
/// Logic for the filter handling, feed posting and commenting.
/// </summary>
public partial class UserProfile : System.Web.UI.Page
{
    //the userId loaded for this page
    private int userId;

    #region User feed page
    /// <summary>
    /// Retrieving latest user feeds and producing HTML of the user feed page.
    /// </summary>
    /// <param name="lastLoadedFeedId"></param>
    /// <param name="filter"></param>
    [System.Web.Services.WebMethod]
    public static AjaxFeeds AjaxLoadMoreHumanFeeds(int lastLoadedFeedId, AjaxFeedFilter filter)
    {
        //NOTE: ajaxFeedsHTML is rendered FeedPage, so we return HTML thats going to be appended.
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
    #endregion

    #region User Activity page
    /// <summary>
    /// Loading the users activity and producing charts.
    /// </summary>
    /// <param name="userId"></param>
    private void LoadProfileActivity(int userId)
    {
        FeedManager feedManager = new FeedManager();
        List<Feed> allFeeds = feedManager.LoadFeedsByFilter(userId, null, DateTime.Today.AddDays(-30), DateTime.Today, FeedType.FeedSource.Human, null, 100);

        List<string> distinctDates = allFeeds.OrderBy(i => i.TimeStamp).Select(f => f.TimeStamp.ToShortDateString()).Distinct().ToList();
        foreach (string dateTime in distinctDates)
        {
            int datePostCount = 0;
            datePostCount = allFeeds.Where(f => f.TimeStamp.ToShortDateString() == dateTime).Count();
            profilePostActivityChart.Series[0].Points.AddXY(dateTime, datePostCount);
        }

        string[] daysOfWeek = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        foreach (string dayOfWeek in daysOfWeek)
        {
            int dayPostCount = 0;
            dayPostCount = allFeeds.Where(f => f.TimeStamp.DayOfWeek.ToString() == dayOfWeek).Count();
            profilePostByDayOfWeekActivityChart.Series[0].Points.AddXY(dayOfWeek, dayPostCount);
        }



        string[] feedTypes = { "Sensor Alarm", "Sticky Note", "Work Post", "Vacation Post", "Sensor Boundary Change" };
        foreach (string category in feedTypes)
        {
            int categoryPostCount = 0;
            string categoryWithoutSpace = category.Replace(" ", "");

            categoryPostCount = allFeeds.Where(f => ((HumanFeed)f).Category.CategoryName.ToString() == categoryWithoutSpace).Count();
            profilePostByFeedTypeChart.Series[0].Points.AddXY(category, categoryPostCount);
        }
    }

    /// <summary>
    /// Loginc for the 'Load More' button. 
    /// Retrieving latest user feed and producing HTML for the activity page.
    /// </summary>
    /// <param name="lastLoadedActivityId"></param>
    [System.Web.Services.WebMethod]
    public static AjaxFeeds AjaxLoadMoreHumanActivities(int lastLoadedActivityId)
    {
        //return the HTML for the activities that are going to be appended
        if (lastLoadedActivityId == -1) lastLoadedActivityId = int.MaxValue;

        AjaxFeeds ajaxActivitiesHTML = new AjaxFeeds(lastLoadedActivityId);
        Page page = new Page();
        page.ClientIDMode = ClientIDMode.Static;

        controls_ActivityPage activityPageContainer = (controls_ActivityPage)page.LoadControl("controls/ActivityPage.ascx");
        page.Controls.Add(activityPageContainer);
        activityPageContainer.EnableViewState = false;
        activityPageContainer.LastFeedId = lastLoadedActivityId;

        activityPageContainer.UserId = int.Parse(HttpContext.Current.Session["humanID"].ToString());

        activityPageContainer.RenderActivityPage();

        StringWriter textWriter = new StringWriter();
        HttpContext.Current.Server.Execute(page, textWriter, false);

        ajaxActivitiesHTML.FeedsRawData = textWriter.ToString();
        return ajaxActivitiesHTML;
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
    /// Filling the user information labels.
    /// </summary>
    /// <param name="userId"></param>
    private void LoadUserProfileData(int userId)
    {
        humanFeedsFilterUserId.Value = userId.ToString();

        UserManager userManager = new UserManager();
        Human user = new Human();
        user = userManager.LoadHumanInformation(userId);

        litFeedsUserName.Text = "Feeds published by " + user.FirstName + "," + user.LastName;
        litProfileUserName.Text = "Contact information for " + user.FirstName + "," + user.LastName;
        litActivityUserName.Text = user.FirstName + "'s recent activity";
        litUserName.Text = user.UserName;
        litUserPhoneNumber.Text = user.PhoneNumber;
        litUserLocation.Text = user.Location;
        litUserEmail.Text = user.Email;
        if (user.Avatar == "")
            litAvatar.Text = "<img id='feedAvatar' class='feed-avatar' alt='' src='content/img/avatar-abb.png'>";
        else
            litAvatar.Text = "<img id='feedAvatar' class='feed-avatar' alt='' src='" + user.Avatar + "'>";
    }

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

    /// <summary>
    /// Changing user picture.
    /// </summary>
    [System.Web.Services.WebMethod]
    public static void AjaxChangeAvatar(int userId, String pictureBase64)
    {
        UserManager userManager = new UserManager();

        userManager.AddUserAvatar(userId,pictureBase64);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //check if user is logged in
        CheckUserLogin();

        FeedManager feedManager = new FeedManager();
        int userId = int.Parse(Request.QueryString["userId"].ToString());
        LoadUserProfileData(userId);
        LoadProfileActivity(userId);

        // HumanFeeds
        var lastHumanFeed = (List<Feed>)null;
        // Get last Feed so we can get Id of it
        DateTime filterStartTime = DateTime.MinValue;
        DateTime filterEndTime = DateTime.MaxValue;
        lastHumanFeed = feedManager.LoadFeedsByFilter(-1, null, filterStartTime, filterEndTime, FeedType.FeedSource.Human, null, 1);
        FeedPage.LastFeedId = lastHumanFeed.First().ID + 1;
        FeedPage.FilterUserId = userId;
        FeedPage.RenderFeedPage();

        //HumanActivities
        ActivityPage.LastFeedId = lastHumanFeed.First().ID + 1;
        ActivityPage.UserId = this.userId;
        ActivityPage.RenderActivityPage();

        //Call JS Methods
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "registerAvatarUpload", "<script type='text/javascript'>registerAvatarUpload(" + userId + ")</script>", false); 
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "populateFeedPostTypes", "<script type='text/javascript'>AjaxPopulateSelectBoxPostFeedType()</script>", false);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "getAvailableUsersToTag", "<script type='text/javascript'>AjaxGetAvailableUsersToTag()</script>", false);
    }
}