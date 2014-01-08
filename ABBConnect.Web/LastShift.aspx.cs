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
 * Written by: Marta Milakovic
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */

/// <summary>
/// Logic of the LastShift page which represents human and sensor feeds from the previous shift.
/// Logic for the commenting.
/// </summary>
public partial class _LastShift : System.Web.UI.Page
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

        controls_LastShiftFeedPage feedPageContainer = (controls_LastShiftFeedPage)page.LoadControl("controls/LastShiftFeedPage.ascx");
        page.Controls.Add(feedPageContainer);
        feedPageContainer.EnableViewState = false;
        feedPageContainer.LastFeedId = lastLoadedFeedId;
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

        try
        {
            lastHumanFeed = feedManager.LoadLastShiftFeeds(1);
            LastShiftFeedPage.LastFeedId = lastHumanFeed.First().ID + 1;
        }
        catch
        {}
        LastShiftFeedPage.RenderFeedPage();
    }
}