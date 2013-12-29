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

public partial class UserProfile : System.Web.UI.Page
{
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

    [System.Web.Services.WebMethod]
    public static List<Category> AjaxGetPostFeedTypes()
    {
        CommonDataManager commonDataManager = new CommonDataManager();
        List<Category> categories = commonDataManager.GetFeedCategories();

        return categories;
    }

    [System.Web.Services.WebMethod]
    public static List<Human> AjaxGetAvailableUsersToTag()
    {
        UserManager userManager = new UserManager();
        List<Human> approximateUserNames = userManager.GetAllHumanUsers();

        return approximateUserNames;
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

    [System.Web.Services.WebMethod]
    public static AjaxFeeds AjaxDisplayNewPublishedHumanFeed(AjaxFeedFilter filter)
    {
        //NOTE: ajaxFeedsHTML is rendered FeedPage, so we return HTML thats going to be appended

        UserManager userManager = new UserManager();
        FeedManager feedManager = new FeedManager();

        Human loggedUser = new Human();
        loggedUser = userManager.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

        int loggedUserLastPostedHumanFeedId = -1;
        loggedUserLastPostedHumanFeedId = feedManager.LoadFeedsByFilter(loggedUser.ID, null, DateTime.MinValue, DateTime.MaxValue, FeedType.FeedSource.Human, 1).FirstOrDefault().ID;

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

    protected void Page_Load(object sender, EventArgs e)
    {
        FeedManager feedManager = new FeedManager();
        int userId = int.Parse(Request.QueryString["userId"].ToString());
        LoadUserProfileData(userId);
        LoadProfileActivity(userId);

        // HumanFeeds
        var lastHumanFeed = (List<Feed>)null;
        // Get last Feed so we can get Id of it
        DateTime filterStartTime = DateTime.MinValue;
        DateTime filterEndTime = DateTime.MaxValue;
        lastHumanFeed = feedManager.LoadFeedsByFilter(-1, null, filterStartTime, filterEndTime, FeedType.FeedSource.Human, 1);
        FeedPage.LastFeedId = lastHumanFeed.First().ID + 1;
        FeedPage.FilterUserId = userId;
        FeedPage.RenderFeedPage();

        //HumanActivities
        ActivityPage.LastFeedId = lastHumanFeed.First().ID + 1;
        ActivityPage.FilterUserId = userId;
        ActivityPage.RenderActivityPage();

        //Call JS Methods
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "populateFeedPostTypes", "<script type='text/javascript'>AjaxPopulateSelectBoxPostFeedType()</script>", false);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "getAvailableUsersToTag", "<script type='text/javascript'>AjaxGetAvailableUsersToTag()</script>", false);
    }
    private void LoadUserProfileData(int userId)
    {       
        humanFeedsFilterUserId.Value = userId.ToString();

        UserManager userManager = new UserManager();
        Human user = new Human();
        user = userManager.LoadHumanInformation(userId);

        litFeedsUserName.Text = "Feeds published by " + user.FirstName + "," + user.LastName;
        litProfileUserName.Text = "Contact information for " + user.FirstName + "," + user.LastName;
        litActivityUserName.Text = user.FirstName +"'s recent activity";
        litUserName.Text = user.UserName;
        litUserPhoneNumber.Text = user.PhoneNumber;
        litUserLocation.Text = user.Location;
        litUserEmail.Text = user.Email;
    }
    private void LoadProfileActivity(int userId)
    {
        FeedManager feedManager = new FeedManager();
        List<Feed> allFeeds =  feedManager.LoadFeedsByFilter(userId, null, DateTime.Today.AddDays(-30), DateTime.Today, FeedType.FeedSource.Human, 100);

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
}