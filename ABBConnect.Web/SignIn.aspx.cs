using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

/*
 * Written by: 
 * Project: Social Media in the Process Automation Industry (ABB Connect)
 */

/// <summary>
/// Logic of the Login session
/// </summary>
public partial class SignIn : System.Web.UI.Page
{
    /// <summary>
    // On user Login button click authenticate the user information and make a user session.
    /// </summary>
    public void LoginButtonClick(Object sender, EventArgs e)
    {
        UserManager userManager = new UserManager();
        Human human = new Human();
        string userName = txtUsername.Value;
        string password = txtPassword.Value;

        human = userManager.LoadHumanInformationByUsername(userName);

        // Check the user credentials by username and password
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

    protected override void OnInit(EventArgs e)
    {
        if (!Request.IsSecureConnection)
        {
            UriBuilder builder = new UriBuilder(Request.Url)
            {
                Scheme = Uri.UriSchemeHttps,
                Port = 44300
            };
            Response.Redirect(builder.Uri.ToString());
        }
        base.OnInit(e);
    }
}