using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

public partial class SignIn : System.Web.UI.Page
{
    public void LoginButtonClick(Object sender, EventArgs e)
    {
        UserManager um = new UserManager();
        User user = new User();
        string userName = txtUsername.Value;
        string password = txtPassword.Value;

        user = um.LoadUserInformation(userName);

        if (um.Login(userName, password))
        {
            Response.Redirect("Home.aspx");
        }
        else
        {
            LoginSuccedLabel.Visible = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}