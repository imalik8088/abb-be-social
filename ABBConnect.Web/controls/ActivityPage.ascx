<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActivityPage.ascx.cs" Inherits="controls_ActivityPage" %>

<asp:Repeater ID="ActivityRepeater" runat="server" OnItemDataBound="ActivityRepeater_ItemDataBound">
    <ItemTemplate>
        <div id='activity-container-<%# DataBinder.Eval(Container.DataItem,"ID")%>' class="activity-container">
            <div class="activity-inner-container activity-inner-container-default">
                <div class="activity-information">
                    <%--<img class="feed-avatar" alt="" src="content/img/avatar-abb-2.png">--%>
                    <span class="label label-default label-feed-information-default">
                        <%--<%# ((BLL.Activity)Container.DataItem).Type %>--%>
                        Activity
                    </span>
                </div>
                <div class="activity-message activity-message-default">
                    <span class="activity-arrow activity-arrow-default"></span>
                    <a href="userProfile.aspx?userId=<%# ((BLL.Activity)Container.DataItem).UserId %>" class="activity-name activity-name-default">
                        <asp:Literal runat="server" ID="litFeedPosterName"></asp:Literal>
                    </a>
                    <span class="activity-date-time"><%# DataBinder.Eval(Container.DataItem,"TimeStamp")%>
                    </span>
                    <span class="activity-body">
                        <asp:Literal runat="server" ID="litFeedContent"></asp:Literal>
                    </span>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<%--<div runat="server" id="activity_page_load_more_container" class="row feed-page-load-more-container">
    <a id="load_more" runat="server" class="btn btn-danger feed-page-load-more-anchor">Load more</a>
</div>--%>
