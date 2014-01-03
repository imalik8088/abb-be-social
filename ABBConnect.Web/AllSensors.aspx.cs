using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AllSensors : System.Web.UI.Page
{
    [System.Web.Services.WebMethod]
    public static int AjaxUserFollowSensor(int sensorId)
    {
        bool returnValue = false;
        UserManager userManager = new UserManager();
        int userId = int.Parse(HttpContext.Current.Session["humanID"].ToString());
        returnValue = userManager.FollowSensor(userId, sensorId);
        if (returnValue == true) return sensorId;
        else return 0;           
    }
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
    [System.Web.Services.WebMethod]
    public static AjaxUserFollowedSensors AjaxGetAllUserFollowedSensors()
    {
        // Check if user is loged, if not, return null, since we cannot do redirect from WebMethod

        FeedManager feedManager = new FeedManager();
        Page page = new Page();
        page.ClientIDMode = ClientIDMode.Static;

        controls_RealTimeSensorPage userFollowedSensorsContainer = (controls_RealTimeSensorPage)page.LoadControl("controls/RealTimeSensorPage.ascx");
        page.Controls.Add(userFollowedSensorsContainer);
        userFollowedSensorsContainer.EnableViewState = true;
        userFollowedSensorsContainer.ContainerPrefix = "user";
        userFollowedSensorsContainer.IsFilteredByUser = true;
        userFollowedSensorsContainer.RenderSensorPage();

        StringWriter textWriter = new StringWriter();
        HttpContext.Current.Server.Execute(page, textWriter, false);
        String userFollowedSensorsRawData = textWriter.ToString();

        AjaxUserFollowedSensors ajaxUserFollowedSensorsHTML = new AjaxUserFollowedSensors(userFollowedSensorsRawData);

        return ajaxUserFollowedSensorsHTML;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //check if user is logged in
        CheckUserLogin();

        AllRealTimeSensorPage.ContainerPrefix = "all";
        AllRealTimeSensorPage.IsFilteredByUser = false;
        AllRealTimeSensorPage.RenderSensorPage();

        UserFollowedRealTimeSensorPage.ContainerPrefix = "user";
        UserFollowedRealTimeSensorPage.IsFilteredByUser = true;
        UserFollowedRealTimeSensorPage.RenderSensorPage();
    }

    private void CheckUserLogin()
    {
        //If a login is not present, redirect to the signin page
        if (Session["humanID"] == null)
        {
            Response.Redirect("~/SignIn.aspx");
        }
    }
}