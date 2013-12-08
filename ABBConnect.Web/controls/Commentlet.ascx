<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Commentlet.ascx.cs" Inherits="controls_Commentlet" %>
<asp:Repeater ID="feedCommentsRepeater" runat="server">
    <ItemTemplate>
        <div id='feed-single-comment-container-<%= FeedId %>' class="feed-single-comment-container">
            <div class="feed-single-comment-info pull-left">
                <img src="content/img/avatar-abb-small.png" alt="user-avatar">
            </div>
            <div class="feed-single-comment-data">
                <div class="name">
                    <a href="#"><%= Feed.Owner.UserName%></a>
                </div>
                <div class="time">
                    <i class="icon-time"></i>
                    <span><%= Feed.TimeStamp%></span>
                </div>
                <div class="feed-single-comment-hr"></div>
                <small><%= Feed.Content %></small>
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
