<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LastShift.aspx.cs" Inherits="_LastShift" %>

<%@ Register Src="controls/LastShiftFeedPage.ascx" TagName="LastShiftFeedPage" TagPrefix="abbConnect" %>
<%@ Register Src="controls/FeedComments.ascx" TagName="FeedComments" TagPrefix="abbConnect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>$("#LastShiftIcon").addClass("active"); type = "text/javascript"</script>
    <div class="row">
        <div class="col-md-6">
            <div class="feed-header">
                <div class="form-inline">
                    <div class="form-group">
                        <h3><span class="glyphicon glyphicon-link"></span>Last shift user feeds <small>Feeds from human source</small></h3>
                    </div>
                </div>
            </div>
            <div id="feedsContainer">
                <abbConnect:LastShiftFeedPage ID="LastShiftFeedPage" runat="server" />
            </div>
            <div id="loading_throbber_human_feeds" class="loading-throbber" data-container="feedsContainer"></div>
        </div>
    </div>
</asp:Content>
