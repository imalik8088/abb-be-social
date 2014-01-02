using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class controls_RealTimeSensorPage : System.Web.UI.UserControl
{
    // Default Sensor Settings
    public bool IsFilteredByUser = false;

    public int FilterUserId = -1;
    public string ContainerPrefix = "all";
    private List<Sensor> userFollowedSensorList;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void RenderSensorPage()
    {
        UserManager userManager = new UserManager();
        List<Sensor> sensorList = new List<Sensor>();

        this.userFollowedSensorList = userManager.GetFollowedSensors(int.Parse(Session["humanID"].ToString()));

        if (IsFilteredByUser == true)
        {
            sensorList = this.userFollowedSensorList;
        }
        else
        {
            sensorList = userManager.GetAllSensors();          
        }

        SensorRepeater.DataSource = sensorList;
        SensorRepeater.DataBind();
    }
    protected void SensorRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Literal l = new Literal();
        HtmlGenericControl hgc = new HtmlGenericControl();

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            /* Determine feedType (Human, Sensor) and render innerMessage (FeedContent) for it (TextMessage, SensorWarning, SensorGauge)
             * 
             */
            UserManager userManager = new UserManager();
            Sensor currentSensor = ((Sensor)e.Item.DataItem);

            if (userFollowedSensorList.Any(s => s.ID == currentSensor.ID) == true)
            {
                if (ContainerPrefix == "all")
                {
                    ((HtmlGenericControl)e.Item.FindControl("userFollowSensor")).Attributes.Remove("class");
                    ((HtmlGenericControl)e.Item.FindControl("userFollowSensor")).Attributes.Add("class", "dont-show");

                    ((HtmlGenericControl)e.Item.FindControl("userFollowSensorDisabled")).Attributes.Remove("class");
                }
                else
                {
                    ((HtmlGenericControl)e.Item.FindControl("userFollowSensor")).Attributes.Remove("class");
                    ((HtmlGenericControl)e.Item.FindControl("userFollowSensor")).Attributes.Add("class", "dont-show");

                    ((HtmlGenericControl)e.Item.FindControl("userUnFollowSensor")).Attributes.Remove("class");
                }               
            }
            else
            {
                ((HtmlGenericControl)e.Item.FindControl("userUnFollowSensor")).Attributes.Remove("class");
                ((HtmlGenericControl)e.Item.FindControl("userUnFollowSensor")).Attributes.Add("class", "dont-show");

                ((HtmlGenericControl)e.Item.FindControl("userFollowSensor")).Attributes.Remove("class");
            }

            int currentSensorValue = 0;
            int maxSensorValue = 0;
            int minSensorValue = 0;

            minSensorValue = (int)currentSensor.LowerBoundary;
            maxSensorValue = (int)currentSensor.UpperBoundary;
            currentSensorValue = userManager.LoadCurrentValuesBySensor(currentSensor.ID);

            l = (Literal)e.Item.FindControl("litSensorMessage");
            l.Text = currentSensor.Location;

            var gaugeDiv = ((HtmlGenericControl)e.Item.FindControl("feed_gauge"));
            gaugeDiv.Attributes.Add("data-containerPrefix", ContainerPrefix);
            gaugeDiv.Attributes.Add("data-sensorId", currentSensor.ID.ToString());
            gaugeDiv.Attributes.Add("data-currentSensorValue", currentSensorValue.ToString());
            gaugeDiv.Attributes.Add("data-minSensorValue", minSensorValue.ToString());
            gaugeDiv.Attributes.Add("data-maxSensorValue", maxSensorValue.ToString());
            gaugeDiv.Attributes.Add("data-sensorTitle", currentSensor.UserName);
            gaugeDiv.Attributes.Add("data-sensorUnit", currentSensor.UnitMetric);
            
            string jGaugeSensorInit = "  var g = new JustGage({    id: 'real-time-sensor-" + ContainerPrefix + "-" +currentSensor.ID + "',     value: " + currentSensorValue + ",     min: " + minSensorValue + ",    max: " + maxSensorValue + ",    title: '" + currentSensor.UserName + "', label: '" + currentSensor.UnitMetric + "'  }); ";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "initGauge-" + ContainerPrefix + "-"  + currentSensor.ID, "<script type='text/javascript'>" + jGaugeSensorInit + "</script>", false);  
        }      
    }
}