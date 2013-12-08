<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewFeedPagelet.ascx.cs" Inherits="controls_NewFeedPagelet" %>

<asp:Repeater ID="FeedRepeater" runat="server" OnItemDataBound="FeedRepeater_ItemDataBound">
    <ItemTemplate>
        <div id='feed-container-<%# DataBinder.Eval(Container.DataItem,"ID")%>' class="feed-container">
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
                    <span class="feed-body">
                        <asp:Literal runat="server" ID="litFeedContent"></asp:Literal>
                    </span>
                </div>
            </div>
            <div class="feed-comments-container">
                <asp:Repeater ID="feedCommentsRepeater" runat="server">
                    <ItemTemplate>
                        <div id='feed-single-comment-container-<%# DataBinder.Eval(Container.DataItem,"ID")%>' class="feed-single-comment-container">
                            <div class="feed-single-comment-info pull-left">
                                <img src="content/img/avatar-abb-small.png" alt="user-avatar">
                            </div>
                            <div class="feed-single-comment-data">
                                <div class="name">
                                    <a href="#"><%# DataBinder.Eval(Container.DataItem,"Owner.UserName")%></a>
                                </div>
                                <div class="time">
                                    <i class="icon-time"></i>
                                    <span><%# DataBinder.Eval(Container.DataItem,"TimeStamp")%></span>
                                </div>
                                <div class="feed-single-comment-hr"></div>
                                <small><%# DataBinder.Eval(Container.DataItem,"Content")%></small>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="feed-post-comment-data">
                    <textarea type="text" id='feed-post-comment-input-<%# DataBinder.Eval(Container.DataItem,"ID")%>' class="feed-post-comment-input" onclick="focusOnFeedCommentContainer(<%# DataBinder.Eval(Container.DataItem,"ID")%>)">Write comment...</textarea>
                    <div id='feed-post-comment-additional-settings-<%# DataBinder.Eval(Container.DataItem,"ID")%>' class="dont-show">
                        <div class="feed-single-comment-hr"></div>
                        <div class="feed-post-comment-button-container">
                            <button type="button" class="btn btn-primary btn-sm" onclick="hideFullFeedCommentContainer(<%# DataBinder.Eval(Container.DataItem,"ID")%>)">Cancel</button>
                            <button type="button" class="btn btn-primary btn-sm" onclick="AjaxPostFeedComment(<%# DataBinder.Eval(Container.DataItem,"ID")%>)">Post</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
