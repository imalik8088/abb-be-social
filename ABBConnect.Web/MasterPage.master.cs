using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

public partial class MasterPage : System.Web.UI.MasterPage
{
    [System.Web.Services.WebMethod]
    public void OnClickSignOut()
    {
        Session.Remove("humanID");
    }
    protected  void Page_Load(object sender, EventArgs e)
    {
    //    UserManager userManager = new UserManager();
    //    Human human = new Human();
    //    if (Session["humanID"] != null)
    //    {
    //        human = await userManager.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));
    //        labelHuman.Text = human.FirstName + " " + human.LastName;
    //    }
    }
}
