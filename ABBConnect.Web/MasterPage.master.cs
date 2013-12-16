using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserManager hm = new UserManager();
        Human human = new Human();
        if (Session["humanID"] != null)
        {
            human = hm.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));
            labelHuman.Text = human.FirstName + " " + human.LastName;
        }
    }

    [System.Web.Services.WebMethod]
    public void OnClickSignOut()
    {
        Session.Abandon();
    }
}
