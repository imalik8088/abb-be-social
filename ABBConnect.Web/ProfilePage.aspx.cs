using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BLL;

public partial class _UserPage : System.Web.UI.Page
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

        if (filter.StartDate != null)
        {
            feedPageContainer.FilterStartDateValue = (DateTime)filter.StartDate;
        }
        if (filter.EndDate != null)
        {
            feedPageContainer.FilterEndDateValue = (DateTime)filter.EndDate;
        }
        feedPageContainer.RenderFeedPage();

        StringWriter textWriter = new StringWriter();
        HttpContext.Current.Server.Execute(page, textWriter, false);

        ajaxFeedsHTML.FeedsRawData = textWriter.ToString();
        return ajaxFeedsHTML;
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
        feedComment.TimeStamp = DateTime.Now;

        fm.PublishComment(feedId, feedComment);
        return feedId;
    }

    [System.Web.Services.WebMethod]
    public static AjaxFeedComments AjaxGetAllFeedComments(int feedId)
    {
        /* Check if user is loged, if not, return null, since we cannot do redirect from WebMethod
         * 
         */

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

    //[System.Web.Services.WebMethod]
    //public static Boolean AjaxPostNewFeed(string feedContentData, string feedType)
    //{
    //    /* Check if user is loged, if not, return null, since we cannot do redirect from WebMethod
    //     * 
    //     */
    //    CommonDataManager cdm = new CommonDataManager();
    //    FeedManager fm = new FeedManager();
    //    UserManager hm = new UserManager();

    //    Human feedOwner = new Human();
    //    feedOwner = hm.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

    //    HumanFeed newFeed = new HumanFeed();
    //    newFeed.Content = feedContentData;
    //    newFeed.Owner = feedOwner;
    //    newFeed.TimeStamp = DateTime.Now;
    //    newFeed.Category = cdm.GetFeedCategories().Where(tempCat => tempCat.CategoryName == feedType).Single();
    //    Boolean result = fm.PublishFeed(newFeed);

    //    return result;
    //}

    //[System.Web.Services.WebMethod]
    //public static AjaxFeeds AjaxDisplayNewFeed()
    //{
    //    /* Check if user is loged, if not, return null, since we cannot do redirect from WebMethod
    //     * 
    //     */
    //    CommonDataManager cdm = new CommonDataManager();
    //    FeedManager fm = new FeedManager();
    //    UserManager hm = new UserManager();

    //    HumanFeed lastFeed = (HumanFeed)fm.LoadLatestXFeeds(1).Single();

    //    AjaxFeeds af = new AjaxFeeds();
    //    Page page = new Page();
    //    page.ClientIDMode = ClientIDMode.Static;

    //    controls_NewFeedPagelet fp = (controls_NewFeedPagelet)page.LoadControl("controls/NewFeedPagelet.ascx");

    //    page.Controls.Add(fp);
    //    fp.EnableViewState = false;

    //    StringWriter textWriter = new StringWriter();
    //    HttpContext.Current.Server.Execute(page, textWriter, false);

    //    af.FeedsRawData = textWriter.ToString();
    //    return af;
    //}

    //[System.Web.Services.WebMethod]
    //public static string AjaxGetPostTypes()
    //{
    //    CommonDataManager commonDataManager = new CommonDataManager();
    //    List<Category> categories = commonDataManager.GetFeedCategories();

    //    JavaScriptSerializer serializer = new JavaScriptSerializer();

    //    string returnString = serializer.Serialize(categories);

    //    return returnString;
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        FeedManager feedManager = new FeedManager();
        var lastMixedFeed = (List<Feed>)null;

        UserManager hm = new UserManager();
        Human human = new Human();
        FeedManager fm = new FeedManager();

        if (Session["humanID"] != null)
        {
            human = hm.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));

            UserName.Text = human.FirstName + " " + human.LastName;
            UserLocation.Text = human.Location;
            UserEmail.Text = human.Email;
            UserPhoneNumber.Text = human.PhoneNumber;
        }
        // Get last Feed so we can get Id of it

        lastMixedFeed = feedManager.LoadFeedsByUser(human.ID, 1);
        if (lastMixedFeed.Count > 0)
        {
            ProfileFeedPage.LastFeedId = lastMixedFeed.First().ID;
            ProfileFeedPage.RenderFeedPage();
        }
    }
}