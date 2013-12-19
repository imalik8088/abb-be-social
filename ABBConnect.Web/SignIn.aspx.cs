using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

public partial class SignIn : System.Web.UI.Page
{
    public void LoginButtonClick(Object sender, EventArgs e)
    {
        UserManager userManager = new UserManager();
        Human human = new Human();
        string userName = txtUsername.Value;
        string password = txtPassword.Value;

        human = userManager.LoadHumanInformationByUsername(userName);

        bool serviceResult = userManager.Login(userName, password);
        if (serviceResult == true)
        {
            Session.Add("humanID", human.ID);
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