<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RealTimeSensorFeedPage.ascx.cs" Inherits="controls_RealTimeSensorFeedPage" %>
 <asp:Repeater ID="RealTimeSensorFeedRepeater" runat="server" OnItemDataBound="RealTimeSensorFeedRepeater_ItemDataBound">
     <ItemTemplate>
         <div id="real-time-sensor-feed-container-<%# DataBinder.Eval(Container.DataItem,"ID")%>" class="feed-container">
           <div class="feed-inner-container feed-inner-container-<%# Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString())%>">
                <div class="feed-information">
                    <img class="feed-avatar" alt="" src="content/img/avatar-abb-2.png">
                    <span class="label label-<%# Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString())%> label-feed-information-<%# Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString())%>">
                        <%# Core.ConvertStringToUppercaseFirst(Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString()))%>
                    </span>
                </div>
                <div class="feed-message feed-message-<%# Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString())%>">
                    <span class="feed-arrow feed-arrow-<%# Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString())%>"></span>
                    <a href="#" class="feed-name feed-name-<%# Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString())%>">
                        <asp:Literal runat="server" ID="litFeedPosterName"></asp:Literal>
                    </a>
                    <span class="feed-date-time"><%# DataBinder.Eval(Container.DataItem,"TimeStamp")%>
                    </span>
                    <span class="feed-body row">                     
                        <div class="col-md-12">
                            <div class="bs-callout bs-callout-<%# Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString())%>">
                                <h4>Sensor alert</h4>
                                <div class="bs-callout-hr"></div>
                                <asp:Literal runat="server" ID="litFeedContent"></asp:Literal>
                            </div>
                        </div>
                    </span>
                </div>
            </div>
        </div>
     </ItemTemplate>
</asp:Repeater>
<div runat="server" id="feed_page_load_more_container" class="row feed-page-load-more-container">
    <a id="load_more" runat="server" class="btn btn-danger feed-page-load-more-anchor">Load more</a>
</div>
