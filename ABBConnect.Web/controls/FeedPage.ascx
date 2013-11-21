<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FeedPage.ascx.cs" Inherits="controls_FeedPage" %>

<asp:Repeater ID="FeedRepeater" runat="server" OnItemDataBound="FeedRepeater_ItemDataBound">
    <ItemTemplate>
        <div id="feed-container-<%# DataBinder.Eval(Container.DataItem,"ID")%>" class="feed-container">
            <div class="feed-inner-container feed-inner-container-danger">
                <div class="feed-information">
                    <img class="feed-avatar" alt="" src="content/img/avatar-abb-2.png">
                    <span class="label label-danger label-feed-information-danger">Danger</span>
                </div>
                <div class="feed-message feed-message-danger">
                    <span class="feed-arrow feed-arrow-danger"></span>
                    <a href="#" class="feed-name feed-name-danger" ><asp:Literal runat="server" ID="litFeedPosterName"></asp:Literal>
                    </a>
                    <span class="feed-date-time"><%# DataBinder.Eval(Container.DataItem,"TimeStamp")%>
                    </span>
                    <span class="feed-body"><asp:Literal runat="server" ID="litFeedContent"></asp:Literal>
                    </span>
                </div>
            </div>
            <div class="feed-comments-container">
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
