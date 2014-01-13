<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FeedPage.ascx.cs" Inherits="controls_FeedPage" %>

<asp:Repeater ID="FeedRepeater" runat="server" OnItemDataBound="FeedRepeater_ItemDataBound">
    <ItemTemplate>
        <div id="feed-container-<%# DataBinder.Eval(Container.DataItem,"ID")%>" class="feed-container">
            <div class="feed-inner-container feed-inner-container-<%# Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString())%>">
                <div class="feed-information">
                    <asp:Literal runat="server" ID="litFeedAvatar"></asp:Literal>
                    <span class="label label-<%# Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString())%> label-feed-information-<%# Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString())%>">
                        <%# Core.ConvertStringToUppercaseFirst(Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString()))%>
                    </span>
                </div>
                <div class="feed-message feed-message-<%# Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString())%>">
                    <span class="feed-arrow feed-arrow-<%# Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString())%>"></span>
                    <a href="userProfile.aspx?userId=<%# ((BLL.HumanFeed)Container.DataItem).Owner.ID %>" class="feed-name feed-name-<%# Core.GetPriorityCssClass(DataBinder.Eval(((BLL.Feed)Container.DataItem).Category,"Priority").ToString())%>">
                        <asp:Literal runat="server" ID="litFeedPosterName"></asp:Literal>
                    </a>
                    <span class="feed-date-time"><%# DataBinder.Eval(Container.DataItem,"TimeStamp")%>
                    </span>
                    <span class="feed-body">
                        <asp:Literal runat="server" ID="litFeedContent"></asp:Literal>
                    </span>
                    <span class="feed-body-addition">
                        <div class="control-group">
                            <hr class="mXhr10">
                            <input runat="server" type="text" id="feed_input_tags" class="feed-input-tags locked">
                            <script>
                                initSelectize('feed-container', '<%# DataBinder.Eval(Container.DataItem,"ID")%>', 1);
                            </script>
                        </div>
                    </span>
                </div>
            </div>
            <div class="feed-comments-container">
                <div class="feed-comments-data">
                    <asp:Repeater ID="feedCommentsRepeater" runat="server">
                        <ItemTemplate>
                            <div id="feed-single-comment-container-<%# DataBinder.Eval(Container.DataItem,"ID")%>" class="feed-single-comment-container">
                                <div class="feed-single-comment-info pull-left">
                                    <img src="<%# (((String)DataBinder.Eval(Container.DataItem,"Owner.Avatar")) == "")? "content/img/avatar-abb-small.png": ((String)DataBinder.Eval(Container.DataItem,"Owner.Avatar")) %>" alt="user-avatar">
                                </div>
                                <div class="feed-single-comment-data">
                                    <div class="name">
                                        <a href="userProfile.aspx?userId=<%# DataBinder.Eval(Container.DataItem,"Owner.ID")%>">
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
                    <div runat="server" id="feed_show_all_comments" class="feed-show-all-comments-container">
                        <div class="feed-show-all-comments-data" onclick="AjaxGetAllFeedComments(<%# DataBinder.Eval(Container.DataItem,"ID")%>)">Show All Comments (<%#((BLL.Feed)Container.DataItem).Comments.Count.ToString()%>)</div>
                    </div>
                    <div class="feed-post-comment-data">
                        <textarea type="text" id="feed-post-comment-input-<%# DataBinder.Eval(Container.DataItem,"ID")%>" class="feed-post-comment-input" onclick="focusOnFeedCommentContainer(<%# DataBinder.Eval(Container.DataItem,"ID")%>)">Write comment...</textarea>
                        <div id="feed-post-comment-additional-settings-<%# DataBinder.Eval(Container.DataItem,"ID")%>" class="dont-show">
                            <div class="feed-single-comment-hr"></div>
                            <div class="feed-post-comment-button-container">
                                <button type="button" class="btn btn-primary btn-sm" onclick="hideFullFeedCommentContainer(<%# DataBinder.Eval(Container.DataItem,"ID")%>)">Cancel</button>
                                <button type="button" class="btn btn-primary btn-sm" onclick="AjaxPostFeedComment(<%# DataBinder.Eval(Container.DataItem,"ID")%>)">Post</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="loading_throbber_human_feed_comments" class="loading-throbber" data-container="feed-comments-container"></div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<div runat="server" id="feed_page_load_more_container" class="row feed-page-load-more-container">
    <a id="load_more" runat="server" class="btn btn-danger feed-page-load-more-anchor">Load more</a>
</div>
