<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RealTimeSensorPage.ascx.cs" Inherits="controls_RealTimeSensorPage" %>
<asp:Repeater ID="SensorRepeater" runat="server" OnItemDataBound="SensorRepeater_ItemDataBound">
    <ItemTemplate>
        <div id="real-time-sensor-feed-container-<%# DataBinder.Eval(Container.DataItem,"ID")%>" class="feed-container">
            <div class="feed-inner-container feed-inner-container-sensor">
                <div runat="server" class="feed-gauge" id="feed_gauge">
                   <div id="real-time-sensor-<%# ContainerPrefix + "-" +  DataBinder.Eval(Container.DataItem,"ID")%>" style="width:200px; height:160px"></div>
                </div>
                <div class="feed-message feed-message-gauge feed-message-sensor">
                    <span class="feed-arrow feed-arrow-sensor"></span>
                    <a href="#" class="feed-name feed-name-sensor">
                        <asp:Literal runat="server" ID="litFeedPosterName"></asp:Literal>
                    </a>
                    <span class="feed-body row">
                        <div class="col-md-12">
                            <div class="bs-callout bs-callout-sensor">
                                <h4>
                                    <span class="glyphicon glyphicon-map-marker"></span>
                                    <asp:Literal runat="server" ID="litSensorMessage"></asp:Literal>
                                </h4>
                                <div class="bs-callout-hr"></div>
                                <div class="feed-page-load-more-container">
                                    <div runat="server" id="userFollowSensorDisabled" class="dont-show">
                                        <a class="btn btn-success disabled" onclick="AjaxUserFollowSensor(<%# DataBinder.Eval(Container.DataItem,"ID")%>)"><span class="glyphicon glyphicon-plus"></span> Follow</a>
                                    </div>
                                    <div runat="server" id="userFollowSensor" class="dont-show">
                                        <a class="btn btn-success" onclick="AjaxUserFollowSensor(<%# DataBinder.Eval(Container.DataItem,"ID")%>)"><span class="glyphicon glyphicon-plus"></span> Follow</a>
                                    </div>
                                    <div runat="server" id="userUnFollowSensor" class="dont-show">
                                        <a class="btn btn-danger" onclick="AjaxUserUnFollowSensor(<%# DataBinder.Eval(Container.DataItem,"ID")%>)"><span class="glyphicon glyphicon-minus"></span> Unfollow</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </span>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
