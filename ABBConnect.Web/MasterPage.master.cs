using System;
using System.Collections;
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
        UserManager userManager = new UserManager();
        Human human = new Human();
        ArrayList values = new ArrayList();

        if (Session["humanID"] != null)
        {
            human = userManager.LoadHumanInformation(int.Parse(HttpContext.Current.Session["humanID"].ToString()));
            values.Add(new LogedUserInfo(human.ID, human.FirstName, human.LastName));

            Repeater1.DataSource = values;
            Repeater1.DataBind();
        }
    }
}

public class LogedUserInfo
{
    private int id;
    private string firstName;
    private string lastName;

    public LogedUserInfo(int id, string name, string surname)
    {
        this.id = id;
        this.firstName = name;
        this.lastName = surname;
    }

    public int ID
    {
        get
        {
            return id;
        }
    }

    public string FirstName
    {
        get
        {
            return firstName;
        }
    }

    public string LastName
    {
        get
        {
            return lastName;
        }
    }
}