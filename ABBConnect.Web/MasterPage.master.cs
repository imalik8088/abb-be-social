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
        HumanManager hm = new HumanManager();
        Human human = new Human();
        human = hm.LoadHumanInformation((int)Session["humanID"]);
        labelHuman.Text = human.FirstName + " " + human.LastName;
    }
}
