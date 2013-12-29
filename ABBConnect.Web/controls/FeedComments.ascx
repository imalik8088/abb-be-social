<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FeedComments.ascx.cs" Inherits="controls_FeedComments" %>
<asp:Repeater ID="feedCommentsRepeater" runat="server">
    <ItemTemplate>
        <div id='feed-single-comment-container-<%# DataBinder.Eval(Container.DataItem,"ID")%>' class="feed-single-comment-container">
            <div class="feed-single-comment-info pull-left">
                <img src="content/img/avatar-abb-small.png" alt="user-avatar">
            </div>
            <div class="feed-single-comment-data">
                <div class="name">                                          
                     <a href="userProfile.aspx?userId=<%# DataBinder.Eval(Container.DataItem,"Owner.ID")%>">
                         <%--<%# DataBinder.Eval(Container.DataItem,"Owner.UserName")%>--%>
                         <%# DataBinder.Eval(Container.DataItem,"Owner.FirstName")%> <%# DataBinder.Eval(Container.DataItem,"Owner.LastName")%>
                     </a>
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
    <textarea type="text" id='feed-post-comment-input-<%= FeedId %>' class="feed-post-comment-input" onclick="focusOnFeedCommentContainer(<%= FeedId %>)">Write comment...</textarea>
    <div id='feed-post-comment-additional-settings-<%= FeedId%>' class="dont-show">
        <div class="feed-single-comment-hr"></div>
        <div class="feed-post-comment-button-container">
            <button type="button" class="btn btn-primary btn-sm" onclick="hideFullFeedCommentContainer(<%= FeedId %>)">Cancel</button>
            <button type="button" class="btn btn-primary btn-sm" onclick="AjaxPostFeedComment(<%= FeedId %>)">Post</button>
        </div>
    </div>
</div>